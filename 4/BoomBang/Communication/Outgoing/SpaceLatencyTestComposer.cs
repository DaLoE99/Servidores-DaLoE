namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceLatencyTestComposer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.LATENCY_TEST, 0, false);
            message.AppendParameter(5, false);
            return message;
        }
    }
}

