namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class SpaceFullComposer
    {
        public static ServerMessage Compose()
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.SPACES, ItemcodesOut.SPACES_ENTER_SCENE, false);
            message.AppendParameter(-1, false);
            return message;
        }
    }
}

