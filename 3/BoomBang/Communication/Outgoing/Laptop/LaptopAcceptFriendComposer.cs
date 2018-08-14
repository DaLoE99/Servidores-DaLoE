using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Laptop
{
    class LaptopAcceptFriendComposer
    {
        public static ServerMessage Compose(uint uint_0)
        {
            ServerMessage message = new ServerMessage(132131);
            message.AppendParameter(true, false);
            message.AppendParameter(uint_0, false);
            return message;
        }
    }
}
