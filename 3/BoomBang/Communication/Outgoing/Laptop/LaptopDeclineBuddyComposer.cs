using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Laptop
{
    class LaptopDeclineBuddyComposer
    {
        public static ServerMessage Compose(uint CharacterId)
        {
            ServerMessage message = new ServerMessage(Opcodes.LAPTOPFRIENDDECLINE);
            message.AppendParameter(CharacterId, false);
            return message;
        }
    }
}
