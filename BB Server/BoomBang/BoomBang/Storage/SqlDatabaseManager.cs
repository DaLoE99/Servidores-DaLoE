namespace BoomBang.Storage
{
    using BoomBang;
    using BoomBang.Config;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading;

    public static class SqlDatabaseManager
    {
        internal static Dictionary<int, SqlDatabaseClient> dictionary_0;
        /* private scope */ static int int_0;
        /* private scope */ static int int_1;
        /* private scope */ static int int_2;
        /* private scope */ static int int_3;
        /* private scope */ static int int_4;
        /* private scope */ static object object_0;

        public static string GenerateConnectionString()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder {
                Server = (string) ConfigManager.GetValue("mysql.host"),
                Port = (uint) ((int) ConfigManager.GetValue("mysql.port")),
                UserID = (string) ConfigManager.GetValue("mysql.user"),
                Password = (string) ConfigManager.GetValue("mysql.pass"),
                Database = (string) ConfigManager.GetValue("mysql.dbname"),
                MinimumPoolSize = (uint) ((int) ConfigManager.GetValue("mysql.pool.min")),
                MaximumPoolSize = (uint) ((int) ConfigManager.GetValue("mysql.pool.max"))
            };
            return builder.ToString();
        }

        public static SqlDatabaseClient GetClient()
        {
            lock (object_0)
            {
                using (Dictionary<int, SqlDatabaseClient>.ValueCollection.Enumerator enumerator = dictionary_0.Values.GetEnumerator())
                {
                    SqlDatabaseClient current;
                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        if (current.Available)
                        {
                            goto Label_003F;
                        }
                    }
                    goto Label_0079;
                Label_003F:
                    Output.WriteLine("(Sql) Assigned client " + current.Int32_0 + ".", OutputLevel.DebugInformation);
                    current.Available = false;
                    return current;
                }
            Label_0079:
                if ((int_2 <= 0) || (ClientCount < int_2))
                {
                    SetClientAmount(ClientCount + 1, "out of assignable clients in GetClient()");
                    return GetClient();
                }
            }
            int_0++;
            Output.WriteLine("(Sql) Client starvation; out of assignable clients/maximum pool size reached. Consider increasing the `mysql.pool.max` configuration value. Starvation count is " + int_0 + ".", OutputLevel.Warning);
            Thread.Sleep(100);
            return GetClient();
        }

        public static void Initialize()
        {
            dictionary_0 = new Dictionary<int, SqlDatabaseClient>();
            int_1 = (int) ConfigManager.GetValue("mysql.pool.min");
            int_2 = (int) ConfigManager.GetValue("mysql.pool.max");
            int_3 = (int) ConfigManager.GetValue("mysql.pool.lifetime");
            object_0 = new object();
            new Thread(new ThreadStart(SqlDatabaseManager.ProcessMonitorThread)) { Priority = ThreadPriority.Lowest, Name = "SqlMonitor" }.Start();
            if (int_1 < 0)
            {
                throw new ArgumentException("(Sql) Invalid database pool size configured (less than zero).");
            }
            SetClientAmount(int_1, "server init");
        }

        public static void ProcessMonitorThread()
        {
            while (Program.Alive)
            {
                if (ClientCount > int_1)
                {
                    lock (object_0)
                    {
                        List<int> list = new List<int>();
                        foreach (SqlDatabaseClient client in dictionary_0.Values)
                        {
                            if (client.Available && (client.TimeInactive >= int_3))
                            {
                                list.Add(client.Int32_0);
                            }
                        }
                        foreach (int num in list)
                        {
                            dictionary_0[num].Close();
                            dictionary_0.Remove(num);
                        }
                        if (list.Count > 0)
                        {
                            Output.WriteLine("(Sql) Disconnected " + list.Count + " inactive client(s).", OutputLevel.DebugInformation);
                        }
                    }
                }
                Thread.Sleep((int) (int_3 / 2));
            }
        }

        public static void SetClientAmount(int ClientAmount, string LogReason = "Unknown")
        {
            int num;
            lock (object_0)
            {
                num = ClientAmount - ClientCount;
                if (num > 0)
                {
                    for (int i = 0; i < num; i++)
                    {
                        int key = smethod_0();
                        dictionary_0.Add(key, smethod_1(key));
                    }
                }
                else
                {
                    int num4 = -num;
                    int num5 = 0;
                    foreach (SqlDatabaseClient client in dictionary_0.Values)
                    {
                        if (client.Available)
                        {
                            if ((num5 >= num4) || (ClientCount <= int_1))
                            {
                                goto Label_00C6;
                            }
                            client.Close();
                            dictionary_0.Remove(client.Int32_0);
                            num5++;
                        }
                    }
                }
            }
        Label_00C6:;
            Output.WriteLine(string.Concat(new object[] { "(Sql) Client availability: ", ClientAmount, "; modifier: ", num, "; reason: ", LogReason, "." }), OutputLevel.DebugInformation);
        }

        private static int smethod_0()
        {
            lock (object_0)
            {
                return int_4++;
            }
        }

        private static SqlDatabaseClient smethod_1(int int_5)
        {
            MySqlConnection connection = new MySqlConnection(GenerateConnectionString());
            connection.Open();
            return new SqlDatabaseClient(int_5, connection);
        }

        public static void Uninitialize()
        {
            int num = 0;
            while (dictionary_0.Count > 0)
            {
                lock (object_0)
                {
                    List<int> list = new List<int>();
                    foreach (SqlDatabaseClient client in dictionary_0.Values)
                    {
                        if (client.Available || (num > 15))
                        {
                            list.Add(client.Int32_0);
                        }
                    }
                    foreach (int num2 in list)
                    {
                        dictionary_0[num2].Close();
                        dictionary_0.Remove(num2);
                    }
                }
                if (dictionary_0.Count > 0)
                {
                    Output.WriteLine("(Sql) Waiting for all database clients to release (" + ++num + ")...", OutputLevel.DebugInformation);
                    Thread.Sleep(100);
                }
            }
        }

        public static int ClientCount
        {
            get
            {
                return dictionary_0.Count;
            }
        }

        public static Dictionary<int, SqlDatabaseClient> Clients
        {
            get
            {
                return dictionary_0;
            }
            set
            {
                lock (object_0)
                {
                    dictionary_0 = value;
                }
            }
        }
    }
}

