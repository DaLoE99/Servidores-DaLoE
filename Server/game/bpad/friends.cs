using System;
using System.Collections.Generic;
using System.Text;

using Boombang.game.user;

namespace Boombang.game.bpad
{
    public class friends
    {
        public int usuario_1;
        public int usuario_2;
        public bool aceptado;
        public userInfo info_usuario_1;
        public userInfo info_usuario_2;
        public bool necesita_actualizar;

        public friends(int id_1, int id_2, int i_aceptado)
        {
            usuario_1 = id_1;
            usuario_2 = id_2;
            aceptado = (i_aceptado > 0) ? true : false;
            info_usuario_1 = Environment.Game.User.getUser(usuario_1);
            info_usuario_2 = Environment.Game.User.getUser(usuario_2);
        }

        internal userInfo friend(int id_usuario)
        {
            if (usuario_1 != id_usuario)
                return info_usuario_1;
            if (usuario_2 != id_usuario)
                return info_usuario_2;
            else
                return null;
        }

        internal string ToString(int id_usuario)
        {
            StringBuilder userData = new StringBuilder();
            userInfo nUser = friend(id_usuario);
            int es_amigo = (Environment.Game.bpad.esAmigo(id_usuario, nUser.id) == true) ? 0 : 1;
            long session = Environment.sessions.GetSessionFromUser(nUser.id);
            int esta_online = (session != -1) ? 0 : 1;
            //³²2132535³²zinzinati³²vendo keko!!!!³²5³²A17850996600A17850003A75FF0066006666FFFFFF³²15³²bcn³² ³²1³²0
            //³x³,1³,2³,Amigo³,Hola! :)³,3³,B88A5CFF99000099CC0099CCE31709FFFFFF336666³,18³,BoomBang³, ³,1³,0³
            //³²ID_USUARIO³²NOMBRE_USUARIO³²BOCADILLO_USUARIO³²TIPO_PERSON³²COLOR_PERSON_HEX³²EDAD³²CIUDAD³²NPI( )³²NPI(1)³²ACEPTADO(0) -- 0 = si 1 = peticion
            userData.Append("³²" + nUser.id + "³²" + nUser.usuario + "³²" + nUser.bocadillo + "³²" + nUser.tipo_avatar + "³²" + nUser.colores_avatar + "³²" + nUser.edad + "³²" + nUser.ciudad + "³² ³²" + esta_online + "³²" + es_amigo);
            return userData.ToString();
        }

        internal string ToString_z(int id_usuario)
        {
            StringBuilder userData = new StringBuilder();
            userInfo nUser = friend(id_usuario);

            long session = Environment.sessions.GetSessionFromUser(nUser.id);
            string area_actual = (string.IsNullOrEmpty(Environment.sessions.GetSession(session).area_actual)) ? "Flower Power" : Environment.sessions.GetSession(session).area_actual;
            int esta_online = (session != -1) ? 0 : 1;

            //±³z³²5002237³²0³²0³²0³²0³²0³²0³²-1³²2011-07-21 20:37:24³²° -> Offline
            //±³z³²2805352³²1³²1³²1³²0³²0³²1³²0³²Flower Power³²° -> Online

            if (esta_online == 0)
            {
                userData.Append(nUser.id + "³²" + 1 + "³²" + -1 + "³²" + -1 + "³²" + 0 + "³²" + 0 + "³²" + -1 + "³²" + 0 + "³²" + area_actual + "³²");
                return userData.ToString();
            }
            else
            {
                userData.Append(nUser.id + "³²" + 0 + "³²" + 0 + "³²" + 0 + "³²" + 0 + "³²" + 0 + "³²" + 0 + "³²" + -1 + "³²" + nUser.ultimo_login + "³²");
                return userData.ToString();
            }
        }
    }
}
