using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Sessions;
using Snowlight.Game.Characters;

namespace Snowlight.Communication.Outgoing.Laptop
{
    class LaptopSearchResultComposer
    {
        public static ServerMessage Compose(CharacterInfo Info)
        {
            ServerMessage message = new ServerMessage(Opcodes.LAPTOPSEARCHBUDDY);
            if (Info != null)
            {
                message.AppendParameter(1, false);
                message.AppendParameter(Info.Id, false);
                message.AppendParameter(Info.Username, false);
                message.AppendParameter(Info.AvatarType, false);
                message.AppendParameter(Info.AvatarColors, false);
                message.AppendParameter(Info.HasLinkedSession ? -1 : -2, false);
                message.AppendParameter(Info.HasLinkedSession ? -1 : -2, false);
                message.AppendParameter(Info.HasLinkedSession ? 0 : -2, false);
                message.AppendParameter(Info.HasLinkedSession ? 0 : -2, false);
                message.AppendParameter(Info.HasLinkedSession ? -1 : -2, false);
                message.AppendParameter(Info.HasLinkedSession ? 0 : -2, false);
                if (Info.HasLinkedSession)
                {
                    Session sessionByCharacterId = SessionManager.GetSessionByCharacterId(Info.Id);
                    message.AppendParameter(sessionByCharacterId.SpaceJoined ? sessionByCharacterId.AbsoluteSpaceName : "Flower Power", false);
                }
                else
                {
                    message.AppendParameter(UnixTimestamp.GetDateTimeFromUnixTimestamp(Info.TimestampLastOnline).ToString("yyyy-MM-dd HH:mm:ss"), false);
                }
                message.AppendParameter(Info.Age, false);
                message.AppendParameter(Info.City, false);
                return message;
            }
            message.AppendParameter(0, false);
            return message;
        }
    }
}
