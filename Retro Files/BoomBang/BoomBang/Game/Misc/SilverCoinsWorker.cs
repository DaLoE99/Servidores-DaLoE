namespace BoomBang.Game.Misc
{
    using BoomBang.Communication;
    using BoomBang.Communication.Incoming;
    using BoomBang.Communication.Outgoing;
    using BoomBang.Game.Characters;
    using BoomBang.Game.Sessions;
    using System;

    public static class SilverCoinsWorker
    {
        public static void Initialize()
        {
            DataRouter.RegisterHandler(FlagcodesIn.SILVER_TIMELEFT, 0, new ProcessRequestCallback(SilverCoinsWorker.OnTimeLeftRequest), false);
        }

        public static void OnTimeLeftRequest(Session Session, ClientMessage Message)
        {
            CharacterInfo characterInfo = Session.CharacterInfo;
            if (characterInfo != null)
            {
                characterInfo.TimeSinceLastActivityPointsUpdate -= 100.0;
                if (characterInfo.TimeSinceLastActivityPointsUpdate > 0.0)
                {
                    Session.SendData(SilverCoinsTimeLeftComposer.Compose((int) characterInfo.TimeSinceLastActivityPointsUpdate), false);
                }
                else
                {
                    characterInfo.TimeSinceLastActivityPointsUpdate = 900.0;
                    Session.SendData(SilverCoinsTimeLeftComposer.Compose(900), false);
                    Session.SendData(CharacterCoinsComposer.AddSilverCompose(50), false);
                }
            }
        }
    }
}

