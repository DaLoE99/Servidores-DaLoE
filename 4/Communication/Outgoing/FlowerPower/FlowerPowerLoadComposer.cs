using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing
{
    class FlowerPowerLoadComposer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(Opcodes.FLOWERPOWER);
            message.AppendParameter(16, false);
            message.AppendParameter(-1, false);
            message.AppendParameter(-1, false);
            message.AppendParameter(25, false);
            return message;
        }
    }
}
