using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using Snowlight.Storage;
using Snowlight.Game.Navigation;
using System.Data;
using Snowlight.Game.Sessions;
using Snowlight.Specialized;
using Snowlight.Game.Laptop;
using Snowlight.Communication.Outgoing;

namespace Snowlight.Game.Spaces
{
    class SpaceManager
    {
        /* private scope */
        static ConcurrentDictionary<uint, SpaceInstance> concurrentDictionary_0;
        /* private scope */
        static ConcurrentDictionary<string, SpaceModel> concurrentDictionary_1;
        /* private scope */
        static object object_0;
        public const int SPACE_UPDATE_SPEED = 710;
        /* private scope */
        static Thread thread_0;
        /* private scope */
        static Thread thread_1;
        /* private scope */
        static uint uint_0;

        public static uint CreateArea(uint OwnerId, string Name, string Model, uint IslandId)
        {
            return 0;
        }

        public static uint CreateIsland(uint OwnerId, string Name, string Model)
        {
            string s = string.Empty;
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                client.SetParameter("type", "flat");
                client.SetParameter("ownerid", OwnerId);
                client.SetParameter("name", Name);
                client.SetParameter("model", Model);
                s = client.ExecuteScalar("INSERT INTO escenarios (tipo_area,id_usuario,nombre,modelo) VALUES (@type,@ownerid,@name,@model); SELECT LAST_INSERT_ID();").ToString();
            }
            if (!(s == string.Empty))
            {
                return uint.Parse(s);
            }
            return 0;
        }

        public static void DeleteArea(SqlDatabaseClient MySqlClient, uint AreaId)
        {
        }

        public static void DeleteIsland(SqlDatabaseClient MySqlClient, uint IslandId)
        {
            MySqlClient.SetParameter("id", IslandId);
            MySqlClient.ExecuteNonQuery("DELETE FROM escenarios WHERE id = @id LIMIT 1");
            SpaceInfoLoader.RemoveFromCache(IslandId);
            Navigator.ReloadOfficialItems(MySqlClient);
        }

        public static uint GenerateInstanceId()
        {
            lock (object_0)
            {
                return uint_0++;
            }
        }

        public static SpaceInstance GetInstanceBySpaceId(uint spaceId)
        {
            foreach (SpaceInstance instance in concurrentDictionary_0.Values)
            {
                if (instance.Info.UInt32_0 == spaceId)
                {
                    return instance;
                }
                if (!instance.Unloaded && (instance.Info.UInt32_0 == spaceId))
                {
                    return instance;
                }
            }
            //Console.WriteLine("null");
            return null;
        }

        public static SpaceModel GetModel(string string_0)
        {
            foreach (SpaceModel model in concurrentDictionary_1.Values)
            {
                if (model.String_0 == string_0)
                {
                    return model;
                }
            }
            return null;
        }

        public static void Initialize(SqlDatabaseClient MySqlClient)
        {
            concurrentDictionary_0 = new ConcurrentDictionary<uint, SpaceInstance>();
            concurrentDictionary_1 = new ConcurrentDictionary<string, SpaceModel>();
            ReloadModels(MySqlClient);
            thread_0 = new Thread(new ThreadStart(SpaceManager.smethod_0));
            thread_0.Name = "spaceInstanceThread";
            thread_0.Priority = ThreadPriority.Highest;
            thread_0.Start();
            thread_1 = new Thread(new ThreadStart(SpaceManager.smethod_1));
            thread_1.Name = "spaceWritebackThread";
            thread_1.Priority = ThreadPriority.BelowNormal;
            thread_1.Start();
            uint_0 = 1;
            object_0 = new object();
        }

        public static bool InstanceIsLoadedForSpace(uint SpaceId)
        {
            return (GetInstanceBySpaceId(SpaceId) != null);
        }

        public static void ReloadModels(SqlDatabaseClient MySqlClient)
        {
            concurrentDictionary_1.Clear();
            foreach (DataRow row in MySqlClient.ExecuteQueryTable("SELECT * FROM modelos").Rows)
            {
                concurrentDictionary_1.TryAdd((string)row["id"], new SpaceModel((string)row["id"], (((string)row["tipo"]) == "isla") ? SpaceModelType.Island : SpaceModelType.Area, new Heightmap((string)row["mapa_bits"]), new Vector3((int)row["pos_x"], (int)row["pos_y"], (int)row["pos_z"]), (int)row["rotacion"], (int)row["max_usuarios"]));
            }
        }

        public static bool RemoveUserFromSpace(Session Session, bool SendKick = true)
        {
            bool flag = false;
            uint absoluteSpaceId = Session.AbsoluteSpaceId;
            if (absoluteSpaceId > 0)
            {
                if (Session.SpaceJoined)
                {
                    SpaceInstance instanceBySpaceId = GetInstanceBySpaceId(absoluteSpaceId);
                    if (instanceBySpaceId != null)
                    {
                        instanceBySpaceId.RemoveCharacterFromSpace(Session.CharacterId);
                    }
                }
                Session.AbsoluteSpaceId = 0;
                Session.SpaceAuthed = false;
                Session.SpaceJoined = false;
                LaptopHandler.MarkUpdateNeeded(Session, 0, false);
                flag = true;
            }
            if (SendKick)
            {
                Session.SendData(SpaceUserRemovedComposer.SingleCompose());
            }
            return flag;
        }

        private static void smethod_0()
        {
            try
            {
                while (Program.Alive)
                {
                    DateTime now = DateTime.Now;
                    Dictionary<uint, SpaceInstance> dictionary = new Dictionary<uint, SpaceInstance>(concurrentDictionary_0);
                    List<uint> list = new List<uint>();
                    List<uint> list2 = new List<uint>();
                    foreach (SpaceInstance instance in dictionary.Values)
                    {
                        if (instance.Unloaded)
                        {
                            if (instance.TimeUnloaded <= 15.0)
                            {
                                goto Label_00B6;
                            }
                            list.Add(instance.InstanceId);
                            continue;
                        }
                        if (instance.HumanActorCount == 0)
                        {
                            if (instance.MarkedAsEmpty >= 10)
                            {
                                list2.Add(instance.InstanceId);
                            }
                            else
                            {
                                instance.MarkedAsEmpty++;
                            }
                            continue;
                        }
                        if (instance.MarkedAsEmpty > 0)
                        {
                            instance.MarkedAsEmpty = 0;
                        }
                    Label_00B6:
                        foreach (SpaceActor actor in instance.Actors)
                        {
                            Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(actor.ReferenceId);
                            if ((sessionByCharacterId == null) || (sessionByCharacterId.AbsoluteSpaceId != instance.SpaceId))
                            {
                                instance.RemoveActorFromSpace(actor.UInt32_0);
                            }
                        }
                        instance.PerformUpdate();
                    }
                    foreach (uint num in list2)
                    {
                        if (concurrentDictionary_0.ContainsKey(num))
                        {
                            concurrentDictionary_0[num].Unload();
                            Output.WriteLine("[SpaceMgr] Unloaded Space instance #" + num + ".", OutputLevel.DebugInformation);
                        }
                    }
                    foreach (uint num2 in list)
                    {
                        SpaceInstance instance2 = null;
                        concurrentDictionary_0[num2].Dispose();
                        concurrentDictionary_0[num2] = null;
                        concurrentDictionary_0.TryRemove(num2, out instance2);
                        Output.WriteLine("[SpaceMgr] Disposed Space instance #" + num2 + " and associated resources.", OutputLevel.DebugInformation);
                    }
                    TimeSpan span = (TimeSpan)(DateTime.Now - now);
                    double num3 = 710.0 - span.TotalMilliseconds;
                    if (num3 < 0.0)
                    {
                        num3 = 0.0;
                        Output.WriteLine("Can't keep up! Did the system time change, or is the server overloaded?", OutputLevel.Warning);
                    }
                    if (num3 > 710.0)
                    {
                        num3 = 710.0;
                    }
                    Thread.Sleep((int)num3);
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        private static void smethod_1()
        {
            try
            {
                while (Program.Alive)
                {
                    List<SpaceInstance> list = new List<SpaceInstance>(concurrentDictionary_0.Values);
                    foreach (SpaceInstance instance in list)
                    {
                        if (instance.ActorCountDatabaseWritebackNeeded)
                        {
                            instance.DoActorCountSync();
                        }
                    }
                    Thread.Sleep(0x1d4c);
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        public static bool TryLoadSpaceInstance(uint SpaceId)
        {
            if (GetInstanceBySpaceId(SpaceId) != null)
            {
                return false;
            }
            uint instanceId = GenerateInstanceId();
            SpaceInstance instance2 = SpaceInstance.TryCreateSpaceInstance(instanceId, SpaceId);
            if (instance2 == null)
            {
                return false;
            }
            concurrentDictionary_0.TryAdd(instanceId, instance2);
            Output.WriteLine(string.Concat(new object[] { "[SpaceMgr] Space instance #", instanceId, " has been loaded for area #", SpaceId, "." }), OutputLevel.DebugInformation);
            return true;
        }

        public static Dictionary<uint, SpaceInstance> SpaceInstances
        {
            get
            {
                return new Dictionary<uint, SpaceInstance>(concurrentDictionary_0);
            }
        }
    }
}
