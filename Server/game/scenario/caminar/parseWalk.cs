using System;
using System.Text;
using System.Threading;

using Boombang.sockets.messages;
using Boombang.game.session;

namespace Boombang.game.scenario.caminar
{
    public static class parseWalk
    {
        private static int _user_id;
        private static int length;
        private static string _data;
        private static string[] pebete;
        private static scenarioInstance _scenarioInstance;
        private static scenarioUser _sUser;
        private static Thread walk_thread = new Thread(send_walk);


        public static void parse(int user_id, string data, bool mustUpdate, scenarioInstance scenarioInstance, scenarioUser sUser)
        {
            sUser.esta_caminando = true;

                    _user_id = user_id;
                    _scenarioInstance = scenarioInstance;
                    _sUser = sUser;
                    length = data.Length;
                    _data = data;
                    int size = length / 5;
                    pebete = new string[size];

                    send_walk();
        }

        //static internal void send_walk(int user_id, int length, string[] pebete, scenarioInstance scenarioInstance, scenarioUser sUser, bool mustUpdate)
        static internal void send_walk()
        {
            StringBuilder builder = new StringBuilder();
            int j = 0;

            for (int i = 0; i < length; i += 5)
            {
                builder.Append(_data.Substring(i, 2) + "³²");
                builder.Append(_data.Substring(i + 2, 2) + "³²");
                builder.Append(_data.Substring(i + 4, 1) + "³²");
                pebete[j] = builder.ToString();

                builder.Remove(0, builder.Length);
                j++;
            }

            int q = 0;

            while (q < pebete.Length)
            {
                server message = new server("¶³²1³²");
                message.Append(_user_id + "³²");
                message.Append(pebete[q]);
                message.Append("650³²");
                if (q == pebete.Length - 1)
                {
                    message.Append("1³²");
                    _sUser.pos_actual = pebete[q];
                }
                else
                    message.Append("0³²");

                _scenarioInstance.enviar_atodoelarea(message);
                q++;
                Thread.Sleep(550);
            }

            _sUser.esta_caminando = false;
        }
    }
}
