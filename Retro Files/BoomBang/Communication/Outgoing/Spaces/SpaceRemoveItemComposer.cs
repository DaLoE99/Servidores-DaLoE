using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceRemoveItemComposer
    {
        public static ServerMessage Compose(uint ItemId)
        {
            ServerMessage message = new ServerMessage(Opcodes.SPACEITEMREMOVE);
            message.AppendParameter(true, false);
            message.AppendParameter(ItemId, false);
            return message;
        }
    }
}
