namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class CreateIslandComposer
    {
        public static ServerMessage Compose(uint uint_0)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.ISLANDS, ItemcodesOut.ISLAND_CREATE, false);
            message.AppendParameter(uint_0, false);
            return message;
        }

        public static ServerMessage TakenNameComposer()
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.ISLANDS, ItemcodesOut.ISLAND_CREATE, false);
            message.AppendParameter(0, false);
            return message;
        }
    }
}

