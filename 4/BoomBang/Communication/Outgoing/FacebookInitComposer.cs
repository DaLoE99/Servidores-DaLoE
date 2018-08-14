namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class FacebookInitComposer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(120, 145, true);
            message.AppendParameter(true, false);
            message.AppendParameter("135271416547234", false);
            message.AppendParameter("http://fbproxy.boombang.tv/esp/login/popup/", false);
            message.AppendParameter("http://bcn3.boombang.tv/facebook/connect.php", false);
            message.AppendParameter("user_birthday", false);
            message.AppendParameter("ITWfwzOB_7PzWOMHNTETpw==", false);
            return message;
        }
    }
}

