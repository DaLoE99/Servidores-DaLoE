namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class CreateZoneComposer
    {
        public static ServerMessage Compose(uint IslandId, uint OwnerId, uint ZoneId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.ISLANDS, ItemcodesOut.ISLAND_CREATE_ZONE, false);
            message.AppendParameter(0, false);
            message.AppendParameter(0, false);
            message.AppendParameter(IslandId, false);
            message.AppendParameter(OwnerId, false);
            message.AppendParameter(ZoneId, false);
            return message;
        }
    }
}

