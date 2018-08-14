using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Boombang.sockets.messages;

namespace Boombang.game.scenario
{
    public partial class scenarioInstance
    {
        public string construir_usuario(long session_id)
        {
            StringBuilder builder = new StringBuilder();
            scenarioUser sUser = usuarios_enArea[session_id];
            int es_moderador = (sUser.userInfo.es_moderador == true) ? 1 : 0;

            if (sUser != null)
            {
//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   Ring 2000                               10     500       500                                                                                   
                //                  0                       ³²          nichepapi           ³²              10      ³²FF6663000000FB11E50033333333330099CC000000       ³²    7    ³²    12    ³²    4    ³²          Boombang           ³²         14                ³²               0                        "³²" + 6 + "³²³²" + 6 + "³²" + 6 + "³²" + 6 + "³²" + 6 + "³²"                                     Vacio³Vacio³Vacio                                     ³²                                       Vacio³Vacio³Vacio                                    ³²              50                  ³             51                  ³               50                     ³²            Hola                ³²               0                     ³                 2                    ³                   0                    ³                   1                     ³                 0                    ³                 1                     ³                 0                       ³                   0                      ³                   0                 ³               1                                                 ³5³0³0³1³-1³0³0³0³1³0³0³5³0³0³0³200³²        Ninja      Ninja Ninja             mod         ³²   vip   ³² cambios ³²  pocima  ³²        9288322          ³² °
                builder.Append(sUser.userid_en_escenario + "³²" + sUser.userInfo.usuario + "³²" + sUser.userInfo.tipo_avatar + "³²" + sUser.userInfo.colores_avatar + "³²" + 7 + "³²" + 12 + "³²" + sUser.userInfo.area_a_entrar + "³²" + sUser.userInfo.ciudad + "³²" + sUser.userInfo.edad + "³²" + sUser.userInfo.tiempo_registrado + "³²" + 9 + "³²³²" + 9 + "³²" + 9 + "³²"+ 0 + "³²" + 0 + "³²" + sUser.userInfo.hobby_1 + "³" + sUser.userInfo.hobby_2 + "³" + sUser.userInfo.hobby_3 + "³²" + sUser.userInfo.deseo_1 + "³" + sUser.userInfo.deseo_2 + "³" + sUser.userInfo.deseo_3 + "³²" + sUser.userInfo.votos_legal + "³" + sUser.userInfo.votos_sexy + "³" + sUser.userInfo.votos_simpatico + "³²" + sUser.userInfo.bocadillo + "³²" + sUser.userInfo.besos_enviados + "³" + sUser.userInfo.besos_recibidos + "³" + sUser.userInfo.cocteles_enviados + "³" + sUser.userInfo.cocteles_recibidos + "³" + sUser.userInfo.flores_enviadas + "³" + sUser.userInfo.flores_recibidas + "³" + sUser.userInfo.uppercuts_enviados + "³" + sUser.userInfo.uppercuts_recibidos + "³" + sUser.userInfo.cocos_enviados + "³" + sUser.userInfo.cocos_recibidos + "³5³2000³1³-1³1³999³1³300³10³300³600³300³10³500³10³500³²" + es_moderador + "³²" + 1 + "³²" + 1 + "³²" + 0 + "³²" + sUser.userInfo.id + "³²");
            }
            return builder.ToString();
        }

        public void publicar_statususuario(long session_id, bool forzar_actualizacion)
        {
            string status_usuario = construir_statususuario(session_id, forzar_actualizacion);
            if (status_usuario != null)
            {
                server message = new server("");
                message.Append(status_usuario);
                enviar_atodoelarea(message);
            }
        }

        public string construir_statususuario(long session_id, bool forzar_actualizacion)
        {
            scenarioUser sUser = getUserBySession(session_id);
            StringBuilder builder = new StringBuilder();

            if (sUser.esta_caminando)
            {
                builder.Append("");
                builder.Append(sUser.x_siguiente.ToString());
                builder.Append("");
                builder.Append(sUser.y_siguiente.ToString());
                builder.Append("");
            }

            StringBuilder userStatus = new StringBuilder();
            userStatus.Append(sUser.userid_en_escenario);
            userStatus.Append(sUser.x_actual);
            userStatus.Append(sUser.y_actual);

            userStatus.Append(sUser.direccion_cuerpo);
            userStatus.Append(userStatus + "");

            if (forzar_actualizacion || sUser.necesita_actualizar)
            {
                sUser.necesita_actualizar = true;
                string nuevo_status = userStatus.ToString();

                if (forzar_actualizacion)
                    return nuevo_status;
                else
                    return null;
            }
            else return null;
        }

    }
}
