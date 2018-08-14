using System;

using Boombang.sockets.messages;
using Boombang.game.session;

namespace Boombang.game.handlers
{
    public partial class ping : Handler
    {
        public void Handler163_type_178() //Ping
        {
            server message = new server("£");
            message.Append("³²20³²");
            SendMessage(message);
            //Console.WriteLine("[DEBUG] Respuesta de ping enviada.");
        }

        public void Handler163_type_0() //Ping
        {
            server message = new server("£");
            message.Append("³²20³²");
            SendMessage(message);
            //Console.WriteLine("[DEBUG] Respuesta de ping enviada.");
        }
    }
}
