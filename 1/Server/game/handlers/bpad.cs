using System;
using System.Collections.Generic;

using Boombang.game.bpad;
using Boombang.game.user;
using Boombang.game.session;
using Boombang.sockets.messages;

namespace Boombang.game.handlers
{
    partial class bPad : Handler
    {
        public void Handler132_type_120()
        {
            List<friends> list_amigos = Environment.Game.bpad.amigos(mUser.id);
            List<friends> list_peticiones = Environment.Game.bpad.peticiones(mUser.id);

            //±³x³²3³²2132535³²zinzinati³²vendo keko!!!!³²5³²A17850996600A17850003A75FF0066006666FFFFFF³²15³²bcn³² ³²1³²0³²4866787³²ismaaell
            //³²soy el jefe de mi clan moredadores ³²6³²F5B87AFF9900F2F7FFFFB23D000000000000000000³²15³²boombang³²³²1³²0³²5002237³²Identity1
            //³²Hola³²1³²FFD797CC5806FFFFFF6633000066CCFFFFFF000000³²1³²Boombang³²³²1³²1³²°

            server message = new server("");
            message.Append("³x³²" + (list_amigos.Count + list_peticiones.Count));

            foreach (friends amigo in list_amigos)
            {
                message.Append(amigo.ToString(mUser.id));
            }
            foreach (friends no_amigo in list_peticiones)
            {
                message.Append(no_amigo.ToString(mUser.id));
            }
            message.Append("³²");
            SendMessage(message);
        }
        public void Handler132_type_122()
        {
            //Sin amigos
            //±³z³²°
            List<friends> list_amigos = Environment.Game.bpad.amigos(mUser.id);

            server message = new server("³z³²");
            foreach (friends amigo in list_amigos)
            {
                message.Append(amigo.ToString_z(mUser.id));
            }
            SendMessage(message);
        }

        public void Handler132_type_123()
        {
            int id_amigo = int.Parse(Message.getParameter());
            long session = Environment.sessions.GetSessionFromUser(id_amigo);

            Environment.Game.bpad.agregar_amigo(mUser.id, id_amigo);

            if(session != -1)
                Environment.sessions.GetSession(session).SendMessage("³{³²" + mUser.id + "³²" + mUser.usuario + "³²" + mUser.bocadillo + "³²" + mUser.tipo_avatar + "³²" + mUser.colores_avatar + "³²" + mUser.edad + "³²" + mUser.ciudad + "³²³²1³²1³²");
        }

        public void Handler132_type_124()
        {
            //³³,1³,5208175³ -> Al que acepta la peticion
            //³|³,ID³,NOMBRE³,BOCATA³,PERSON³,COLOR_HEX³,AGE³,CIUDAD³,³,1³,ONLINE³,³ -> Al que ha mandado la peticion
            //³³,-2³,322268³ -> Lleno

            int id_amigo = int.Parse(Message.getParameter());
            friends amigos = new friends(id_amigo, mUser.id, 0);
            Environment.Game.bpad.aceptar_amigo(mUser.id, id_amigo);
            long session = Environment.sessions.GetSessionFromUser(id_amigo);
            
            if (session != -1)
                Environment.sessions.GetSession(session).SendMessage("³|³²" + mUser.id + "³²" + mUser.usuario + "³²" + mUser.bocadillo + "³²" + mUser.tipo_avatar + "³²" + mUser.colores_avatar + "³²" + mUser.edad + "³²" + mUser.ciudad + "³²³²1³²0³²³²");
            
            userInfo nUser = Environment.Game.User.getUser(id_amigo);

            server message = new server("³³²1");
            message.Append("³²" + nUser.id + "³²");
            SendMessage(message);
        }

        public void Handler132_type_125()
        {
            int id_borrado = int.Parse(Message.getParameter());

            Console.WriteLine("[DEBUG] ID borrado: " + id_borrado);
            long session = Environment.sessions.GetSessionFromUser(id_borrado);
            Environment.Game.bpad.borrar_amigo(mUser.id, id_borrado);
            server message = new server("³}³²" + id_borrado+ "³²");
            SendMessage(message);

            if(session != -1)
                Environment.sessions.GetSession(session).SendMessage("³}³²" + mUser.id + "³²");
            //³}³,5208175³ -> YO
            //³}³,2805352³ -> EL


        }

        public void Handler132_type_127()
        {
            Random id_mensaje = new Random();
            int id_destino = int.Parse(Message.getParameter());
            int tipo_mensaje = (mUser.es_moderador == true) ? 1 : 0;
            string mensaje = Message.getParameter();

            long session = Environment.sessions.GetSessionFromUser(id_destino);

            if (!Environment.Game.bpad.esAmigo(mUser.id, id_destino))
            {
                return;
            }


            //³³,919522148³,5208175³,07/22/11 18:29³,Cuerpo del mensaje³,0³
            if (session != -1)
            {
                server message = new server("³³²");
                message.Append(id_mensaje.Next(800000, 9000000) + "³²" + mUser.id + "³²" + System.DateTime.Now + "³²" + mensaje + "³²" + tipo_mensaje + "³²");
                Environment.sessions.GetSession(session).SendMessage(message);
            }
        }

        public void Handler132_type_129()
        {
            //³³,1³,4815³,jairo³,3³,B88A5CFF99000099CC0099CCE31709FFFFFF336666³,-2³,-2³,-2³,-2³,-2³,-2³,0000-00-00 00:00:00³,28³,BoomBang³ -> Offline
            //³³,1³,2805³,jairo³,3³,090D66FCF30E15E7FFFFF91A14F7FFFFFFFF36FC0F³,-1³,-1³,0³,0³,-1³,0³,Flower Power³,16³,BoomBang³ -> Online
            //³³,1³,2135³,zinzi³,5³,A17850996600A17850003A75FF0066006666FFFFFF³,-2³,-2³,-2³,-2³,-2³,-2³,2011-07-18 15:03:38³,15³,bcn³ -> Ya amigo
            string nombre_busqueda = Message.getParameter();

            List<userInfo> nUser_list = Environment.Game.bpad.buscar_usuario(nombre_busqueda);
            List<userInfo> es_amigo = new List<userInfo>();
            List<userInfo> es_desconocido = new List<userInfo>();

            foreach (userInfo nUser in nUser_list)
            {
                if (Environment.Game.bpad.esAmigo(mUser.id, nUser.id))
                    es_amigo.Add(nUser);
                else
                    es_desconocido.Add(nUser);
            }

            server message = new server("³³²");
            message.Append(es_desconocido.Count + es_amigo.Count);

            foreach (userInfo nUser in es_amigo)
            {
                long session = Environment.sessions.GetSessionFromUser(nUser.id);
                string area_actual = (string.IsNullOrEmpty(Environment.sessions.GetSession(session).area_actual)) ? "Flower Power" : Environment.sessions.GetSession(session).area_actual;

                if(session != -1)
                    message.Append("³²" + nUser.id + "³²" + nUser.usuario + "³²" + nUser.tipo_avatar + "³²" + nUser.colores_avatar + "³²" + -1 + "³²" + -1 + "³²" + 0 + "³²" + 0 + "³²" + -1 + "³²" + 0 + "³²" + area_actual + "³²" + nUser.edad + "³²" + nUser.ciudad);
                else
                    message.Append("³²" + nUser.id + "³²" + nUser.usuario + "³²" + nUser.tipo_avatar + "³²" + nUser.colores_avatar + "³²" + -2 + "³²" + -2 + "³²" + -2 + "³²" + -2 + "³²" + -2 + "³²" + -2 + "³²" + nUser.ultimo_login + "³²" + nUser.edad + "³²" + nUser.ciudad);
            }

            foreach (userInfo nUser in es_desconocido)
            {
                long session = Environment.sessions.GetSessionFromUser(nUser.id);
                string area_actual = (string.IsNullOrEmpty(Environment.sessions.GetSession(session).area_actual)) ? "Flower Power" : Environment.sessions.GetSession(session).area_actual;

                if (session != -1)
                    message.Append("³²" + nUser.id + "³²" + nUser.usuario + "³²" + nUser.tipo_avatar + "³²" + nUser.colores_avatar + "³²" + -1 + "³²" + -1 + "³²" + 0 + "³²" + 0 + "³²" + -1 + "³²" + 0 + "³²" + area_actual + "³²" + nUser.edad + "³²" + nUser.ciudad);
                else
                    message.Append("³²" + nUser.id + "³²" + nUser.usuario + "³²" + nUser.tipo_avatar + "³²" + nUser.colores_avatar + "³²" + -2 + "³²" + -2 + "³²" + -2 + "³²" + -2 + "³²" + -2 + "³²" + -2 + "³²" + nUser.ultimo_login + "³²" + nUser.edad + "³²" + nUser.ciudad);

            }

            message.Append("³²");
            SendMessage(message);

        }

        public void Handler132_type_130()
        {
            //³}³²2805352³²
            int id_amigo = int.Parse(Message.getParameter());
            Environment.Game.bpad.borrar_amigo(mUser.id, id_amigo);

        }
    }
}
