namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;
    using System.Runtime.InteropServices;

    public static class CheckUsernameComposer
    {
        public static ServerMessage Compose(bool Result, string[] RandomUsername = null)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER, ItemcodesOut.REGISTER_CHECK_NAME, false);
            if (Result)
            {
                message.AppendParameter(Result, false);
                message.AppendParameter(RandomUsername[0], false);
                message.AppendParameter(RandomUsername[1], false);
                message.AppendParameter(RandomUsername[2], false);
                message.AppendParameter(RandomUsername[3], false);
                return message;
            }
            message.AppendParameter(2, false);
            return message;
        }
    }
}

