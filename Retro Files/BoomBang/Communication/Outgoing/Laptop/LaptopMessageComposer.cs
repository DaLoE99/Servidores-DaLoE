﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Laptop
{
    class LaptopMessageComposer
    {
        /* private scope */
        static Random random_0 = new Random();

        public static ServerMessage Compose(uint CharacterId, string Text, uint Color)
        {
            ServerMessage message = new ServerMessage(Opcodes.LAPTOPSENDMESSAGE);
            message.AppendParameter(random_0.Next(1, 0x186a0), false);
            message.AppendParameter(CharacterId, false);
            message.AppendParameter(DateTime.Now.ToString("MM/dd/yy HH:mm"), false);
            message.AppendParameter(Text, false);
            message.AppendParameter(Color, false);
            return message;
        }
    }
}
