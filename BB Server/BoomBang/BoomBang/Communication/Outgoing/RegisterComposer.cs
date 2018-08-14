namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.Characters;
    using System;

    public static class RegisterComposer
    {
        public static ServerMessage Compose(CharacterInfo Info)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER, ItemcodesOut.LANDING_LOGIN, false);
            message.AppendParameter(true, false);
            message.AppendParameter(Info.Username, false);
            message.AppendParameter(Info.AvatarType, false);
            message.AppendParameter(Info.AvatarColors, false);
            message.AppendParameter(Info.Email, false);
            message.AppendParameter(Info.Age, false);
            message.AppendParameter(Info.MonthsRegistered, false);
            message.AppendParameter(Info.Motto, false);
            message.AppendParameter(Info.Vip, false);
            message.AppendParameter(Info.UInt32_0, false);
            message.AppendParameter(Info.Staff, false);
            message.AppendParameter(Info.GoldCoins, false);
            message.AppendParameter(Info.SilverCoins, false);
            message.AppendParameter(200, false);
            message.AppendParameter(5, false);
            message.AppendParameter(0, false);
            message.AppendParameter(-1, false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendParameter(0, false);
            message.AppendParameter("ES", false);
            message.AppendParameter(false, false);
            message.AppendParameter(false, false);
            message.AppendParameter(false, false);
            message.AppendNullParameter(false);
            message.AppendParameter(false, false);
            message.AppendParameter(false, false);
            message.AppendParameter(0x21, false);
            message.AppendParameter(false, false);
            message.AppendParameter(true, true);
            message.AppendParameter(false, false);
            message.AppendParameter(false, false);
            return message;
        }
    }
}

