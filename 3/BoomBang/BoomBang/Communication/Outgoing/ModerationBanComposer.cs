namespace BoomBang.Communication.Outgoing
{
    using BoomBang;
    using BoomBang.Communication;
    using BoomBang.Game.Moderation;
    using System;

    public static class ModerationBanComposer
    {
        public static ServerMessage Compose(BanDetails Details)
        {
            ServerMessage message;
            switch (Details.BanType)
            {
                case 0:
                    message = new ServerMessage(FlagcodesOut.BAN, 0, false);
                    message.AppendParameter((uint) (((uint) Details.TimestampEx) - ((uint) UnixTimestamp.GetCurrent())), false);
                    message.AppendParameter(Details.Reason, false);
                    return message;

                case 1:
                    message = new ServerMessage(FlagcodesOut.USER, ItemcodesOut.LANDING_LOGIN, false);
                    message.AppendParameter(4, false);
                    message.AppendParameter(Details.Reason, false);
                    message.AppendParameter(0, false);
                    return message;

                case 2:
                    message = new ServerMessage(FlagcodesOut.USER, ItemcodesOut.LANDING_LOGIN, false);
                    message.AppendParameter(4, false);
                    message.AppendParameter(Details.Reason, false);
                    message.AppendParameter(1, false);
                    return message;
            }
            message = new ServerMessage(FlagcodesOut.USER, ItemcodesOut.LANDING_LOGIN, false);
            message.AppendParameter(4, false);
            message.AppendParameter(Details.Reason, false);
            message.AppendParameter(0, false);
            return message;
        }
    }
}

