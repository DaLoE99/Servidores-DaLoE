namespace BoomBang.Game.Register
{
    using BoomBang;
    using BoomBang.Communication;
    using BoomBang.Communication.Incoming;
    using BoomBang.Communication.Outgoing;
    using BoomBang.Config;
    using BoomBang.Game.Misc;
    using BoomBang.Game.Sessions;
    using BoomBang.Storage;
    using System;
    using System.Security.Cryptography;

    public static class RegisterManager
    {
        /* private scope */
        static Random random_0;

        public static void Initialize()
        {
            random_0 = new Random();
            DataRouter.RegisterHandler(FlagcodesIn.USER, ItemcodesIn.REGISTER_CHECK_NAME, new ProcessRequestCallback(RegisterManager.smethod_0), true);
            DataRouter.RegisterHandler(FlagcodesIn.USER, ItemcodesIn.LANDING_REGISTER, new ProcessRequestCallback(RegisterManager.smethod_2), true);
        }

        private static void smethod_0(Session session_0, ClientMessage clientMessage_0)
        {
            string str = clientMessage_0.ReadString();
            if (smethod_1(str).Equals((uint)0))
            {
                session_0.SendData(CheckUsernameComposer.Compose(false, null), false);
            }
            else
            {
                string[] randomUsername = new string[] { str + random_0.Next(100, 0x3e8), str + random_0.Next(0x7d0, 0xbb8), str + random_0.Next(0xbb8, 0xfa0), str + random_0.Next(0xfa0, 0x1388) };
                session_0.SendData(CheckUsernameComposer.Compose(true, randomUsername), false);
            }
        }

        private static uint smethod_1(string string_0)
        {
            if (WordFilterManager.ModeratorNames.Contains(string_0.ToLower()))
            {
                return 1;
            }
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                client.SetParameter("Username", string_0);
                if (client.ExecuteQueryRow("SELECT id FROM usuarios WHERE usuario = @Username LIMIT 1") != null)
                {
                    return 1;
                }
            }
            return 0;
        }

        private static void smethod_2(Session session_0, ClientMessage clientMessage_0)
        {
            string str = clientMessage_0.ReadString();
            string s = clientMessage_0.ReadString();
            uint num = clientMessage_0.ReadUnsignedInteger();
            string str3 = clientMessage_0.ReadString();
            int num2 = clientMessage_0.ReadInteger();
            string str4 = clientMessage_0.ReadString();
            string str5 = string.Empty;
            string str6 = string.Empty;
            if ((((str.Length >= 1) && (s.Length >= 4)) && ((num2 >= 0) || (num2 <= 0x41))) && (str4.Length >= 4))
            {
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    client.SetParameter("Username", str);
                    client.SetParameter("Password", str5);
                    client.SetParameter("Avatar", num);
                    client.SetParameter("Colors", str3);
                    client.SetParameter("Age", num2);
                    client.SetParameter("Email", str4);
                    client.SetParameter("CurrentTimestamp", UnixTimestamp.GetCurrent());
                    client.SetParameter("RemoteAddress", session_0.RemoteAddress);
                    client.SetParameter("Null", 0);
                    str6 = client.ExecuteScalar("INSERT INTO usuarios (`usuario`, `password`, `email`, `edad`, `tiempo_registrado`, `ip_registro`,  `ip_actual`, `ultimo_login`, `tipo_avatar`, `colores_avatar`, `timestamp_monedas`) VALUES (@Username, @Password, @Email, @Age, @Null, @RemoteAddress, @RemoteAddress, @CurrentTimestamp, @Avatar, @Colors, @CurrentTimestamp); SELECT LAST_INSERT_ID();").ToString();
                    client.SetParameter("Id", (str6 == string.Empty) ? ((object)0) : ((object)uint.Parse(str6)));
                    client.ExecuteNonQuery("INSERT INTO rankings (`usuario`) VALUES (@Id)");
                }
                session_0.TryAuthenticate(str, str5, session_0.RemoteAddress, true);
            }
        }
    }
}

