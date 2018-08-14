namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceChatComposer
    {
        public static ServerMessage Compose(uint ActorId, string MessageText, int MessageColor, ChatType ChatType)
        {
            switch (ChatType)
            {
                case ChatType.Say:
                {
                    ServerMessage message = new ServerMessage(FlagcodesOut.USER_CHAT, 0, false);
                    message.AppendParameter(ActorId, false);
                    message.AppendParameter(MessageText, false);
                    message.AppendParameter(MessageColor, false);
                    return message;
                }
                case ChatType.Whisper:
                {
                    ServerMessage message2 = new ServerMessage(FlagcodesOut.USER_WHISPER, 0, false);
                    message2.AppendParameter(ActorId, false);
                    message2.AppendParameter(MessageText, false);
                    message2.AppendParameter(MessageColor, false);
                    return message2;
                }


                        

                        
            }
            ServerMessage message3 = new ServerMessage(FlagcodesOut.USER_CHAT, 0, false);
            message3.AppendParameter(ActorId, false);
            message3.AppendParameter(MessageText, false);
            message3.AppendParameter(MessageColor, false);
            return message3;
        }
    }
}

