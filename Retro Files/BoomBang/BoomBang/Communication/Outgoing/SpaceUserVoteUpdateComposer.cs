namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceUserVoteUpdateComposer
    {
        public static ServerMessage Compose(uint ActorId, uint ColorId, int Vote)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_VOTES, 0, false);
            message.AppendParameter(ActorId, false);
            message.AppendParameter(ColorId, false);
            message.AppendParameter(Vote, false);
            return message;
        }
    }
}

