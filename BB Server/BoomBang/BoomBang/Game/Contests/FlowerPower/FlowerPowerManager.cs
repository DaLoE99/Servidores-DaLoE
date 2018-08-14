namespace BoomBang.Game.FlowerPower
{
    using BoomBang.Communication;
    using BoomBang.Communication.Incoming;
    using BoomBang.Communication.Outgoing;
    using BoomBang.Game.Misc;
    using BoomBang.Game.Sessions;
    using BoomBang.Storage;
    using System;

    public static class FlowerPowerManager
    {
        public static void Initialize()
        {
            DataRouter.RegisterHandler(FlagcodesIn.USER, ItemcodesIn.FLOWERPOWER, new ProcessRequestCallback(FlowerPowerManager.smethod_0), false);
            DataRouter.RegisterHandler(FlagcodesIn.USER, ItemcodesIn.USER_EDIT_AVATAR, new ProcessRequestCallback(FlowerPowerManager.smethod_3), false);
            DataRouter.RegisterHandler(FlagcodesIn.NEWS, ItemcodesIn.NEWS_LOAD_ARTICLES, new ProcessRequestCallback(FlowerPowerManager.smethod_1), false);
            DataRouter.RegisterHandler(FlagcodesIn.NEWS, ItemcodesIn.NEWS_SHOW_ARTICLES, new ProcessRequestCallback(FlowerPowerManager.smethod_2), false);
        }

        private static void smethod_0(Session session_0, ClientMessage clientMessage_0)
        {
            session_0.SendData(FlowerPowerLoadComposer.Compose(), false);
        }

        private static void smethod_1(Session session_0, ClientMessage clientMessage_0)
        {
            session_0.SendData(NewsInitComposer.Compose(NewsCacheManager.list_0), false);
        }

        private static void smethod_2(Session session_0, ClientMessage clientMessage_0)
        {
            clientMessage_0.ReadString();
            uint num = clientMessage_0.ReadUnsignedInteger();
            for (int i = 0; i < NewsCacheManager.list_0.Count; i++)
            {
                if (NewsCacheManager.list_0[i].UInt32_0.Equals(num))
                {
                    session_0.SendData(NewsContentComposer.Compose(NewsCacheManager.list_0[i]), false);
                }
            }
        }

        private static void smethod_3(Session session_0, ClientMessage clientMessage_0)
        {
            string colors = clientMessage_0.ReadString();
            uint type = clientMessage_0.ReadUnsignedInteger();
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                session_0.CharacterInfo.UpdateAvatar(client, type, colors);
                session_0.SendData(EditAvatarComposer.Compose(type, colors), false);
            }
        }
    }
}

