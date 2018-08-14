namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class FacebookInitComposer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER, ItemcodesOut.LANDING_FACEBOOK_INIT, true);
            message.AppendParameter(true, false);
            message.AppendParameter("135271416547234", false);
            message.AppendParameter("http://esp.mus.boombang.tv/facebook/connect.php", false);
            message.AppendParameter("user_birthday", false);
            message.AppendParameter("Xueb4DJCL9cEDRZ1fwwqXg==", false);
            return message;
        }
    }
}

