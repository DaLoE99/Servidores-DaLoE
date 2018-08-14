namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class CharacterCoinsComposer
    {
        public static ServerMessage AddGoldCompose(uint Amount)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_GOLD_CREDITS_ADD, 0, false);
            message.AppendParameter(Amount, false);
            return message;
        }

        public static ServerMessage AddSilverCompose(uint Amount)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_SILVER_CREDITS_ADD, 0, false);
            message.AppendParameter(Amount, false);
            return message;
        }

        public static ServerMessage RemoveGoldCompose(uint Amount)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_GOLD_CREDITS_REMOVE, 0, false);
            message.AppendParameter(Amount, false);
            return message;
        }

        public static ServerMessage RemoveSilverCompose(uint Amount)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER_SILVER_CREDITS_REMOVE, 0, false);
            message.AppendParameter(Amount, false);
            return message;
        }
    }
}

