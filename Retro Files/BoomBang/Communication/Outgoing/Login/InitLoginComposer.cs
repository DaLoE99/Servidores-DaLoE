using System;

namespace Snowlight.Communication.Outgoing
{
    public static class InitLoginComposer
    {
        public static ServerMessage Compose()
        {
            //±x³‘³x³²1³²135271416547234³²http://esp.mus.boombang.tv/facebook/connect.php³²user_birthday³²+aLU7i7v0WmIsICjU+57EA==³²°
            ServerMessage message = new ServerMessage(Opcodes.RFACE);
            message.Append(1);
            message.Append(1);
            message.Append(135271416547234);
            message.Append("http://esp.mus.boombang.tv/facebook/connect.php³²user_birthday³²+aLU7i7v0WmIsICjU+57EA==");

            return message;
        }
    }
}
