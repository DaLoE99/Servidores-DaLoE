namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.Spaces;
    using System;

    public static class SpaceUserSendUppercut
    {
        public static ServerMessage Compose(SpaceActor Actor, SpaceActor TargetActor)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.SEND_UPPERCUT, 0, false);
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
            return new ServerMessage(FlagcodesOut.UPPERCUT_REMOVE, 0, false);
        }
    }
}

