namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.Contests;
    using System;

    public static class SpaceFallingItemComposer
    {
        public static ServerMessage Compose(ContestItem Item)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.SPACE_ITEM, ItemcodesOut.SPACE_ITEM_ADD, false);
            message.AppendParameter(Item.UInt32_0, false);
            message.AppendParameter(Item.SpaceId, false);
            message.AppendParameter(Item.Position.Int32_0, false);
            message.AppendParameter(Item.Position.Int32_1, false);
            message.AppendParameter(Item.DefinitionId, false);
            message.AppendParameter(Item.CatchType, false);
            message.AppendParameter(Item.FallType, false);
            return message;
        }
    }
}

