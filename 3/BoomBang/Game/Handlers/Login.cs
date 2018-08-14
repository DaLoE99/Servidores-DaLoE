using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Communication.Incoming;
using Snowlight.Communication;
using Snowlight.Game.Sessions;
using Snowlight.Communication.Outgoing;
using Snowlight.Storage;
using System.Text.RegularExpressions;
using System.Threading;

namespace Snowlight.Game.Handlers
{
    class Login
    {
        public static void Initialize()
        {
            DataRouter.RegisterHandler(Opcodes.RFACE, new ProcessRequestCallback(InitLogin), true);
            DataRouter.RegisterHandler(Opcodes.REGISTERCHECKUSER, new ProcessRequestCallback(CheckUsername), true);
            DataRouter.RegisterHandler(Opcodes.REGISTER, new ProcessRequestCallback(Register), true);
            DataRouter.RegisterHandler(Opcodes.LOGIN, new ProcessRequestCallback(Loginn), true);
        }

        private static void InitLogin(Session Session, ClientMessage Message)
        {
            if (Session.Authenticated)
            {
                return;
            }

            Session.SendData(InitLoginComposer.Compose());
        }
        private static void CheckUsername(Session Session, ClientMessage Message)
        {
            string[] GetParameter = Regex.Split(Message.ToString(), "³²");
            string User = GetParameter[1];
            if (Session.Authenticated)
            {
                return;
            }
            if (CU(User) == true)
            {
                Session.SendData(CheckUsernameComposer.ComposeFalse(User));
            }
            else
            {
                Session.SendData(CheckUsernameComposer.ComposeTrue());
            }
        }
        private static void Register(Session Session, ClientMessage Message)
        {
            if (Session.Authenticated)
            {
                return;
            }
            //±x³ƒ³²Test23432³²contrasea2³²11³²FFCC669933001A0000FF0000009999996666000000³²15³²asd@hotmail.com³²³²³²³²°
            string[] GetParameter = Regex.Split(Message.ToString(), "³²");
            string User = GetParameter[1];
            string Password = GetParameter[2];
            int Avatar = int.Parse(GetParameter[3]);
            string Colors = GetParameter[4];
            string Age = GetParameter[5];
            string Email = GetParameter[6];
            string str5 = string.Empty;
            string str6 = string.Empty;

            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                client.SetParameter("Username", User);
                client.SetParameter("Password", Password);
                client.SetParameter("Avatar", Avatar);
                client.SetParameter("Colors", Colors);
                client.SetParameter("Age", Age);
                client.SetParameter("Email", Email);
                client.SetParameter("CurrentTimestamp", UnixTimestamp.GetCurrent());
                client.SetParameter("RemoteAddress", Session.RemoteAddress);
                client.SetParameter("Null", 0);
                str6 = client.ExecuteScalar("INSERT INTO usuarios (`usuario`, `password`, `email`, `edad`, `tiempo_registrado`, `ip_registro`,  `ip_actual`, `ultimo_login`, `tipo_avatar`, `colores_avatar`, `timestamp_monedas`) VALUES (@Username, @Password, @Email, @Age, @Null, @RemoteAddress, @RemoteAddress, @CurrentTimestamp, @Avatar, @Colors, @CurrentTimestamp); SELECT LAST_INSERT_ID();").ToString();
                client.SetParameter("Id", (str6 == string.Empty) ? ((object)0) : ((object)uint.Parse(str6)));
            }

            Session.SendData(RegisterComposer.Compose());
        }

        private static void Loginn(Session Session, ClientMessage Message)
        {
            //±x³y³²Test23432³²contrasea2³²°
            string[] GetParameter = Regex.Split(Message.ToString(), "³²");
            string User = GetParameter[1];
            string Password = GetParameter[2];

            Session.TryAuthenticate(User, Password, Session.RemoteAddress, false);
        }

        private static bool CU(string usuario)
        {
            using (SqlDatabaseClient MySqlClient = SqlDatabaseManager.GetClient())
            {
                MySqlClient.SetParameter("@usuario", usuario);
                int result = MySqlClient.ReadInt32("SELECT COUNT(*) FROM usuarios WHERE usuario = @usuario");
                MySqlClient.ClearParameters();
                return (result > 0) ? true : false;
            }
        }
    }
}
