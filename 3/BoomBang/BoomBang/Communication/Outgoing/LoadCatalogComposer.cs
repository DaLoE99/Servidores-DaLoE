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
            message.AppendParameter("8³²cactus_new³²-1³²20³²1³²7F471309A21D06B5DA³²125,41,50,29,17,64,2,84,86³²0.7³²1³²0.5³²0,0³²0,0³²0,0³²0³²0³²0³²1³²0³²0³2³²1³²0³²1³²-1³²1³²1³²1");
            return message;
        }
    }
}

