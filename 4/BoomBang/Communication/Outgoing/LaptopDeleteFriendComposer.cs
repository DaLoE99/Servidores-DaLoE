namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class LaptopDeleteFriendComposer
    {
        public static ServerMessage Compose(uint FriendId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.LAPTOP, ItemcodesOut.LAPTOP_FRIEND_DELETE, false);
            message.AppendParameter(FriendId, false);
            return message;
        }
    }
}

