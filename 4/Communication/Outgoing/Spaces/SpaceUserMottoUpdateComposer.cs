using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceUserMottoUpdateComposer
    {
        public static ServerMessage Compose(uint ActorId, string Motto)
        {
            ServerMessage message = new ServerMessage(Opcodes.USERMOTTO);
            message.AppendParameter(ActorId, false);
            message.AppendParameter(Motto, false);
            return message;
        }
    }
}
