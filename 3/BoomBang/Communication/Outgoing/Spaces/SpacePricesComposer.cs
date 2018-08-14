using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpacePricesComposer
    {
        public static ServerMessage Compose(bool AllowUppercut, bool AllowCoconut, uint PriceUppercut, uint PriceCoconut)
        {
            ServerMessage message = new ServerMessage(Opcodes.ACTIONS);
            message.AppendParameter(1, true);
            message.AppendParameter(0, true);
            message.AppendParameter(1, false);
            message.AppendParameter(2, true);
            message.AppendParameter(0, true);
            message.AppendParameter(1, false);
            message.AppendParameter(3, true);
            message.AppendParameter(0, true);
            message.AppendParameter(1, false);
            message.AppendParameter(4, true);
            message.AppendParameter(AllowUppercut ? ((int)PriceUppercut) : -1, true);
            message.AppendParameter(AllowUppercut, false);
            message.AppendParameter(5, true);
            message.AppendParameter(AllowCoconut ? ((int)PriceCoconut) : -1, true);
            message.AppendParameter(AllowCoconut, false);
            return message;
        }
    }
}
