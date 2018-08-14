using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceCatchItemComposer
    {
        public static ServerMessage Compose(uint ItemId)
        {
            ServerMessage message = new ServerMessage(Opcodes.SPACEITEMCATCH);
            message.AppendParameter(ItemId, false);
            message.AppendParameter(true, false);
            return message;
        }
    }
}
