using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Characters;
using System.Data;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class PreEnterIslandComposer
    {
        public static ServerMessage Compose(CharacterInfo Info, DataRow Island)
        {
            ServerMessage message = new ServerMessage(Opcodes.ISLANDPRE);
            message.AppendParameter((uint)Island["id"], false);
            message.AppendParameter((string)Island["nombre"], false);
            message.AppendNullParameter(false);
            message.AppendParameter((uint)Island["modelo_area"], false);
            message.AppendParameter(0, false);
            message.AppendParameter(Info.Id, false);
            message.AppendParameter(Info.Username, false);
            message.AppendParameter(Info.AvatarType, false);
            message.AppendParameter(Info.AvatarColors, false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendParameter(0, false);
            return message;
        }
    }
}
