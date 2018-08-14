namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class LaptopAcceptFriendComposer
    {
        public static ServerMessage Compose(uint uint_0)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.LAPTOP, ItemcodesOut.LAPTOP_ADD_FRIEND, false);
            message.AppendParameter(true, false);
            message.AppendParameter(uint_0, false);
            return message;
        }
    }
}

