using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.FlowerPower
{
    class LoaderAdvertisementComposer
    {
        public static ServerMessage Compose(bool IsActive)
        {
            ServerMessage message = new ServerMessage(Opcodes.ADVERTISEMENT);
            message.Append(IsActive);
            return message;
        }
    }
}
