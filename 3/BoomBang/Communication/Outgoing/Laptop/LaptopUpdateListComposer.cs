using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Sessions;

namespace Snowlight.Communication.Outgoing
{
    class LaptopUpdateListComposer
    {
        public static ServerMessage Compose(List<LaptopUpdate> Updates)
        {
            ServerMessage message = new ServerMessage(Opcodes.LAPTOPUPDATE);
            if (Updates.Count > 0)
            {
                foreach (LaptopUpdate update in Updates)
                {
                    message.Append(update.CharacterInfo.Id);
                    if (update.Mode != -1)
                    {
                        message.Append(update.CharacterInfo.HasLinkedSession ? 1 : 0);
                        message.Append(-1);
                        message.Append(-1);
                        message.Append(0);
                        message.Append(0);
                        message.Append(-1);
                        message.Append(update.CharacterInfo.HasLinkedSession ? 0 : -1);
                        if (update.CharacterInfo.HasLinkedSession)
                        {
                            Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(update.CharacterInfo.Id);
                            message.Append(sessionByCharacterId.SpaceJoined ? sessionByCharacterId.AbsoluteSpaceName : "Flower Power");
                        }
                        else
                        {
                            message.Append(UnixTimestamp.GetDateTimeFromUnixTimestamp(update.CharacterInfo.TimestampLastOnline).ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                }
            }
            return message;
        }
    }
}
