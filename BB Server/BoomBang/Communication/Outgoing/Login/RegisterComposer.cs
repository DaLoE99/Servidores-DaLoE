using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing
{
    class RegisterComposer
    {
        public static ServerMessage Compose()
        {
            //±x³ƒ³²1³²°
            ServerMessage message = new ServerMessage(Opcodes.REGISTER);
            message.Append(1);

            return message;
        }
    }
}
