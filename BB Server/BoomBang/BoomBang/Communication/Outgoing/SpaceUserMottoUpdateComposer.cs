namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceUserMottoUpdateComposer
    {
        public static ServerMessage Compose(uint ActorId, string Motto)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_MOTTO, 0, false);
            message.AppendParameter(ActorId, false);
            message.AppendParameter(Motto, false);
            return message;
        }
    }
}

