namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SilverCoinsTimeLeftComposer
    {
        public static ServerMessage Compose(int Time)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.SILVER_TIMELEFT, 0, false);
            message.AppendParameter(Time, false);
            return message;
        }
    }
}

