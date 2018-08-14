namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.Spaces;
    using System;

    public static class SpaceUserAcceptInteract
    {
        public static ServerMessage Compose(SpaceActor Actor, SpaceActor TargetActor, uint ActionId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_INTERACT, ItemcodesOut.USER_INTERACT_ACCEPT, false);
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

