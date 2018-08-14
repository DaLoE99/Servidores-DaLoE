using System;

namespace Snowlight.Communication.Outgoing
{
    public static class CheckUsernameComposer
    {
        public static ServerMessage ComposeTrue()
        {
            //±x³‹³²2³²°
            ServerMessage message = new ServerMessage(Opcodes.REGISTERCHECKUSER);
            message.Append(2);

            return message;
        }
        public static ServerMessage ComposeFalse(string username)
        {
            Random Rand = new Random();
            //±x³‹³²1³²a³²a083³²sdf³²°
            ServerMessage message = new ServerMessage(Opcodes.REGISTERCHECKUSER);
            message.Append(1);
            message.Append(username + Rand.Next(1, 999));
            message.Append(username + Rand.Next(1, 999));
            message.Append(username + Rand.Next(1, 999));

            return message;
        }
    }
}
