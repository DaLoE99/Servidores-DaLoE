namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.Navigation;
    using BoomBang.Game.Spaces;
    using System;
    using System.Collections.Generic;

    public static class NavigatorSubItemsComposer
    {
        public static ServerMessage Compose(uint CategoryId, uint MainId, List<NavigatorItem> ItemContainer)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.NAVIGATOR, ItemcodesOut.NAVIGATOR_SUBITEMS, false);
            message.AppendParameter(CategoryId, false);
            foreach (NavigatorItem item in ItemContainer)
            {
                SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(item.UInt32_0);
                if (item.SpaceId.Equals(MainId) && !item.IsCategory)
                {
                    message.AppendParameter(true, true);
                    message.AppendParameter(true, true);
                    message.AppendParameter(item.SpaceId, true);
                    message.AppendParameter(item.Name, true);
                    if (instanceBySpaceId == null)
                    {
                        message.AppendParameter(0, true);
                    }
                    else
                    {
                        message.AppendParameter((instanceBySpaceId.ActorCount > 12) ? 12 : instanceBySpaceId.HumanActorCount, true);
                    }
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(item.UInt32_0, true);
                    message.AppendParameter("-1", true);
                    message.AppendParameter(12, false);
                }
            }
            return message;
        }
    }
}
