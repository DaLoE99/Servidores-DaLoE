using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Characters;

namespace Snowlight.Communication.Outgoing.Laptop
{
    class LaptopRequestNotificationComposer
    {
        public static ServerMessage Compose(CharacterInfo Info)
        {
            ServerMessage message = new ServerMessage(Opcodes.LAPTOPFRIENREQUEST);
            message.AppendParameter(Info.Id, false);
            message.AppendParameter(Info.Username, false);
            message.AppendParameter(Info.Motto, false);
            message.AppendParameter(Info.AvatarType, false);
            message.AppendParameter(Info.AvatarColors, false);
            message.AppendParameter(Info.Age, false);
            message.AppendParameter(Info.City, false);
            message.AppendNullParameter(false);
            message.AppendParameter(1, false);
            message.AppendParameter(true, false);
            return message;
        }
    }
}
