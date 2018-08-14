using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Spaces;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceUserAcceptInteract
    {
        public static ServerMessage Compose(SpaceActor Actor, SpaceActor TargetActor, uint ActionId)
        {
            ServerMessage message = new ServerMessage(Opcodes.USERINTERACTACCEPT);
            message.AppendParameter(ActionId, false);
            message.AppendParameter(Actor.UInt32_0, false);
            message.AppendParameter(Actor.Position.Int32_0, false);
            message.AppendParameter(Actor.Position.Int32_1, false);
            message.AppendParameter(TargetActor.UInt32_0, false);
            message.AppendParameter(TargetActor.Position.Int32_0, false);
            message.AppendParameter(TargetActor.Position.Int32_1, false);
            return message;
        }
    }
}
