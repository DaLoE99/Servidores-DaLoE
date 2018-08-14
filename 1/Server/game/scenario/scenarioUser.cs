using System;

using Boombang.game.user;

namespace Boombang.game.scenario
{
    public class scenarioUser
    {
        public long session_id;
        public int userid_en_escenario;

        public bool esta_bloqueado;
        public bool esta_caminando;
        public bool necesita_actualizar;

        public int id_accion;
        public int direccion_cuerpo;

        public string pos_actual;
        public int x_actual;
        public int y_actual;
        public int x_siguiente;
        public int y_siguiente;
        public int x_destino;
        public int y_destino;

        public userInfo userInfo { get { return Environment.sessions.GetSession(session_id).mUser; } }
    }
}
