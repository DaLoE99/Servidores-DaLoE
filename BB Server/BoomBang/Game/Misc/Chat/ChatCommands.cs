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
            }
            return false;
        }
    }
}
