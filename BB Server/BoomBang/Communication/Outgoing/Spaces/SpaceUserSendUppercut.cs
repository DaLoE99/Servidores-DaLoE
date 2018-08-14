using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Spaces;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceUserSendUppercut
    {
        public static ServerMessage Compose(SpaceActor Actor, SpaceActor TargetActor)
        {
            ServerMessage message = new ServerMessage(Opcodes.SENDUPPERCUT);
            message.AppendParameter(4, false);
            message.AppendParameter(1, false);
            message.AppendParameter(Actor.UInt32_0, false);
            message.AppendParameter(Actor.Position.Int32_0, false);
            message.AppendParameter(Actor.Position.Int32_1, false);
            message.AppendParameter(1, false);
            message.AppendParameter(TargetActor.UInt32_0, false);
            message.AppendParameter(TargetActor.Position.Int32_0, false);
            message.AppendParameter(TargetActor.Position.Int32_1, false);
            return message;
        }

        public static ServerMessage RemoveComposer()
        {
            return new ServerMessage(Opcodes.REMOVEUPPERCUT);
        }
    }
}
