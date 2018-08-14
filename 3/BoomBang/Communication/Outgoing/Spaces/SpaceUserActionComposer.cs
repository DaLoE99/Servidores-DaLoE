using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceUserActionComposer
    {
        public static ServerMessage Compose(uint CharacterId, uint ActionId)
        {
            ServerMessage message = new ServerMessage(Opcodes.USERACTIONS);
            message.AppendParameter(ActionId, false);
            message.AppendParameter(CharacterId, false);
            return message;
        }
    }
}
