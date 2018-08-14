namespace BoomBang.Game.Games
{
    using BoomBang.Communication;
    using BoomBang.Communication.Incoming;
    using BoomBang.Communication.Outgoing;
    using BoomBang.Game.Sessions;
    using System;

    public static class GameHandler
    {
        public static void Initialize()
        {
            DataRouter.RegisterHandler(FlagcodesIn.GAMES, ItemcodesIn.GAME_DESC, new ProcessRequestCallback(GameHandler.smethod_0), false);
        }

        private static void smethod_0(Session session_0, ClientMessage clientMessage_0)
        {
            uint gameId = clientMessage_0.ReadUnsignedInteger();
            session_0.SendData(GameDescriptionComposer.Compose(gameId), false);
        }
    }
}

