namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceRemoveItemComposer
    {
        public static ServerMessage Compose(uint ItemId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.SPACE_ITEM, ItemcodesOut.SPACE_ITEM_REMOVE, false);
            message.AppendParameter(true, false);
            message.AppendParameter(ItemId, false);
            return message;
        }
    }
}

