using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceUserSendCoconut
    {
        public static ServerMessage SendAndBlock(uint TargetId)
        {
            ServerMessage message = new ServerMessage(184120);
            message.AppendParameter(TargetId, false);
            message.AppendParameter(0, false);
            message.AppendParameter(0x23, false);
            return message;
        }

        public static ServerMessage Unlock(uint TargetId)
        {
            ServerMessage message = new ServerMessage(184121);
            message.AppendParameter(TargetId, false);
            message.AppendParameter(-1, false);
            message.AppendParameter(0x23, false);
            return message;
        }
    }
}
