﻿namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class UpdateCoconutColor
    {
        public static ServerMessage Compose(uint CharacterId, uint Color)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.COLOR_COCONUT, 0, false);
            message.AppendParameter(CharacterId, false);
            message.AppendParameter(Color, false);
            return message;
        }
    }
}

