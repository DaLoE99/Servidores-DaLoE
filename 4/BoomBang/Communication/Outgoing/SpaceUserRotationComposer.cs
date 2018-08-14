namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceUserRotationComposer
    {
        public static ServerMessage Compose(uint ActorId, int int_0, int int_1, int Rotation)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_ROTATION, 0, false);
            message.AppendParameter(ActorId, false);
            message.AppendParameter(int_0, false);
            message.AppendParameter(int_1, false);
            message.AppendParameter(Rotation, false);
            return message;
        }
    }
}

