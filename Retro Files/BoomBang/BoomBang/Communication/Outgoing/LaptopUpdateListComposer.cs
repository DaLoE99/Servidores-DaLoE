namespace BoomBang.Communication.Outgoing
{
    using BoomBang;
    using BoomBang.Communication;
    using BoomBang.Game.Sessions;
    using System;
    using System.Collections.Generic;

    public static class LaptopUpdateListComposer
    {
        public static ServerMessage Compose(List<LaptopUpdate> Updates)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.LAPTOP, ItemcodesOut.LAPTOP_UPDATE_LAPTOP, false);
            if (Updates.Count > 0)
            {
                foreach (LaptopUpdate update in Updates)
                {
                    message.AppendParameter(update.CharacterInfo.UInt32_0, false);
                    if (update.Mode != -1)
                    {
                        message.AppendParameter(update.CharacterInfo.HasLinkedSession ? 1 : 0, false);
                        message.AppendParameter(-1, false);
                        message.AppendParameter(-1, false);
                        message.AppendParameter(0, false);
                        message.AppendParameter(0, false);
                        message.AppendParameter(-1, false);
                        message.AppendParameter(update.CharacterInfo.HasLinkedSession ? 0 : -1, false);
                        if (update.CharacterInfo.HasLinkedSession)
                        {
                            Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(update.CharacterInfo.UInt32_0);
                            message.AppendParameter(sessionByCharacterId.SpaceJoined ? sessionByCharacterId.AbsoluteSpaceName : "Flower Power", false);
                        }
                        else
                        {
                            message.AppendParameter(UnixTimestamp.GetDateTimeFromUnixTimestamp(update.CharacterInfo.TimestampLastOnline).ToString("yyyy-MM-dd HH:mm:ss"), false);
                        }
                    }
                }
            }
            return message;
        }
    }
}

