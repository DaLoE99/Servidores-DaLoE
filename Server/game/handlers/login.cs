using System;

using Boombang.sockets.messages;
using Boombang.game.session;

namespace Boombang.game.handlers
{
    public partial class login : Handler
    {
        public void Handler120_type_130() //Login
        {
            //SND: ±x³³²0³²° -> Login erroneo
            //SND: ±x³³²1³²° -> Login correcto

            string usuario = Message.getParameter();
            string password = Message.getParameter();

            if (!(string.IsNullOrEmpty(usuario)) && !(string.IsNullOrEmpty(password)))
            {
                if (Environment.Game.User.login(usuario, password) == true)
                {
                    server message = new server("x");
                    message.Append("³³²1³²");
                    SendMessage(message);
                    //Console.WriteLine("[Debug] Login válido.");

                    mUser = Environment.Game.User.getUserByName(usuario);

                    mUser.ip_actual = Environment.connections.GetConnection(Convert.ToInt32(mSessionID.ToString())).GetIP();
                    mUser.ultimo_login = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    Environment.Game.User.update_criticaldata(mUser);

                    int es_moderador = (mUser.es_moderador == true) ? 1 : 0;

                    server message2 = new server("x");
                    //±x³³²1³²seitaridis³²3³²D7AC90CFE4FFCFE4FFFFFAF7FFFFFFFFFFFFC0FF77³²elcrashplash@hotmail.es³²13³²10³²Grecia³²1³²3655792³²0³²525³²4964³²200³²5³²0³²1607³²1³2³3³4³5³23³81³89³90³91³92³93³94³96³97³98³99³100³101³102³103³104³105³110³153³154³155³163³165³174³175³176³181³183³²4³37³14³4³2³130³52³18³22³21³25³20³18³43³54³40³54³51³43³36³50³45³49³63³3³1³1³1³1³1³1³1³2³1³²0³²ES³²6³²1³²1³²28/07/2011³²1³²1³²2206³²0³²1³0³²0³²0³²°
                    message2.Append("³³²1³²" + mUser.usuario + "³²" + mUser.tipo_avatar + "³²" + mUser.colores_avatar + "³²" + mUser.email + "³²" + mUser.edad + "³²" + 10 + "³²" + mUser.bocadillo + "³²0³²" + mUser.id + "³²" + es_moderador + "³²" + mUser.creditos_oro + "³²" + mUser.creditos_plata + "³²200³²5³²0³²-1³²³²³²1³²ES³²0³²0³²0³²³²0³²0³²0³²0³²1³0³²0³²0³²");
                    SendMessage(message2);
                    Environment.sessions.GetSession(mSessionID).SessionAuthenticated(mUser);
                }
                else
                {
                    server message = new server("x");
                    message.Append("³³²0³²");
                    SendMessage(message);
                    //Console.WriteLine("[Debug] Login incorrecto.");
                }
            }
        }

        public void Handler120_type_131() //Completo registro
        {
            string usuario = Message.getParameter();
            string password = Message.getParameter();
            int tipo_avatar = Convert.ToInt32(Message.getParameter());
            string color_avatar = Message.getParameter();
            int edad = Convert.ToInt32(Message.getParameter());
            string email = Message.getParameter();
            string ip = Environment.connections.GetConnection(Convert.ToInt32(mSessionID.ToString())).GetIP();

            if (!(string.IsNullOrEmpty(usuario)) && !(string.IsNullOrEmpty(password)) && !(string.IsNullOrEmpty(email)) && !(string.IsNullOrEmpty(ip)))
            {
                Environment.Game.User.register(usuario, password, tipo_avatar, color_avatar, edad, email, ip);

                mUser = Environment.Game.User.getUserByName(usuario);

                server message = new server("x");
                message.Append("³³²1³²" + mUser.usuario + "³²" + mUser.tipo_avatar + "³²" + mUser.colores_avatar + "³²" + mUser.email + "³²" + mUser.edad + "³²2" + "³²" + mUser.bocadillo + "³²0³²" + mUser.id + "³²0³²" + mUser.creditos_oro + "³²" + mUser.creditos_plata + "³²200³²5³²0³²-1³²³²³²1³²ES³²0³²0³²0³²³²0³²0³²0³²0³²1³0³²0³²0³²");
                SendMessage(message);
                Environment.sessions.GetSession(mSessionID).SessionAuthenticated(mUser);
            }
        }

        public void Handler120_type_132()
        {
            string colores = Message.getParameter();
            int id_avatar = int.Parse(Message.getParameter());

            mUser.colores_avatar = colores;
            mUser.tipo_avatar = id_avatar;

            Environment.Game.User.update_look(mUser);
        }

        public void Handler120_type_139() //Check nombre de usuario, registro.
        {
            string usuario = Message.getParameter();

            if (!(string.IsNullOrEmpty(usuario)))
            {

                if (Environment.Game.User.checkUsername(usuario) == true)
                {
                    server sMessage = new server("x");
                    sMessage.Append("³³²1³²10110³²Jairo34³²GranPC³²Solerini³²");
                    SendMessage(sMessage);
                    //Console.WriteLine("[DEBUG] Nombre no disponible.");
                }
                else
                {
                    server sMessage = new server("x");
                    sMessage.Append("³³²2³²");
                    SendMessage(sMessage);
                    //Console.WriteLine("[DEBUG] Nombre disponible.");
                }
            }
        }

        public void Handler120_type_145() //Inicio login
        {
            server message = new server("x");
            message.Append("³³x³²1³²135271416547234³²http://esp.mus.boombang.tv/facebook/connect.php³²user_birthday³²2GZZM5VLsQ+jmHE5bWa4QA==³²");
            SendMessage(message);
            //Console.WriteLine("[DEBUG] Login iniciado."); 
        }
    }
}
