using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceUserSendActionComposer
    {
        public static ServerMessage Compose(uint ActionId, uint SenderId)
        {
            ServerMessage message = new ServerMessage(137120);
            message.AppendParameter(ActionId, false);
            message.AppendParameter(SenderId, false);
            return message;
        }
    }
}
