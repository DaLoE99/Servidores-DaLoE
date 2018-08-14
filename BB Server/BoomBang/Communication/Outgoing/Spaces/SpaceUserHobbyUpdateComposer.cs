using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceUserHobbyUpdateComposer
    {
        public static ServerMessage Compose(uint ActorId, uint LabelId, string Hobby)
        {
            ServerMessage message = new ServerMessage(Opcodes.USERHOBBYS);
            message.AppendParameter(ActorId, false);
            message.AppendParameter(LabelId, false);
            message.AppendParameter(Hobby, false);
            return message;
        }
    }
}
