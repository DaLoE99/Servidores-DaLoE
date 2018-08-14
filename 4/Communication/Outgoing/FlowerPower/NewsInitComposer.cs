using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Handlers;

namespace Snowlight.Communication.Outgoing.FlowerPower
{
    class NewsInitComposer
    {
        public static ServerMessage Compose(List<Notice> NoticeList)
        {
            ServerMessage message = new ServerMessage(Opcodes.NEWSLOAD);
            message.AppendParameter(2, false);
            message.AppendParameter(0, false);
            message.AppendParameter(NoticeList.Count, false);
            foreach (Notice notice in NoticeList)
            {
                message.AppendParameter(notice.Id, true);
                message.AppendParameter(notice.Title, true);
                message.AppendParameter(notice.Date, false);
            }
            return message;
        }
    }
}
