using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Communication.Outgoing.Chat;

namespace Snowlight.Communication.Outgoing
{
    class SpaceChatComposer
    {
        public static ServerMessage Compose(uint ActorId, string MessageText, int MessageColor, ChatType ChatType)
        {
            switch (ChatType)
            {
                case ChatType.Say:
                    {
                        ServerMessage message = new ServerMessage(Opcodes.USERCHAT);
                        message.AppendParameter(ActorId, false);
                        message.AppendParameter(MessageText, false);
                        message.AppendParameter(MessageColor, false);
                        return message;
                    }
                case ChatType.Whisper:
                    {
                        ServerMessage message2 = new ServerMessage(Opcodes.USERWHISPER);
                        message2.AppendParameter(ActorId, false);
                        message2.AppendParameter(MessageText, false);
                        message2.AppendParameter(MessageColor, false);
                        return message2;
                    }
            }
            ServerMessage message3 = new ServerMessage(Opcodes.USERCHAT);
            message3.AppendParameter(ActorId, false);
            message3.AppendParameter(MessageText, false);
            message3.AppendParameter(MessageColor, false);
            return message3;
        }
    }
}
