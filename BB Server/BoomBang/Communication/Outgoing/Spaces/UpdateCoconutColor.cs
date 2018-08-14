using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class UpdateCoconutColor
    {
        public static ServerMessage Compose(uint CharacterId, uint Color)
        {
            ServerMessage message = new ServerMessage(Opcodes.COLORCOCONUT);
            message.AppendParameter(CharacterId, false);
            message.AppendParameter(Color, false);
            return message;
        }
    }
}
