namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceUserActionComposer
    {
        public static ServerMessage Compose(uint CharacterId, uint ActionId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_ACTIONS, 0, false);
            message.AppendParameter(ActionId, false);
            message.AppendParameter(CharacterId, false);
            return message;
        }
    }
}

