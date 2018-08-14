namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class FlowerPowerLoadComposer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER, ItemcodesOut.FLOWERPOWER);
            message.AppendParameter(16, false);
            message.AppendParameter(-1, false);
            message.AppendParameter(-1, false);
            message.AppendParameter(25, false);
            return message;
        }
    }
}
