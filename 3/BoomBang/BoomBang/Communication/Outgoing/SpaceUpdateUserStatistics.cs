namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.Characters;
    using BoomBang.Game.Spaces;
    using System;

    public static class SpaceUpdateUserStatistics
    {
        public static ServerMessage Compose(SpaceActor Actor, uint ActionId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.UPDATE_STATISTICS, 0, false);
            message.AppendParameter(Actor.ReferenceId, false);
            message.AppendParameter(ActionId, false);
            CharacterInfo referenceObject = (CharacterInfo) Actor.ReferenceObject;
            switch (ActionId)
            {
                case 1:
                    message.AppendParameter(referenceObject.SentKisses, false);
                    message.AppendParameter(referenceObject.ReceivedKisses, false);
                    return message;

                case 2:
                    message.AppendParameter(referenceObject.SentCocktails, false);
                    message.AppendParameter(referenceObject.ReceivedCocktails, false);
                    return message;

                case 3:
                    message.AppendParameter(referenceObject.SentRoses, false);
                    message.AppendParameter(referenceObject.ReceivedRoses, false);
                    return message;

                case 4:
                    message.AppendParameter(referenceObject.SentUppercuts, false);
                    message.AppendParameter(referenceObject.ReceivedUppercuts, false);
                    return message;

                case 5:
                    message.AppendParameter(referenceObject.SentCoconuts, false);
                    message.AppendParameter(referenceObject.ReceivedCoconuts, false);
                    return message;
            }
            message.AppendParameter(referenceObject.SentKisses, false);
            message.AppendParameter(referenceObject.ReceivedKisses, false);
            return message;
        }
    }
}

