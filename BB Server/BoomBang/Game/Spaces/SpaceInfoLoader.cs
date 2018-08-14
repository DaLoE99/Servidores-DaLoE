using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Storage;
using System.Data;
using System.Threading;

namespace Snowlight.Game.Spaces
{
    class SpaceInfoLoader
    {
        /* private scope */
        static Dictionary<uint, SpaceInfo> dictionary_0;
        /* private scope */
        const double double_0 = 300.0;
        /* private scope */
        static Thread thread_0;

        public static SpaceInfo GenerateRawSpaceInfo(uint uint_0, List<uint> SubIds, uint ParentId, string Name, string Description, uint OwnerId, uint SpaceId, SpaceType Type, SpaceAccessType AccessType, string Password, bool AllowCoconuts, bool AllowUppercuts, List<string> WhiteList, List<string> BlackList, uint MaxUsers, string Model)
        {
            return new SpaceInfo(uint_0, SubIds, ParentId, Name, Description, OwnerId, SpaceId, Type, AccessType, Password, AllowUppercuts, AllowCoconuts, WhiteList, BlackList, MaxUsers, Model);
        }

        public static SpaceInfo GenerateSpaceInfoFromRow(DataRow Row)
        {
            SpaceAccessType open = SpaceAccessType.Open;
            string str = Row["acceso"].ToString();
            if (str != null)
            {
                if (!(str == "lock"))
                {
                    if (str == "password")
                    {
                        open = SpaceAccessType.PasswordProtected;
                    }
                }
                else
                {
                    open = SpaceAccessType.Locked;
                }
            }
            SpaceType area = SpaceType.Area;
            string str2 = Row["tipo_area"].ToString();
            if (str2 != null)
            {
                if (str2 == "area")
                {
                    area = SpaceType.Area;
                }
                else if (str2 == "island")
                {
                    area = SpaceType.Island;
                }
                else if (!(str2 == "game"))
                {
                    if (str2 == "home")
                    {
                        area = SpaceType.Home;
                    }
                }
                else
                {
                    area = SpaceType.Game;
                }
            }
            List<uint> subIds = new List<uint>();
            List<string> whiteList = new List<string>();
            List<string> blackList = new List<string>();
            string[] strArray = Row["ids_secundarias"].ToString().Split(new char[] { ',' });
            string[] strArray2 = Row["lista_verde"].ToString().Split(new char[] { ',' });
            string[] strArray3 = Row["lista_negra"].ToString().Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                uint result = 0;
                uint.TryParse(strArray[i], out result);
                if (result > 0)
                {
                    subIds.Add(result);
                }
            }
            for (int j = 0; j < strArray2.Length; j++)
            {
                whiteList.Add(strArray2[j]);
            }
            for (int k = 0; k < strArray3.Length; k++)
            {
                blackList.Add(strArray3[k]);
            }
            return new SpaceInfo((uint)Row["id"], 
                subIds, 
                uint.Parse(Row["id_principal"].ToString()),
                (string)Row["nombre"],
                (string)Row["descripcion"],
                uint.Parse(Row["id_usuario"].ToString()), 
                uint.Parse(Row["tipo_decoracion"].ToString()),
                area, 
                open,
                (string)Row["password"],
                Row["permitir_uppercut"].ToString() == "1",
                Row["permitir_coco"].ToString() == "1",
                whiteList,
                blackList,
                uint.Parse(Row["max_visitantes"].ToString()),
                (string)Row["modelo"]);
        }

        public static SpaceInfo GetSpaceInfo(uint SpaceId)
        {
            return GetSpaceInfo(SpaceId, false);
        }

        public static SpaceInfo GetSpaceInfo(uint SpaceId, bool IgnoreCache)
        {
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(SpaceId);
            if (instanceBySpaceId != null)
            {
                return instanceBySpaceId.Info;
            }
            if (!IgnoreCache)
            {
                SpaceInfo info = smethod_1(SpaceId);
                if (info != null)
                {
                    return info;
                }
            }
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                client.SetParameter("id", SpaceId);
                string query = "SELECT * FROM escenarios WHERE id = " + SpaceId +" LIMIT 1";
                Console.WriteLine(query);
                DataRow row = client.ExecuteQueryRow(query);
                if (row != null)
                {
                    return GenerateSpaceInfoFromRow(row);
                }
            }
            return null;
        }

        public static void Initialize()
        {
            dictionary_0 = new Dictionary<uint, SpaceInfo>();
            thread_0 = new Thread(new ThreadStart(SpaceInfoLoader.smethod_0));
            thread_0.Name = "SpaceInfoLoader Cache Monitor";
            thread_0.Priority = ThreadPriority.Lowest;
            thread_0.Start();
        }

        public static void RemoveFromCache(uint SpaceId)
        {
            lock (dictionary_0)
            {
                if (dictionary_0.ContainsKey(SpaceId))
                {
                    dictionary_0.Remove(SpaceId);
                }
            }
        }

        private static void smethod_0()
        {
            try
            {
                while (Program.Alive)
                {
                    lock (dictionary_0)
                    {
                        List<uint> list = new List<uint>();
                        foreach (SpaceInfo info in dictionary_0.Values)
                        {
                            if ((SpaceManager.GetInstanceBySpaceId(info.UInt32_0) != null) || (info.CacheAge >= 300.0))
                            {
                                list.Add(info.UInt32_0);
                            }
                        }
                        foreach (uint num in list)
                        {
                            dictionary_0.Remove(num);
                        }
                    }
                    Thread.Sleep(0x7530);
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        private static SpaceInfo smethod_1(uint uint_0)
        {
            lock (dictionary_0)
            {
                if (dictionary_0.ContainsKey(uint_0))
                {
                    return dictionary_0[uint_0];
                }
            }
            return null;
        }
    }
}
