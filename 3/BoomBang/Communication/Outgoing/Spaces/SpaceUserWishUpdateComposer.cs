using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceUserWishUpdateComposer
    {
        public static ServerMessage Compose(uint ActorId, uint LabelId, string Wish)
        {
            ServerMessage message = new ServerMessage(Opcodes.USERWHISES);
            message.AppendParameter(ActorId, false);
            message.AppendParameter(LabelId, false);
            message.AppendParameter(Wish, false);
            return message;
        }
    }
}
