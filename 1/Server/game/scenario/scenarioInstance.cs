using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Boombang.game.user;
using Boombang.game.handlers;
using Boombang.sockets.messages;
using Boombang.utils;

namespace Boombang.game.scenario
{
    public partial class scenarioInstance
    {
        private int id_area;

        private dataScenario mDataScenario;
        private managerScenario mManagerScenario;
        private scenarioInteractor mScenarioInteractor;

        public Dictionary<long, scenarioUser> usuarios_enArea;

        private Dictionary<long, int> sesiones_usuarios;

        public scenarioUser nuevo_usuario;
        public List<int> id_usuarios;

        public scenarioInstance(int idArea, bool es_publica)
        {
            id_area = idArea;
            mManagerScenario = new managerScenario();
            if (es_publica) mDataScenario = mManagerScenario.area(idArea);
            usuarios_enArea = new Dictionary<long, scenarioUser>();
            sesiones_usuarios = new Dictionary<long, int>();
            id_usuarios = new List<int>();
            nuevo_usuario = new scenarioUser();
            mScenarioInteractor = new scenarioInteractor(idArea, this);
        }

        public void enviar_atodoelarea(server message)
        {
            foreach (int session_id in sesiones_usuarios.Keys)
            {
                Environment.sessions.GetSession(session_id).SendMessage(message);
            }
        }

        public void enviar_atodoelareamenosami(server message, long mi_session_id)
        {
            foreach (int session_id in sesiones_usuarios.Keys)
            {
                if(session_id != mi_session_id)
                Environment.sessions.GetSession(session_id).SendMessage(message);
            }
        }

        private int asignar_id() { int i = 0; while (true) { if (!id_usuarios.Contains(i)) { id_usuarios.Add(i); return i; } i++; } }

        public void hablar_atodalasala(string mensaje, scenarioUser sUser, int id_color)
        {
            foreach (int session_id in sesiones_usuarios.Keys)
            {
                if (usuarios_enArea.ContainsKey(session_id))
                {
                    server message = new server(Convert.ToChar(133) + "³²");
                    message.Append(sUser.userid_en_escenario + "³²" + mensaje + "³²" + id_color + "³²");
                    Environment.sessions.GetSession(session_id).SendMessage(message);
                }
            }
        }

        public void publicar_usuario(long session_id)
        {
            server message = new server("³z³²");
            message.Append(construir_usuario(session_id));
            enviar_atodoelareamenosami(message, session_id);
            //publicar_statususuario(session_id, true);
        }

        public void publicar_todoslosusuarios(long session_id)
        {
            server message = new server("³y³x³²0³²");
            foreach(long sessionid in sesiones_usuarios.Keys) message.Append(construir_usuario(sessionid));
            Environment.sessions.GetSession(session_id).SendMessage(message);
        }

        public bool añadir_usuario(long session_id, bool area_publica)
        {
            areas Handler = cargar_handler(area_publica);
            int Listener = Environment.sessions.GetSession(session_id).AddListener(Handler.GetType(), Handler);
            sesiones_usuarios.Add(session_id, Listener);
            Environment.sessions.GetSession(session_id).area_actual = Environment.Game.areas.areas[id_area].nombre;
            Environment.sessions.GetSession(session_id).id_areaActual = id_area;
            Environment.sessions.GetSession(session_id).en_areapublica = area_publica;
            Environment.Game.areas.areas[Environment.sessions.GetSession(session_id).id_areaActual].visitantes++;
            return true;
        }

        public scenarioUser getUserBySession(long session_id)
        {
            if (usuarios_enArea.ContainsKey(session_id))
                return usuarios_enArea[session_id];
            else return null;
        }

        public scenarioUser getUserByID(int user_id)
        {
            Dictionary<long, scenarioUser>.Enumerator enumerator = usuarios_enArea.GetEnumerator();

            while (enumerator.MoveNext()) { if (enumerator.Current.Value.userInfo.id == user_id) return enumerator.Current.Value; }
            return null;
        }

        public scenarioUser getUserByUserAreaID(int userid_en_escenario)
        {
            Dictionary<long, scenarioUser>.Enumerator enumerator = usuarios_enArea.GetEnumerator();

            while (enumerator.MoveNext()) { if (enumerator.Current.Value.userid_en_escenario == userid_en_escenario) return enumerator.Current.Value; }
            return null;
        }

        public scenarioUser getUserByPosition(int x, int y)
        {
            Dictionary<long, scenarioUser>.Enumerator enumerator = usuarios_enArea.GetEnumerator();

            while (enumerator.MoveNext()) { if (enumerator.Current.Value.x_actual == x && enumerator.Current.Value.y_actual == y) return enumerator.Current.Value; }
            return null;
        }

        private areas cargar_handler(bool area_publica)
        {
            string handler = "areas";
            if (area_publica) handler = "areas"; else handler = "islas";

            areas Handler = (areas)Activator.CreateInstance(null, "Boombang.game.handlers." + handler).Unwrap();
            Handler.registrar_instancia(this);
            return Handler;
        }

        public scenarioInteractor Interactor
        {
            get
            {
                return mScenarioInteractor;
            }
        }

        public void activar_usuario(long session_id)
        {
            scenarioUser usuario = new scenarioUser();
            usuario.userid_en_escenario = asignar_id();
            usuario.session_id = session_id;

            usuarios_enArea.Add(session_id, usuario);

            publicar_usuario(session_id);

            publicar_todoslosusuarios(session_id);
        }

        public void quitar_usuario(long session_id)
        {
            Environment.Game.areas.areas[Environment.sessions.GetSession(session_id).id_areaActual].visitantes--;
            Environment.sessions.GetSession(session_id).id_areaActual = 0;
            Environment.sessions.GetSession(session_id).area_actual = string.Empty;

            if (sesiones_usuarios.ContainsKey(session_id))
            {
                Environment.sessions.GetSession(session_id).RemoveListener(sesiones_usuarios[session_id]);
                sesiones_usuarios.Remove(session_id);
            }

            if (usuarios_enArea.ContainsKey(session_id))
            {
                server message = new server("³{");

                message.Append("³²" + usuarios_enArea[session_id].userid_en_escenario + "³²");
                enviar_atodoelarea(message);

                id_usuarios.Remove(usuarios_enArea[session_id].userid_en_escenario);

                usuarios_enArea[session_id].esta_bloqueado = false;
                usuarios_enArea.Remove(session_id);
            }

            Environment.sessions.GetSession(session_id).en_areapublica = false;

            //if (usuarios_enArea.Count == 0) Environment.Game.areas.destruir_instancia(id_area, mDataScenario.es_publica);
        }

        public Dictionary<long, scenarioUser> Usuarios { get { return usuarios_enArea; } }

        public dataScenario scenarioInfo { get { return mDataScenario; } set { mDataScenario = value; } }

        public void destroyArea() { mScenarioInteractor.destroyInteractor(); mScenarioInteractor = null; }
    }
}
