namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class ConfirmationCatalogComposer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.CATALOG, ItemcodesOut.CATALOG_LOAD_CONFIRMATION, false);
            message.AppendNullParameter(false);
            return message;
        }
    }
}

