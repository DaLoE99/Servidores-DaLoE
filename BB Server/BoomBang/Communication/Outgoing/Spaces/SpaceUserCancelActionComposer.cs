using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceUserCancelActionComposer
    {
        public static ServerMessage SenderCompose(uint SenderId)
        {
            ServerMessage message = new ServerMessage(Opcodes.USERINTERACTCANCEL);
            message.AppendParameter(SenderId, false);
            return message;
        }

        public static ServerMessage TargetCompose(uint TargetId)
        {
            ServerMessage message = new ServerMessage(Opcodes.TARGETUSERINTERACTCANCEL);
            message.AppendParameter(TargetId, false);
            return message;
        }
    }
}
