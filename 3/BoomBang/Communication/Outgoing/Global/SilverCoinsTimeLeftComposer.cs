using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Global
{
    class SilverCoinsTimeLeftComposer
    {
        public static ServerMessage Compose(int Time)
        {
            ServerMessage message = new ServerMessage(Opcodes.SILVERTIMELEFT);
            message.AppendParameter(Time, false);
            return message;
        }
    }
}
