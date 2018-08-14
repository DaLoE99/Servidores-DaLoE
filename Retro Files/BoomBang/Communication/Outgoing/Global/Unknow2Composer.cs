using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Global
{
    class Unknow2Composer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(Opcodes.UNKNOW2);
            return message;
        }
    }
}
