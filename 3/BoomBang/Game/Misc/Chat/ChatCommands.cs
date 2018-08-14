using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Communication.Outgoing;
using Snowlight.Game.Sessions;
using Snowlight.Game.Spaces;
using Snowlight.Game.Characters;
using Snowlight.Communication.Outgoing.Chat;
using Snowlight.Util;

namespace Snowlight.Game.Misc.Chat
{
    class ChatCommands
    {
        public static bool HandleCommand(Session Session, string Input)
        {
            Input = Input.Substring(1, Input.Length - 1);
            string[] input = Input.Split(new char[] { ' ' });
            SpaceInstance instanceBySpaceId = SpaceManager.GetInstanceBySpaceId(Session.CurrentSpaceId);
            SpaceActor actor = (instanceBySpaceId == null) ? null : instanceBySpaceId.GetActorByReferenceId(Session.CharacterId, SpaceActorType.UserCharacter);
            CharacterInfo referenceObject = (CharacterInfo)actor.ReferenceObject;
            switch (input[0])
            {
                case "alerta":
                    if (referenceObject.Staff == 1)
                    {
                        string messageText = InputFilter.FilterString(InputFilter.MergeString(input, 1), false);
                        instanceBySpaceId.BroadcastMessage(SpaceChatComposer.Compose(0, messageText, 3, ChatType.Say), 0, false);
                        return true;
                    }
                    return false;

                     case "effect":
                    if (referenceObject.Vip == 1)
                    {
                        Input = Input.Replace("effect", "");
                        Input = Input.Replace(" ", "");

                        switch (int.Parse(Input))
                        {
                            case 1:
                                actor.ApplyEffect(1, true);
                                actor.UpdateNeeded = true;
                                break;
                            case 2:
                                actor.ApplyEffect(2, true);
                                actor.UpdateNeeded = true;
                                break;
                            case 3:
                                actor.ApplyEffect(3, true);
                                actor.UpdateNeeded = true;
                                break;
                            case 4:
                                actor.ApplyEffect(4, true);
                                actor.UpdateNeeded = true;
                                break;
                            case 5:
                                actor.ApplyEffect(5, true);
                                actor.UpdateNeeded = true;
                                break;
                            case 6:
                                actor.ApplyEffect(6, true);
                                actor.UpdateNeeded = true;
                                break;
                            case 7:
                                actor.ApplyEffect(7, true);
                                actor.UpdateNeeded = true;
                                break;
                            case 8:
                                actor.ApplyEffect(8, true);
                                actor.UpdateNeeded = true;
                                break;
                        }

                        return true;
                    }
                    return false;
            }
            return false;
        }
    }
}
