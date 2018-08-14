namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceUserSendCoconut
    {
        public static ServerMessage SendAndBlock(uint TargetId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.SEND_COCONUT, 120, false);
            message.AppendParameter(TargetId, false);
            message.AppendParameter(0, false);
            message.AppendParameter(0x23, false);
            return message;
        }

        public static ServerMessage Unlock(uint TargetId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.SEND_COCONUT, 0x79, false);
            message.AppendParameter(TargetId, false);
            message.AppendParameter(-1, false);
            message.AppendParameter(0x23, false);
            return message;
        }
    }
}

