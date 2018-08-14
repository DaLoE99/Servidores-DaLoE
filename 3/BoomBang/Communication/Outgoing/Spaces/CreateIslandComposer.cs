using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class CreateIslandComposer
    {
        public static ServerMessage Compose(uint uint_0)
        {
            ServerMessage message = new ServerMessage(Opcodes.ISLANDCREATE);
            message.AppendParameter(uint_0, false);
            return message;
        }

        public static ServerMessage TakenNameComposer()
        {
            ServerMessage message = new ServerMessage(Opcodes.ISLANDCREATE);
            message.AppendParameter(0, false);
            return message;
        }
    }
}
