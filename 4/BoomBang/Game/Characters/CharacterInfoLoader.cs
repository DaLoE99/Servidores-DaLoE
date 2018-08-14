namespace BoomBang.Game.Characters
{
    using BoomBang;
    using BoomBang.Game.Sessions;
    using BoomBang.Storage;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;

    public static class CharacterInfoLoader
    {
        /* private scope */ static Dictionary<uint, CharacterInfo> dictionary_0;
        /* private scope */ const double double_0 = 300.0;
        /* private scope */ static Thread thread_0;

        public static CharacterInfo GenerateCharacterInfoFromRow(SqlDatabaseClient MySqlClient, uint LinkedClientId, DataRow Row)
        {
           return new CharacterInfo(MySqlClient, LinkedClientId, (uint)Row["id"], (string)Row["usuario"], (string)Row["email"], (string)Row["bocadillo"], (uint)Row["tipo_avatar"], (string)Row["colores_avatar"], (string)Row["ciudad"], (uint)Row["edad"], (uint)Row["moderador"], (uint)Row["vip"], (int)Row["creditos_oro"], (int)Row["creditos_plata"], (int)Row["besos_enviados"], (int)Row["cocteles_enviados"], (int)Row["flores_enviadas"], (int)Row["cocos_enviados"], (int)Row["uppercuts_enviados"], (int)Row["besos_recibidos"], (int)Row["cocteles_recibidos"], (int)Row["flores_recibidas"], (int)Row["cocos_recibidos"], (int)Row["uppercuts_recibidos"], (string)Row["hobby_1"], (string)Row["hobby_2"], (string)Row["hobby_3"], (string)Row["deseo_1"], (string)Row["deseo_2"], (string)Row["deseo_3"], (uint)Row["votos_legal"], (uint)Row["votos_sexy"], (uint)Row["votos_simpatico"], (uint)Row["tiempo_registrado"], (uint)Row["rings_ganados"], (uint)Row["cocos_ganados"], (uint)Row["nivel_ring"], (uint)Row["nivel_cocos"], (uint)Row["nivel_ninja"], (uint)Row["cambios"], (double)Row["ultimo_login"], (double)Row["timestamp_monedas"]);
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
                            if (SessionManager.ContainsCharacterId(info.UInt32_0) || (info.CacheAge >= 300.0))
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

