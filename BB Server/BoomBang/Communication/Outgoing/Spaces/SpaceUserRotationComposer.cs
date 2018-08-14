using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceUserRotationComposer
    {
        public static ServerMessage Compose(uint ActorId, int int_0, int int_1, int Rotation)
        {
            ServerMessage message = new ServerMessage(Opcodes.USERROTATION);
            message.AppendParameter(ActorId, false);
            message.AppendParameter(int_0, false);
            message.AppendParameter(int_1, false);
            message.AppendParameter(Rotation, false);
            return message;
        }
    }
}
