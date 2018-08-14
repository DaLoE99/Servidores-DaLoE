namespace BoomBang.Game.Handlers
{
    using BoomBang.Communication;
    using BoomBang.Communication.Incoming;
    using BoomBang.Communication.Outgoing;
    using BoomBang.Game.Sessions;
    using System;

    public static class GlobalHandler
    {
        public static void Initialize()
        {
           DataRouter.RegisterHandler(FlagcodesIn.SESSION_PING, 0, new ProcessRequestCallback(GlobalHandler.smethod_0), true);
           DataRouter.RegisterHandler(FlagcodesIn.USER, ItemcodesIn.LANDING_FACEBOOK_INIT, new ProcessRequestCallback(GlobalHandler.smethod_1), true);
         
        }

        private static void smethod_0(Session session_0, ClientMessage clientMessage_0)
        {
            session_0.LatencyTestOk = true;
        }

        private static void smethod_1(Session session_0, ClientMessage clientMessage_0)
        {
            session_0.SendData(FacebookInitComposer.Compose(), false);
        }

        private static void smethod_2(Session session_0, ClientMessage clientMessage_0)
        {
            SessionManager.StopSession(session_0.UInt32_0);
        }
      
    }
}

