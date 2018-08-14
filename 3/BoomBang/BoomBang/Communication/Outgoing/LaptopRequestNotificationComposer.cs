namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.Characters;
    using System;

    public static class LaptopRequestNotificationComposer
    {
        public static ServerMessage Compose(CharacterInfo Info)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.LAPTOP, ItemcodesOut.LAPTOP_FRIEND_REQUEST, false);
            message.AppendParameter(Info.UInt32_0, false);
            message.AppendParameter(Info.Username, false);
            message.AppendParameter(Info.Motto, false);
            message.AppendParameter(Info.AvatarType, false);
            message.AppendParameter(Info.AvatarColors, false);
            message.AppendParameter(Info.Age, false);
            message.AppendParameter(Info.City, false);
            message.AppendNullParameter(false);
            message.AppendParameter(1, false);
            message.AppendParameter(true, false);
            return message;
        }
    }
}

