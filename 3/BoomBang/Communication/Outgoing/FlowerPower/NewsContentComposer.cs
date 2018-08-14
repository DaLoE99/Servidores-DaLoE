using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Handlers;

namespace Snowlight.Communication.Outgoing
{
    class NewsContentComposer
    {
        public static ServerMessage Compose(Notice Report)
        {
            ServerMessage message = new ServerMessage(Opcodes.NEWSREAD);
            message.AppendParameter(2, false);
            message.AppendParameter(Report.Id, false);
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
