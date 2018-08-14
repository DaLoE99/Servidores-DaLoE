using System;

namespace Snowlight.Communication.Outgoing
{
    public static class PingComposer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(Opcodes.PING);
            message.Append(20);
            return message;
        }
    }
}
