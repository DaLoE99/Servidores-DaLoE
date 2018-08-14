using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Characters;
using Snowlight.Game.Spaces;

namespace Snowlight.Communication.Outgoing.Spaces
{
    class SpaceObjectListComposer
    {
        public static ServerMessage BroadcastCompose(SpaceActor Actor)
        {
            ServerMessage message = new ServerMessage(Opcodes.SPACESPOSTUSER);
            CharacterInfo referenceObject = (CharacterInfo)Actor.ReferenceObject;
            message.AppendParameter(Actor.UInt32_0, false);
            message.AppendParameter(Actor.Name, false);
            message.AppendParameter(Actor.AvatarType, false);
            message.AppendParameter(Actor.AvatarColor, false);
            message.AppendParameter(Actor.Position.Int32_0, false);
            message.AppendParameter(Actor.Position.Int32_1, false);
            message.AppendParameter(Actor.Rotation, false);
            message.AppendParameter(referenceObject.City, false);
            message.AppendParameter(referenceObject.Age, false);
            message.AppendParameter(referenceObject.MonthsRegistered, false);
            message.AppendParameter(false, false);
            message.AppendParameter(string.Empty, false);
            message.AppendParameter(referenceObject.RingLevel, false);
            message.AppendParameter(referenceObject.RingLevel, false);
            message.AppendParameter(referenceObject.CocoLevel, false);
            message.AppendParameter(referenceObject.NinjaLevel, false);
            message.AppendParameter(referenceObject.Hobby1, true);
            message.AppendParameter(referenceObject.Hobby2, true);
            message.AppendParameter(referenceObject.Hobby3, false);
            message.AppendParameter(referenceObject.Wish1, true);
            message.AppendParameter(referenceObject.Wish2, true);
            message.AppendParameter(referenceObject.Wish3, false);
            message.AppendParameter(referenceObject.BlueVotes, true);
            message.AppendParameter(referenceObject.GreenVotes, true);
            message.AppendParameter(referenceObject.OrangeVotes, false);
            message.AppendParameter(referenceObject.Motto, false);
            message.AppendParameter(referenceObject.SentKisses, true);
            message.AppendParameter(referenceObject.ReceivedKisses, true);
            message.AppendParameter(referenceObject.SentCocktails, true);
            message.AppendParameter(referenceObject.ReceivedCocktails, true);
            message.AppendParameter(referenceObject.SentRoses, true);
            message.AppendParameter(referenceObject.ReceivedRoses, true);
            message.AppendParameter(referenceObject.SentUppercuts, true);
            message.AppendParameter(referenceObject.ReceivedUppercuts, true);
            message.AppendParameter(referenceObject.SentCoconuts, true);
            message.AppendParameter(referenceObject.ReceivedCoconuts, true);
            message.AppendParameter("1\x00b30\x00b30\x00b31\x00b3-1\x00b30\x00b30\x00b30\x00b31\x00b30\x00b30\x00b35\x00b30\x00b30\x00b30\x00b3200", false);
            message.AppendParameter(referenceObject.Staff, false);
            message.AppendParameter(referenceObject.Vip, false);
            message.AppendParameter(referenceObject.AllowChanges, false);
            message.AppendParameter((Actor.AvatarEffectId <= 0) ? -1 : Actor.AvatarEffectId, false);
            message.AppendParameter(Actor.ReferenceId, false);
            return message;
        }

        public static ServerMessage SingleCompose(List<SpaceActor> ActorContainer)
        {
            ServerMessage message = new ServerMessage(Opcodes.SPACESLOADUSER);
            message.AppendBytes(new byte[] { 120, 0xb3, 0xb2 });
            message.AppendParameter(false, false);
            foreach (SpaceActor actor in ActorContainer)
            {
                CharacterInfo referenceObject = (CharacterInfo)actor.ReferenceObject;
                message.AppendParameter(actor.UInt32_0, false);
                message.AppendParameter(actor.Name, false);
                message.AppendParameter(actor.AvatarType, false);
                message.AppendParameter(actor.AvatarColor, false);
                message.AppendParameter(actor.Position.Int32_0, false);
                message.AppendParameter(actor.Position.Int32_1, false);
                message.AppendParameter(actor.Rotation, false);
                message.AppendParameter(referenceObject.City, false);
                message.AppendParameter(referenceObject.Age, false);
                message.AppendParameter(referenceObject.MonthsRegistered, false);
                message.AppendParameter(false, false);
                message.AppendNullParameter(false);
                message.AppendParameter(referenceObject.RingLevel, false);
                message.AppendParameter(referenceObject.RingLevel, false);
                message.AppendParameter(referenceObject.CocoLevel, false);
                message.AppendParameter(referenceObject.NinjaLevel, false);
                message.AppendParameter(referenceObject.Hobby1, true);
                message.AppendParameter(referenceObject.Hobby2, true);
                message.AppendParameter(referenceObject.Hobby3, false);
                message.AppendParameter(referenceObject.Wish1, true);
                message.AppendParameter(referenceObject.Wish2, true);
                message.AppendParameter(referenceObject.Wish3, false);
                message.AppendParameter(referenceObject.BlueVotes, true);
                message.AppendParameter(referenceObject.GreenVotes, true);
                message.AppendParameter(referenceObject.OrangeVotes, false);
                message.AppendParameter(referenceObject.Motto, false);
                message.AppendParameter(referenceObject.SentKisses, true);
                message.AppendParameter(referenceObject.ReceivedKisses, true);
                message.AppendParameter(referenceObject.SentCocktails, true);
                message.AppendParameter(referenceObject.ReceivedCocktails, true);
                message.AppendParameter(referenceObject.SentRoses, true);
                message.AppendParameter(referenceObject.ReceivedRoses, true);
                message.AppendParameter(referenceObject.SentUppercuts, true);
                message.AppendParameter(referenceObject.ReceivedUppercuts, true);
                message.AppendParameter(referenceObject.SentCoconuts, true);
                message.AppendParameter(referenceObject.ReceivedCoconuts, true);
                message.AppendParameter("1\x00b30\x00b30\x00b31\x00b3-1\x00b30\x00b30\x00b30\x00b31\x00b30\x00b30\x00b35\x00b30\x00b30\x00b30\x00b3200", false);
                message.AppendParameter(referenceObject.Staff, false);
                message.AppendParameter(referenceObject.Vip, false);
                message.AppendParameter(referenceObject.AllowChanges, false);
                message.AppendParameter((actor.AvatarEffectId <= 0) ? -1 : actor.AvatarEffectId, false);
                message.AppendParameter(actor.ReferenceId, false);
            }
            return message;
        }
    }
}
