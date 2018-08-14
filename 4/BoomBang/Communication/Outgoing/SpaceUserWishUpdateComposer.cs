namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceUserWishUpdateComposer
    {
        public static ServerMessage Compose(uint ActorId, uint LabelId, string Wish)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_WISHES, 0, false);
            message.AppendParameter(ActorId, false);
            message.AppendParameter(LabelId, false);
            message.AppendParameter(Wish, false);
            return message;
        }
    }
}

