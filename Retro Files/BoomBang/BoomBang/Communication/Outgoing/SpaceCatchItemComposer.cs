namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceCatchItemComposer
    {
        public static ServerMessage Compose(uint ItemId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.SPACE_ITEM, ItemcodesOut.SPACE_ITEM_CATCH, false);
            message.AppendParameter(ItemId, false);
            message.AppendParameter(true, false);
            return message;
        }
    }
}

