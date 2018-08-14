namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

     class SpaceUserRemovedComposer
    {
        public static ServerMessage BroadcastCompose(uint ActorId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.SPACES, ItemcodesOut.SPACES_EXIT_BROADCAST);
            message.AppendParameter(ActorId, false);
            return message;
        }

        public static ServerMessage SingleCompose()
        {
            return new ServerMessage(FlagcodesOut.SPACES, ItemcodesOut.SPACES_EXIT_LAND);
        }
    }
}

   

