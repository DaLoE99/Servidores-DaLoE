using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

using Boombang.sockets.messages;
using Boombang.game.session;
using Boombang.game.user;
using Boombang.game.scenario;
using Boombang.game.scenario.caminar;

namespace Boombang.game.handlers
{
    public partial class areas : Handler
    {
        private scenarioInstance mScenarioInstance;

        public void registrar_instancia(scenarioInstance areaInstance)
        {
            mScenarioInstance = areaInstance;
        }

        public void Handler128_type_120()
        {
            //³x³,1³,1³,1³,0³,0³,15³,16³,SkatePark 1³,1³
            //,x,1,3,,-1

            int null_0 = Convert.ToInt32(Message.getParameter());
            int id_area = Convert.ToInt32(Message.getParameter());
            Dictionary<int, scenario.dataScenario> escenario = Environment.Game.areas.areas;
            server message = new server("³x³²");
            message.Append(1 + "³²" + 1 + "³²" + 1 + "³²" + 0 + "³²" + 0 + "³²" + 0 + "³²" + escenario[id_area].modelo_area + "³²" + escenario[id_area].nombre + "³²" + escenario[id_area].visitantes + "³²");
            SendMessage(message);
        }

        public void Handler128_type_121()
        {
            //³y³x³²
            entrar_area(mUser.area_a_entrar, true, "");

            server message = new server("¯³²1³0³1³²2³0³1³²3³0³1³²");

            if (mUser.dataScenario.permitir_uppercut == 0 && mUser.dataScenario.permitir_coco == 0)
                message.Append("4³-1³0³²5³-1³0³²");
            else if (mUser.dataScenario.permitir_uppercut == 0)
                message.Append("4³-1³0³²5³25³1³²");
            else if (mUser.dataScenario.permitir_coco == 0)
                message.Append("4³250³1³²5³-1³0³²");
            else
                message.Append("4³250³1³²5³25³1³²");

            SendMessage(message);
        }

        public void Handler128_type_124()
        {
            //³|³²
            if (mUser.sUser.esta_bloqueado == false)
            {
                mUser.scenarioInstance.quitar_usuario(mSessionID);
                server message = new server("³|³²");
                SendMessage(message);
            }
        }

        public void Handler128_type_125()
        {
            //²x²1²10²²-1
            //±³x³²1³²1³²1³²0³²0³²10³²11³²Ryuu 1³²1³²°
            //³x³,1³,1³,1³,0³,0³,0³,1³,BelugaBeach 1³,1³
            int null_0 = Convert.ToInt32(Message.getParameter());
            int id_area = Convert.ToInt32(Message.getParameter());
            Dictionary<int, scenario.dataScenario> escenario = Environment.Game.areas.areas;

            mUser.area_a_entrar = id_area;

            //³x³²1³²1³²1³²0³²0³²8³²9³²Igloo 1³²1³²
            //³x³,1³,1³,1³,0³,0³,0³,1³,BelugaBeach 1³,1³
            server message = new server("³x³²");
            message.Append(1 + "³²" + 1 + "³²" + 1 + "³²" + 0 + "³²" + 0 + "³²" + 0 + "³²" + escenario[id_area].modelo_area + "³²" + escenario[id_area].nombre + "³²" + escenario[id_area].visitantes + "³²");
            SendMessage(message);
        }

        public void Handler133_type_178()
        {
            string contenido = Message.Tostring().Substring(4);
            string[] split_data = Regex.Split(contenido, "³²");

            string mensaje = split_data[1];
            int color = (mUser.es_moderador == true) ? 2 : 1;

            if (mensaje.Contains(":cofre") && mUser.es_moderador == true)
            {
                Random rand = new Random();
                int x = rand.Next(7, 16); // yo nose como coño va esta mierda
                int y = rand.Next(6, 17); // necesito saber el x y maximo y el minimo de cada area xdxd ostia ya se 1 sec
                int id = rand.Next(100, 5000);
                int objeto = Convert.ToInt32(mensaje.Substring(7));
                Console.WriteLine("[DEBUG] Se ha tirado un cofre.");

                //È³x³,1682³,3³,15³,16³,2³,1³,1³
                server message = new server("È³x³² " + id + "³²3³²" + y + "³²" + x + "³²" + objeto + "³²1³²1³²1³²");
                mUser.scenarioInstance.enviar_atodoelarea(message);
            }
            else mUser.scenarioInstance.hablar_atodalasala(mensaje, mUser.sUser, color);
        }

        public void Handler134_type_178()
        {
            if (mUser.sUser.esta_bloqueado == false)
            {
                string contenido = Message.Tostring().Substring(4);
                string[] split_data = Regex.Split(contenido, "³²");

                int user_id = Convert.ToInt32(split_data[0]);
                int accion = Convert.ToInt32(split_data[1]);

                server message = new server("³²");
                message.Append(user_id + "³²" + accion + "³²");
                mUser.scenarioInstance.enviar_atodoelarea(message);
            }
        }

        public void Handler135_type_178()
        {
            if (mUser.sUser.esta_bloqueado == false)
            {
                string contenido = Message.Tostring().Substring(4);
                string[] split_data = Regex.Split(contenido, "³²");

                int user_id = Convert.ToInt32(split_data[0]);
                int rotacion = Convert.ToInt32(split_data[1]);

                server message = new server("³²");
                message.Append(user_id + "³²");
                if (!(string.IsNullOrEmpty(mUser.sUser.pos_actual)))
                    message.Append(mUser.sUser.pos_actual.Substring(0, 6));
                message.Append("³²" + rotacion + "³²");
                SendMessage(message);
            }
        }

        public void Handler137_type_178() //beso, coctel, rosa
        {

        }

        public void Handler145_type_178() //Uppercut
        {

            /*
             punx enviado
             ¡³,250³ -> quito creditos
             ³,2805352³,4³,38³,175³ -> mis datos
             ³,759438³,tipo_dato³,2494³,2526³ -> sus datos
             ³,4³,0³,16³,18³,1³,17³,19³ -> hostia con coordenadas
             punx recibido
             ³,759438³,4³,2492³,249³ -> sus datos
             ³,2805352³,tipo_dato³,35³,174³ -> mis datos
             ³,4³,1³,14³,19³,0³,15³,20³ -> hostia con coordenadas
             */

            if (mUser.sUser.esta_bloqueado == false)
            {
                string contenido = Message.Tostring().Substring(4);
                string[] split_data = Regex.Split(contenido, "³²");

                string tipo = split_data[0];
                string id_envia = split_data[1];
                string id_recibe = split_data[4];
                string envia_x = split_data[2];
                string envia_y = split_data[3];
                string recibe_x = split_data[5];
                string recibe_y = split_data[6];

                Console.WriteLine("[DEBUG] id envia: " + id_envia);
                Console.WriteLine("[DEBUG] id recibe: " + id_recibe);

                userInfo oUser = mUser.scenarioInstance.getUserByUserAreaID(int.Parse(id_recibe)).userInfo;

                mUser.scenarioInstance.getUserByUserAreaID(int.Parse(id_recibe)).esta_bloqueado = true;
                mUser.sUser.esta_bloqueado = true;

                server message = new server("¡³²250³²");
                server message_2 = new server("³²");
                server message_3 = new server("³²");
                server message_4 = new server("³²");
                message_2.Append(mUser.id + "³²4³²" + mUser.uppercuts_enviados + "³²" + mUser.uppercuts_recibidos + "³²");
                message_3.Append(oUser.id + "³²4³²" + oUser.uppercuts_enviados + "³²" + oUser.uppercuts_recibidos + "³²");
                message_4.Append("4³²" + id_envia + "³²" + envia_x + "³²" + envia_y + "³²" + id_recibe + "³²" + recibe_x + "³²" + recibe_y + "³²");

                SendMessage(message);
                mUser.scenarioInstance.enviar_atodoelarea(message_2);
                mUser.scenarioInstance.enviar_atodoelarea(message_3);
                mUser.scenarioInstance.enviar_atodoelarea(message_4);

                Thread.Sleep(15000);
                mUser.sUser.esta_bloqueado = false;

                Thread.Sleep(5000);
                server message_5 = new server("³²");
                Environment.sessions.GetSession(Environment.sessions.GetSessionFromUser(oUser.id)).SendMessage(message_5);
                Console.WriteLine("[DEBUG] " + oUser.usuario + " ha sido expulsado de la sala por " + mUser.usuario);
                mUser.scenarioInstance.quitar_usuario(Environment.sessions.GetSessionFromUser(oUser.id));
            }
        }

        public void Handler149_type_178() //Coco
        {
            /*
             coco enviado
             ¸³x³,tu_id³,0³,35³ -> te bloqueo la accion
             ¡³,25³ -> quita creditos
             ³,2805352³,5³,25³,100³ ´-> mis cocos recibidos y enviados
             ³,759438³,5³,1404³,225³ -> tus cocos recibidos y enviados
             ¸³y³,759438³,-1³,35³ -> te desbloqueas
             coco recibido
             ¸³x³,2805352³,0³,35³ -> me bloqueo
             ³,759438³,5³,1403³,223³ -> sus datos
             ³,2805352³,5³,23³,99³ -> mis datos
             ¸³y³,2805352³,-1³,35³ -> me desbloqueo
             */
        }

        public void Handler154_type_32()
        {
            //±³ ³²
            //1³²1³²1³²BelugaBeach³²57³²
            //1³²1³²2³²U.F.O.³²22³²
            //1³²1³²3³²MiniKong³²16³²
            //2³²2³²1³²Ring³²167³²

            server message = new server("³ ");

            foreach (KeyValuePair<int, scenario.dataScenario> escenario in Environment.Game.areas.areas)
            {
                if(escenario.Value.id_principal == 0)
                    message.Append("³²" + escenario.Value.categoria + "³²" + 1 + "³²" + escenario.Value.id_area + "³²" + escenario.Value.nombre + "³²" + escenario.Value.visitantes);
            }
            message.Append("³²");

            SendMessage(message);
        }

        public void Handler154_type_33()
        {
            //±³!³²
            //1³²1³²0³²0³²3³²4³²BaoBab 1³²0³²12³²
            //1³²1³²0³²0³²21³²4³²BaoBab 2³²11³²12³²°
            //1³²1³²0³²0³²1³²2³²U.F.O. 1³²11³²12³²
            //1³²1³²0³²0³²23³²2³²U.F.O. 2³²1³²12³²

            int null_0 = Convert.ToInt32(Message.getParameter());
            int id_area = Convert.ToInt32(Message.getParameter());

            server message = new server("³!");
            foreach (KeyValuePair<int, scenario.dataScenario> escenario in Environment.Game.areas.areas)
            {
                if (escenario.Value.id_principal == id_area)
                    message.Append("³²" + 1 + "³²" + 1 + "³²" + 0 + "³²" + 0 + "³²" + escenario.Value.id_area + "³²" + escenario.Value.id_principal + "³²" + escenario.Value.nombre + "³²" + escenario.Value.visitantes + "³²" + escenario.Value.max_visitantes);
            }
            

            message.Append("³²");
            SendMessage(message);
        }

        public void Handler156_178()
        {
            //Actualizar hobbies
        }

        public void Handler157_178()
        {
            //Actualizar deseos
        }

        public void Handler182_type_178()
        {
            if (mUser.sUser.esta_bloqueado == false || mUser.sUser.esta_caminando == false)
            {
                string contenido = Message.Tostring().Substring(4);
                string[] split_data = Regex.Split(contenido, "³²");

                int user_id = Convert.ToInt32(split_data[0]);
                string pasos = split_data[1];

                if (mUser.sUser.esta_caminando == false) parseWalk.parse(user_id, pasos, false, mUser.scenarioInstance, mUser.sUser);
                else parseWalk.parse(user_id, pasos, true, mUser.scenarioInstance, mUser.sUser);
            }
        }

        public void Handler187_type_178() //Inicio navegador islas
        {
            server message = new server("»");
            message.Append("³²4³²");
            SendMessage(message);
        }

        public void Handler187_type_0() //Inicio navegador islas
        {
            server message = new server("»");
            message.Append("³²4³²");
            SendMessage(message);
        }

        public void Handler200_type_121()
        {
            string id_objeto = Message.getParameter();

            server message = new server("È³z³²");
            message.Append(id_objeto + "³²"); //ID objeto
            message.Append("1³²");
            message.Append(mUser.usuario + " ha atrapado un cofre y obtiene: 1000 créditos de oro.³²");

            server message_2 = new server("È³{³²1³²");
            message_2.Append(id_objeto + "³²");

            server message_3 = new server("¢³²");
            message_3.Append("1000³²");

            mUser.scenarioInstance.enviar_atodoelarea(message);
            mUser.scenarioInstance.enviar_atodoelarea(message_2);
            mUser.scenarioInstance.enviar_atodoelarea(message_3);

            mUser.creditos_oro = mUser.creditos_oro + 1000;

            Environment.Game.User.update_credits(mUser);
        }

        public void entrar_area(int id_area, bool es_publica, string password)
        {
            mUser.scenarioInstance = Environment.Game.areas.get_scenarioInstance(id_area, es_publica);
            if (es_publica) mUser.dataScenario = Environment.Game.areas.area(id_area);
            else mUser.dataScenario = Environment.Game.areas.isla(id_area);

            if (mUser.scenarioInstance.Usuarios.Count < mUser.dataScenario.max_visitantes || mUser.es_moderador == true)
            {
                //if (es_publica) ; else ;
                mUser.scenarioInstance.añadir_usuario(mSessionID, true);
                mUser.scenarioInstance.activar_usuario(mSessionID);
            }        
            else
            {
                return;
            }

            mUser.sUser = mUser.scenarioInstance.usuarios_enArea[mSessionID];
        }
    }
}
