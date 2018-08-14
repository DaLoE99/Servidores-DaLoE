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
        static Random random_0;

        public static void Initialize()
        {
            random_0 = new Random();
            DataRouter.RegisterHandler(120, 139, new ProcessRequestCallback(RegisterManager.smethod_0), true);
            DataRouter.RegisterHandler(120, 131, new ProcessRequestCallback(RegisterManager.smethod_2), true);
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
                string[] randomUsername = new string[] { str + random_0.Next(100, 1000), str + random_0.Next(2000, 3000), str + random_0.Next(3000, 4000), str + random_0.Next(4000, 5000) };
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
            if ((((str.Length >= 1) && (s.Length >= 4)) && ((num2 >= 0) || (num2 <= 65))) && (str4.Length >= 4))
            {
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    client.SetParameter("Username", str);
                    client.SetParameter("Password", s);
                    client.SetParameter("Avatar", num);
                    client.SetParameter("Colors", str3);
                    client.SetParameter("Age", num2);
                    client.SetParameter("Email", str4);
                    client.SetParameter("CurrentTimestamp", UnixTimestamp.GetCurrent());
                    client.SetParameter("CurrentTimestamp1", UnixTimestamp.GetCurrent());
                    client.SetParameter("RemoteAddress", session_0.RemoteAddress);
                    client.SetParameter("RemoteAddress1", session_0.RemoteAddress);
                    client.SetParameter("Null", 0);
                    str6 = client.ExecuteScalar("INSERT INTO usuarios (`usuario`, `password`, `email`, `edad`, `tiempo_registrado`, `ip_registro`,  `ip_actual`, `ultimo_login`, `tipo_avatar`, `colores_avatar`, `timestamp_monedas`) VALUES (@Username, @Password, @Email, @Age, @Null, @RemoteAddress, @RemoteAddress, @CurrentTimestamp, @Avatar, @Colors, @CurrentTimestamp); SELECT LAST_INSERT_ID();").ToString();
                }
                session_0.TryAuthenticate(str, s, session_0.RemoteAddress, true);
            }
        }
    }
}

/*[Server] [120/139] > ±x³‹³²3rgtf35tgf³²°
[Client] [120/139] > ±x³‹³²2³²°
[Server] [120/139] > ±x³‹³²daloe³²°
[Client] [120/139] > ±x³‹³²1³²daloe1091³²daloe2921³²daloe3202³²daloe3323³²daloe3808³²daloe4632³²daloe4858³²daloe5203³²daloe5352³²daloe6821³²daloe7268³²daloe7771³²daloe7792³²daloe8804³²daloe9501³²°

[Server] [163/0] > ±£³²°
[Client] [163/0] > ±£³²20³²°

[Server] [120/139] > ±x³‹³²daloe223³²°
[Client] [120/139] > ±x³‹³²2³²°
[Server] [120/131] > ±x³ƒ³²daloe223³²a@ee.ii³²1³²FFD797CC5806FFFFFF6633000066CCFFFFFF000000³²33³²a@ee.ii³²³²³²³²°
[Client] [120/131] > ±x³ƒ³²1³²°
[Server] [120/121] > ±x³y³²daloe223³²a@ee.ii³²°
[Client] [120/121] > ±x³y³²1³²daloe223³²1³²FFD797CC5806FFFFFF6633000066CCFFFFFF000000³²a@ee.ii³²33³²2³²Boombang³²0³²7196398³²0³²0³²200³²200³²5³²0³²-1³²³²³²1³²ES³²0³²0³²0³²³²0³²0³²0³²0³²1³0³²0³²0³²°
[Server] [163/0] > ±£³²°
[Client] [163/0] > ±£³²20³²°
*/