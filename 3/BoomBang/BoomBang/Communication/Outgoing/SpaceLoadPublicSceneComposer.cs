namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.Spaces;
    using System;

    public static class SpaceLoadPublicSceneComposer
    {
        public static ServerMessage Compose(SpaceInfo Info)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.SPACES, ItemcodesOut.SPACES_ENTER_SCENE, false);
            message.AppendParameter(true, false);
            message.AppendParameter(true, false);
            message.AppendParameter(true, false);
            message.AppendParameter(false, false);
            message.AppendParameter(false, false);
            message.AppendParameter(Info.UInt32_0, false);
            message.AppendParameter((Info.ParentId <= 0) ? Info.UInt32_0 : Info.ParentId, false);
            message.AppendParameter(Info.Name, false);
            message.AppendParameter(true, false);
            return message;
        }
    }
}

