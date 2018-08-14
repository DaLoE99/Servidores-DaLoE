namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceUserCancelActionComposer
    {
        public static ServerMessage SenderCompose(uint SenderId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_INTERACT, ItemcodesOut.USER_INTERACT_CANCEL, false);
            message.AppendParameter(SenderId, false);
            return message;
        }

        public static ServerMessage TargetCompose(uint TargetId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_INTERACT, ItemcodesOut.TARGETUSER_INTERACT_CANCEL, false);
            message.AppendParameter(TargetId, false);
            return message;
        }
    }
}

