﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Snowlight.Game.Sessions;
using Snowlight.Storage;
using System.Data;

namespace Snowlight.Game.Characters
{
    class CharacterInfoLoader
    {
        
        static Dictionary<uint, CharacterInfo> dictionary_0;
        
        const double double_0 = 300.0;
        
        static Thread thread_0;

        public static CharacterInfo GenerateCharacterInfoFromRow(SqlDatabaseClient MySqlClient, uint LinkedClientId, DataRow Row)
        {
            return new CharacterInfo(MySqlClient, LinkedClientId, 
                uint.Parse(Row["id"].ToString()), 
                (string)Row["usuario"], 
                (string)Row["email"], 
                (string)Row["bocadillo"], 
                uint.Parse(Row["tipo_avatar"].ToString()), 
                (string)Row["colores_avatar"], 
                (string)Row["ciudad"], 
                uint.Parse(Row["edad"].ToString()), 
                uint.Parse(Row["moderador"].ToString()), 
                uint.Parse(Row["vip"].ToString()), 
                int.Parse(Row["creditos_oro"].ToString()), 
                int.Parse(Row["creditos_plata"].ToString()), 
                int.Parse(Row["besos_enviados"].ToString()), 
                int.Parse(Row["cocteles_enviados"].ToString()), 
                int.Parse(Row["flores_enviadas"].ToString()), 
                int.Parse(Row["cocos_enviados"].ToString()), 
                int.Parse(Row["uppercuts_enviados"].ToString()), 
                int.Parse(Row["besos_recibidos"].ToString()), 
                int.Parse(Row["cocteles_recibidos"].ToString()), 
                int.Parse(Row["flores_recibidas"].ToString()), 
                int.Parse(Row["cocos_recibidos"].ToString()), 
                int.Parse(Row["uppercuts_recibidos"].ToString()), 
                (string)Row["hobby_1"], 
                (string)Row["hobby_2"], 
                (string)Row["hobby_3"], 
                (string)Row["deseo_1"], 
                (string)Row["deseo_2"], 
                (string)Row["deseo_3"], 
                uint.Parse(Row["votos_legal"].ToString()), 
                uint.Parse(Row["votos_sexy"].ToString()), 
                uint.Parse(Row["votos_simpatico"].ToString()), 
                uint.Parse(Row["tiempo_registrado"].ToString()), 
                uint.Parse(Row["rings_ganados"].ToString()), 
                uint.Parse(Row["cocos_ganados"].ToString()), 
                uint.Parse(Row["nivel_ring"].ToString()), 
                uint.Parse(Row["nivel_cocos"].ToString()), 
                uint.Parse(Row["nivel_ninja"].ToString()), 
                uint.Parse(Row["cambios"].ToString()), 
                double.Parse(Row["ultimo_login"].ToString()), 
                double.Parse(Row["timestamp_monedas"].ToString()));
        }

        public static CharacterInfo GenerateNullCharacter(uint uint_0)
        {
            return new CharacterInfo(null, 0, 0, "Unknown", string.Empty, "BoomBang", 1, string.Empty, string.Empty, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, UnixTimestamp.GetCurrent(), UnixTimestamp.GetCurrent());
        }

        public static CharacterInfo GetCharacterInfo(SqlDatabaseClient MySqlClient, uint CharacterId)
        {
            return GetCharacterInfo(MySqlClient, CharacterId, 0, false);
        }

        public static CharacterInfo GetCharacterInfo(SqlDatabaseClient MySqlClient, uint CharacterId, uint LinkedClientId, bool IgnoreCache)
        {
            if (SessionManager.ContainsCharacterId(CharacterId))
            {
                return SessionManager.GetSessionByCharacterId(CharacterId).CharacterInfo;
            }
            if (!IgnoreCache)
            {
                CharacterInfo info = smethod_1(CharacterId);
                if (info != null)
                {
                    return info;
                }
            }
            MySqlClient.SetParameter("id", CharacterId);
            DataRow row = MySqlClient.ExecuteQueryRow("SELECT * FROM usuarios WHERE id = @id LIMIT 1");
            if (row != null)
            {
                return GenerateCharacterInfoFromRow(MySqlClient, LinkedClientId, row);
            }
            return null;
        }

        public static void Initialize()
        {
            dictionary_0 = new Dictionary<uint, CharacterInfo>();
            thread_0 = new Thread(new ThreadStart(CharacterInfoLoader.smethod_0));
            thread_0.Name = "CharacterInfoLoader Cache Monitor";
            thread_0.Priority = ThreadPriority.Lowest;
            thread_0.Start();
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
                        foreach (CharacterInfo info in dictionary_0.Values)
                        {
                            if (SessionManager.ContainsCharacterId(info.Id) || (info.CacheAge >= 300.0))
                            {
                                list.Add(info.Id);
                            }
                        }
                        foreach (uint num in list)
                        {
                            dictionary_0.Remove(num);
                        }
                    }
                    Thread.Sleep(30000);
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        private static CharacterInfo smethod_1(uint uint_0)
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
