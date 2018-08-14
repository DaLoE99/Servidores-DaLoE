namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.FlowerPower;
    using System;
    using System.Collections.Generic;

    public static class NewsInitComposer
    {
        public static ServerMessage Compose(List<Notice> NoticeList)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.NEWS, ItemcodesOut.NEWS_LOAD_ARTICLES, false);
            message.AppendParameter(2, false);
            message.AppendParameter(0, false);
            message.AppendParameter(NoticeList.Count, false);
            foreach (Notice notice in NoticeList)
            {
                message.AppendParameter(notice.Content, true);
                message.AppendParameter(notice.Title, true);
                message.AppendParameter(notice.Date, false);
            }
            return message;
        }
    }
}

