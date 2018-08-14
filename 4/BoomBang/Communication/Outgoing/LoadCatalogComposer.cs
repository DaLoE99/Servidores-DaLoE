namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;
    using System.Collections.Generic;

    public  class LoadCatalogComposer
    {
        public static ServerMessage Compose(int num, string packet)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.CATALOG, ItemcodesOut.CATALOG_LOAD_ITEMS, false);
            message.AppendParameter(num);
            message.AppendParameter("");
            return message;
        }
    }
}

