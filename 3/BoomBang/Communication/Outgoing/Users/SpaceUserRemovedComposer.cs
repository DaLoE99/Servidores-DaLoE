using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing
{
    class SpaceUserRemovedComposer
    {
        public static ServerMessage BroadcastCompose(uint ActorId)
        {
            ServerMessage message = new ServerMessage(Opcodes.SPACESEXITBROADCAST);
            message.AppendParameter(ActorId, false);
            return message;
        }

        public static ServerMessage SingleCompose()
        {
            return new ServerMessage(Opcodes.SPACESEXITLAND);
        }
    }
}
