using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Laptop
{
    class LaptopDeleteFriendComposer
    {
        public static ServerMessage Compose(uint FriendId)
        {
            ServerMessage message = new ServerMessage(Opcodes.LAPTOPFRIENDDELETE);
            message.AppendParameter(FriendId, false);
            return message;
        }
    }
}
