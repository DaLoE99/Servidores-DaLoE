﻿namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class PingComposer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.SESSION_PONG, 0, false);
            message.AppendParameter(20, false);
            return message;
        }
    }
}

