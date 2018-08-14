using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Spaces;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceLoadPublicSceneComposer
    {
        public static ServerMessage Compose(SpaceInfo Info)
        {
            ServerMessage message = new ServerMessage(Opcodes.SPACESENTERSCENE);
            message.AppendParameter(true, false);
            message.AppendParameter(true, false);
            message.AppendParameter(true, false);
            message.AppendParameter(false, false);
            message.AppendParameter(false, false);
            message.AppendParameter(Info.UInt32_0, false);
            message.AppendParameter((Info.ParentId <= 0) ? Info.UInt32_0 : Info.ParentId, false);
            message.AppendParameter(Info.Name, false);
            message.AppendParameter(true, false);
            return message;
        }
    }
}
