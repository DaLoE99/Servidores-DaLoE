namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceUserSendActionComposer
    {
        public static ServerMessage Compose(uint ActionId, uint SenderId)
        {
            ServerMessage message = new ServerMessage(0x89, 120, false);
            message.AppendParameter(ActionId, false);
            message.AppendParameter(SenderId, false);
            return message;
        }
    }
}

