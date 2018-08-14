namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class EditAvatarComposer
    {
        public static ServerMessage Compose(uint AvatarType, string Colors)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER, ItemcodesOut.USER_EDIT_AVATAR, false);
            message.AppendParameter(true, false);
            message.AppendParameter(AvatarType, false);
            message.AppendParameter(Colors, false);
            return message;
        }
    }
}

