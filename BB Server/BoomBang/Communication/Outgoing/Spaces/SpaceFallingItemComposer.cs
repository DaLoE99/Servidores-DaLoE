using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Contest;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceFallingItemComposer
    {
        public static ServerMessage Compose(ContestItem Item)
        {
            ServerMessage message = new ServerMessage(Opcodes.SPACEITEMADD);
            message.AppendParameter(Item.UInt32_0, false);
            message.AppendParameter(Item.SpaceId, false);
            message.AppendParameter(Item.Position.Int32_0, false);
            message.AppendParameter(Item.Position.Int32_1, false);
            message.AppendParameter(Item.DefinitionId, false);
            message.AppendParameter(Item.CatchType, false);
            message.AppendParameter(Item.FallType, false);
            return message;
        }
    }
}
