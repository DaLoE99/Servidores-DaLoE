using System;

using Boombang.sockets.messages;
using Boombang.game.session;
using Boombang.sockets;

namespace Boombang.game.handlers
{
    public partial class catalogo : Handler
    {
        public void Handler189_type_133()
        {
            server message = new server("½");
            message.Append("³" + Convert.ToChar(133) + "³²8³²cactus³²Cactus³²cactus³²Cuidado! NO TE PINCHES!³²-1³²20³²1³²7F471309A21D06B5DA³²125,41,50,29,17,64,2,84,86³");
            SendCatalog(message);
        }
    }
}
