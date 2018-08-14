using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Storage;
using System.Data;

namespace Snowlight.Game.Sessions
{
    class UserCredentialsAuthenticator
    {
        static int int_0;
        static int int_1;
        static object object_0;

        public static void Initialize()
        {
            int_0 = 0;
            int_1 = 0;
            object_0 = new object();
        }

        private static void UpdateUser(SqlDatabaseClient sqlDatabaseClient_0, uint uint_0, string string_0)
        {
            sqlDatabaseClient_0.SetParameter("id", uint_0);
            sqlDatabaseClient_0.SetParameter("lastip", string_0);
            sqlDatabaseClient_0.SetParameter("lastonline", UnixTimestamp.GetCurrent());
            sqlDatabaseClient_0.ExecuteNonQuery("UPDATE usuarios SET ip_actual = @lastip, ultimo_login = @lastonline WHERE id = @id LIMIT 1");
        }

        public static uint TryAuthenticate(SqlDatabaseClient MySqlClient, string Username, string Password, string RemoteAddress)
        {
            try
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
                    string query = "SELECT id FROM usuarios WHERE usuario = '" + Username + "' AND password = '" + Password + "' LIMIT 1";
                    DataRow row = MySqlClient.ExecuteQueryRow(query);
                    if (row != null)
                    {
                        num = uint.Parse(row["id"].ToString());
                        UpdateUser(MySqlClient, num, RemoteAddress);
                    }
                    if (num <= 0)
                    {
                        int_1++;
                        return 0;
                    }
                    if (SessionManager.ContainsCharacterId(num))
                    {
                        SessionManager.StopSession(SessionManager.GetSessionByCharacterId(num).Id);
                    }
                    Output.WriteLine(string.Concat(new object[] { "User ", Username, " (ID ", num, ") has logged in from ", RemoteAddress, "." }));
                    int_0++;
                    return num;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return 0;
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
