namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class AuthenticationKoComposer
    {
        public static ServerMessage Compose(bool KoType)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER, ItemcodesOut.LANDING_LOGIN, false);
            message.AppendParameter(KoType ? 2 : 0, false);
            return message;
        }
    }
}

