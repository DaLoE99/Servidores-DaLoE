namespace ns0
{
    using BoomBang.Communication;
    using BoomBang.Communication.Incoming;
    using BoomBang.Config;
    using BoomBang.Game.Sessions;
    using System;
    using System.Security.Cryptography;

    internal class Class1
    {
        public static void smethod_0()
        {
            DataRouter.RegisterHandler(FlagcodesIn.USER, ItemcodesIn.LANDING_LOGIN, new ProcessRequestCallback(Class1.smethod_1), true);
        }

        private static void smethod_1(Session session_0, ClientMessage clientMessage_0)
        {
            string username = clientMessage_0.ReadString();
            string password = clientMessage_0.ReadString();
            session_0.TryAuthenticate(username, password, session_0.RemoteAddress, false);
        }
    }
}

