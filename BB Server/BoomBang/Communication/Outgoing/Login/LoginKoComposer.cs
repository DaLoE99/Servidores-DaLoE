using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing
{
    class LoginKoComposer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(Opcodes.LOGIN);
            message.Append(0);
            

            return message;
        }
    }
}
