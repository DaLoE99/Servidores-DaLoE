namespace BoomBang.Game.Moderation
{
    using BoomBang;
    using BoomBang.Storage;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;

    public static class ModerationBanManager
    {
        /* private scope */ static Dictionary<uint, BanDetails> dictionary_0;
        /* private scope */ static List<uint> list_0;
        /* private scope */ static List<string> list_1;
        /* private scope */ static object object_0;
        /* private scope */ static Thread thread_0;

        public static void BanUser(SqlDatabaseClient MySqlClient, uint UserId, uint BanType, string MessageText, string Moderator, double Length)
        {
            MySqlClient.SetParameter("userid", UserId);
            MySqlClient.SetParameter("bantype", BanType);
            MySqlClient.SetParameter("reason", MessageText);
            MySqlClient.SetParameter("timestamp", UnixTimestamp.GetCurrent());
            MySqlClient.SetParameter("timestampex", UnixTimestamp.GetCurrent() + Length);
            MySqlClient.SetParameter("moderator", Moderator);
            MySqlClient.ExecuteNonQuery("INSERT INTO baneos (id_usuario,tipo_baneo,detalles,timestamp,timestampex,moderador) VALUES (@userid,@bantype,@reason,@timestamp,@timestampex,@moderator)");
            lock (object_0)
            {
                list_0.Add(UserId);
                dictionary_0.Add(UserId, new BanDetails(UserId, BanType, MessageText, UnixTimestamp.GetCurrent(), UnixTimestamp.GetCurrent() + Length, Moderator));
            }
        }

        public static void BanHammer(SqlDatabaseClient MySqlClient, uint UserID, string reason, string actor)
        {
            BanUser(MySqlClient, UserID, 3, reason, actor, 99999);
            ReloadCache(MySqlClient);
        }

        public static void BanHammer2(SqlDatabaseClient MySqlClient, uint UserID, string reason, string actor)
        {
            BanUser(MySqlClient, UserID, 3, reason, actor, 100);
            ReloadCache(MySqlClient);
        }

        public static BanDetails GetBanDetails(uint UserId)
        {
            if (dictionary_0.ContainsKey(UserId))
            {
                return dictionary_0[UserId];
            }
            return new BanDetails(0, 0, string.Empty, UnixTimestamp.GetCurrent(), UnixTimestamp.GetCurrent() + 1000.0, string.Empty);
        }

        public static void Initialize(SqlDatabaseClient MySqlClient)
        {
            list_0 = new List<uint>();
            list_1 = new List<string>();
            dictionary_0 = new Dictionary<uint, BanDetails>();
            object_0 = new object();
            thread_0 = new Thread(new ThreadStart(ModerationBanManager.ProcessThread));
            thread_0.Name = "ModerationBanManager";
            thread_0.Priority = ThreadPriority.Lowest;
            thread_0.Start();
            ReloadCache(MySqlClient);
        }

        public static bool IsRemoteAddressBlacklisted(string RemoteAddressString)
        {
            lock (object_0)
            {
                return list_1.Contains(RemoteAddressString);
            }
        }

        public static bool IsUserIdBlacklisted(uint UserId)
        {
            lock (object_0)
            {
                return list_0.Contains(UserId);
            }
        }

        public static void ProcessThread()
        {
            try
            {
                while (Program.Alive)
                {
                    Thread.Sleep(0x927c0);
                    using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                    {
                        ReloadCache(client);
                        continue;
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        public static void ReloadCache(SqlDatabaseClient MySqlClient)
        {
            lock (object_0)
            {
                list_0.Clear();
                list_1.Clear();
                dictionary_0.Clear();
                MySqlClient.SetParameter("timestamp", UnixTimestamp.GetCurrent());
                foreach (DataRow row in MySqlClient.ExecuteQueryTable("SELECT * FROM baneos WHERE timestampex > @timestamp OR tipo_baneo > 0").Rows)
                {
                    uint item = (uint) row["id_usuario"];
                    uint banType = (uint) row["tipo_baneo"];
                    string reason = (string) row["detalles"];
                    string moderator = (string) row["moderador"];
                    string str3 = (string) row["direccion_ip"];
                    double timestamp = (double) row["timestamp"];
                    double timestampEx = (double) row["timestampex"];
                    if ((item > 0) && !list_0.Contains(item))
                    {
                        list_0.Add(item);
                    }
                    if ((str3.Length > 0) && !list_1.Contains(str3))
                    {
                        list_1.Add(str3);
                    }
                    if ((item > 0) && !dictionary_0.ContainsKey(item))
                    {
                        dictionary_0.Add(item, new BanDetails(item, banType, reason, timestamp, timestampEx, moderator));
                    }
                }
            }
        }
    }
}

