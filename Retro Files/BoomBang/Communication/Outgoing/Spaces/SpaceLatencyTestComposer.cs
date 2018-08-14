using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceLatencyTestComposer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(Opcodes.LATENCYTEST);
            message.AppendParameter(5, false);
            return message;
        }
    }
}
