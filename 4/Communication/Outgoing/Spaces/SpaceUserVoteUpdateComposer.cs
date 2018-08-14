using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceUserVoteUpdateComposer
    {
        public static ServerMessage Compose(uint ActorId, uint ColorId, int Vote)
        {
            ServerMessage message = new ServerMessage(Opcodes.USERVOTES);
            message.AppendParameter(ActorId, false);
            message.AppendParameter(ColorId, false);
            message.AppendParameter(Vote, false);
            return message;
        }
    }
}
