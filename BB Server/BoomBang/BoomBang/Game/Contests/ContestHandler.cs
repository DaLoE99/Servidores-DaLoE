namespace BoomBang.Game.Contests
{
    using BoomBang.Communication;
    using BoomBang.Communication.Incoming;
    using BoomBang.Communication.Outgoing;
    using BoomBang.Game.Sessions;
    using System;

    public static class ContestHandler
    {
        public static void Initialize()
        {
            DataRouter.RegisterHandler(FlagcodesIn.USER, ItemcodesIn.CONTEST_INIT, new ProcessRequestCallback(ContestHandler.OnContestInit), false);
        }

        public static void OnContestInit(Session Session, ClientMessage Message)
        {
            Session.SendData(ContestInitComposer.Compose("default"), false);
        }
    }
}

