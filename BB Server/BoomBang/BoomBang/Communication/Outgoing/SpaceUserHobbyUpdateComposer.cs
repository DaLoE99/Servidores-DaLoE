namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceUserHobbyUpdateComposer
    {
        public static ServerMessage Compose(uint ActorId, uint LabelId, string Hobby)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_HOBBYS, 0, false);
            message.AppendParameter(ActorId, false);
            message.AppendParameter(LabelId, false);
            message.AppendParameter(Hobby, false);
            return message;
        }
    }
}

