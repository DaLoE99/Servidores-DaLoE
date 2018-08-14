using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Navigation;
using Snowlight.Game.Spaces;

namespace Snowlight.Communication.Outgoing
{
    class NavigatorItemsComposer
    {
        public static ServerMessage Message(List<NavigatorItem> ItemContainer, SpaceInfo InfoContainer, uint Category)
        {
            ServerMessage message = new ServerMessage(Opcodes.NAVIGATORITEMSLOAD);
            switch (Category)
            {
                case 1:
                    message.AppendParameter(1, false);
                    foreach (NavigatorItem item in ItemContainer)
                    {
                        uint num = 0;
                        if (item.ParentId.Equals((uint)0) && (item.Category == NavigatorCategory.Area))
                        {
                            foreach (SpaceInstance instance in SpaceManager.SpaceInstances.Values)
                            {
                                if (instance.Info.ParentId == item.UInt32_0)
                                {
                                    num += (uint)instance.HumanActorCount;
                                }
                            }
                            message.AppendParameter(1, true);
                            message.AppendParameter(1, true);
                            message.AppendParameter(item.SpaceId, true);
                            message.AppendParameter(item.Name, true);
                            message.AppendParameter(num, true);
                            message.AppendParameter(false, true);
                            message.AppendParameter(false, true);
                            message.AppendParameter(false, true);
                            message.AppendParameter("-1", true);
                            message.AppendParameter(false, false);
                        }
                    }
                    return message;

                case 2:
                    message.AppendParameter(2, false);
                    foreach (NavigatorItem item2 in ItemContainer)
                    {
                        SpaceInstance instance2 = item2.TryGetSpaceInstance();
                        message.AppendParameter(0, true);
                        message.AppendParameter(0, true);
                        message.AppendParameter(0, true);
                        message.AppendParameter(item2.Name, true);
                        if (instance2 == null)
                        {
                            message.AppendParameter(0, true);
                        }
                        else
                        {
                            message.AppendParameter(instance2.ActorCount, true);
                        }
                        message.AppendParameter(item2.UInt32_0, true);
                        message.AppendParameter(0, true);
                        message.AppendParameter(0, true);
                        message.AppendParameter(0, true);
                        message.AppendParameter(0, false);
                    }
                    return message;

                case 3:
                    message.AppendParameter(3, false);
                    foreach (NavigatorItem item3 in ItemContainer)
                    {
                        SpaceInstance instance3 = item3.TryGetSpaceInstance();
                        if (item3.ParentId.Equals((uint)0) && (item3.Category == NavigatorCategory.Game))
                        {
                            message.AppendParameter(1, true);
                            message.AppendParameter(true, true);
                            message.AppendParameter(item3.SpaceId, true);
                            message.AppendParameter(item3.Name, true);
                            if (instance3 == null)
                            {
                                message.AppendParameter(0, true);
                            }
                            else
                            {
                                message.AppendParameter(instance3.ActorCount, true);
                            }
                            message.AppendParameter(false, true);
                            message.AppendParameter(false, true);
                            message.AppendParameter(false, true);
                            message.AppendParameter("-1", true);
                            message.AppendParameter(false, false);
                        }
                    }
                    return message;

                case 4:
                    message.AppendParameter(4, false);
                    return message;
            }
            return message;
        }
    }
}
