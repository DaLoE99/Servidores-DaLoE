namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class LoaderAdvertisementComposer
    {
        public static ServerMessage Compose(bool IsActive)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER, ItemcodesOut.BANNER);
            message.AppendParameter(IsActive);
            return message;
        }
    }
}