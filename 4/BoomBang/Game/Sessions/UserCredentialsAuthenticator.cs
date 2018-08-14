namespace BoomBang.Game.Sessions
{
    using BoomBang;
    using BoomBang.Storage;
    using System;
    using System.Data;

    public static class UserCredentialsAuthenticator
    {
        /* private scope */
        static int int_0;
        /* private scope */
        static int int_1;
        /* private scope */
        static object object_0;

        public static void Initialize()
        {
            int_0 = 0;
            int_1 = 0;
            object_0 = new object();
        }

        private static void smethod_0(SqlDatabaseClient sqlDatabaseClient_0, uint uint_0, string string_0)
        {
            sqlDatabaseClient_0.SetParameter("id", uint_0);
            sqlDatabaseClient_0.SetParameter("lastip", string_0);
            sqlDatabaseClient_0.SetParameter("lastonline", UnixTimestamp.GetCurrent());
            sqlDatabaseClient_0.ExecuteNonQuery("UPDATE usuarios SET ip_actual = @lastip, ultimo_login = @lastonline WHERE id = @id LIMIT 1");
        }

        public static uint TryAuthenticate(SqlDatabaseClient MySqlClient, string Username, string Password, string RemoteAddress)
        {
            lock (object_0)
            {
                if (Password.Length < 4)
                {
                    int_1++;
                    return 0;
                }
                uint num = 0;
                MySqlClient.SetParameter("Username", Username);
                MySqlClient.SetParameter("Password", Password);
                DataRow row = MySqlClient.ExecuteQueryRow("SELECT id FROM usuarios WHERE usuario = @Username AND Password = @Password LIMIT 1");
                if (row != null)
                {
                    num = (uint)row["id"];
                    smethod_0(MySqlClient, num, RemoteAddress);
                }
                if (num <= 0)
                {
                    int_1++;
                    return 0;
                }
                if (SessionManager.ContainsCharacterId(num))
                {
                    SessionManager.StopSession(SessionManager.GetSessionByCharacterId(num).UInt32_0);
                }
                Output.WriteLine(string.Concat(new object[] { "User ", Username, " (ID ", num, ") has logged in from ", RemoteAddress, "." }));
                int_0++;
                return num;
            }
        }

        public static int FailedLoginCount
        {
            get
            {
                return int_1;
            }
        }

        public static int SuccessfulLoginCount
        {
            get
            {
                return int_0;
            }
        }

        public static int TotalLoginCount
        {
            get
            {
                return (int_0 + int_1);
            }
        }
    }
}

