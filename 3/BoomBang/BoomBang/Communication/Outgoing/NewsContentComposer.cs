namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.FlowerPower;
    using System;

    public static class NewsContentComposer
    {
        public static ServerMessage Compose(Notice Report)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.NEWS, ItemcodesOut.NEWS_SHOW_ARTICLES, false);
            message.AppendParameter(2, false);
            message.AppendParameter(Report.UInt32_0, false);
            message.AppendParameter(Report.Title, false);
            message.AppendParameter(Report.Content, false);
            message.AppendParameter(Report.Date, false);
            if (!string.IsNullOrEmpty(Report.Image))
            {
                message.AppendParameter(1, false);
                message.AppendParameter(Report.Image, false);
                return message;
            }
            message.AppendParameter(3, false);
            return message;
        }
    }
}

