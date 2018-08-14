namespace BoomBang.Game.Advertisements
{
    using BoomBang.Communication;
    using BoomBang.Communication.Incoming;
    using BoomBang.Communication.Outgoing;
    using BoomBang.Game.Sessions;
    using System;

    public static class AdvertisementManager
    {
        public static void Initialize()
        {
            DataRouter.RegisterHandler(FlagcodesIn.USER, ItemcodesIn.BANNER, new ProcessRequestCallback(AdvertisementManager.smethod_0), false);
        }

        private static void smethod_0(Session Session, ClientMessage Message)
        {
            Session.SendData(LoaderAdvertisementComposer.Compose(true), false);
        }
    }
}

