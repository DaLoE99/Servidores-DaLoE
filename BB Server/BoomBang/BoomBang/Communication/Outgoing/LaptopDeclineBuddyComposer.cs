namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class LaptopDeclineBuddyComposer
    {
        public static ServerMessage Compose(uint CharacterId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.LAPTOP, ItemcodesOut.LAPTOP_FRIEND_DECLINE, false);
            message.AppendParameter(CharacterId, false);
            return message;
        }
    }
}

