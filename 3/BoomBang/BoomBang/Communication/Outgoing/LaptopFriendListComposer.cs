namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.Characters;
    using BoomBang.Storage;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public static class LaptopFriendListComposer
    {
        public static ServerMessage Compose(ReadOnlyCollection<uint> Friends, List<uint> Requests)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.LAPTOP, ItemcodesOut.LAPTOP_LOAD_FRIENDS, false);
            message.AppendParameter((int) (Friends.Count + Requests.Count), false);
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                foreach (uint num in Friends)
                {
                    CharacterInfo characterInfo = CharacterInfoLoader.GetCharacterInfo(client, num);
                    if (characterInfo != null)
                    {
                        message.AppendParameter(characterInfo.UInt32_0, false);
                        message.AppendParameter(characterInfo.Username, false);
                        message.AppendParameter(characterInfo.Motto, false);
                        message.AppendParameter(characterInfo.AvatarType, false);
                        message.AppendParameter(characterInfo.AvatarColors, false);
                        message.AppendParameter(characterInfo.Age, false);
                        message.AppendParameter(characterInfo.City, false);
                        message.AppendNullParameter(false);
                        message.AppendParameter(1, false);
                        message.AppendParameter(false, false);
                    }
                }
                foreach (uint num2 in Requests)
                {
                    CharacterInfo info2 = CharacterInfoLoader.GetCharacterInfo(client, num2);
                    if (info2 != null)
                    {
                        message.AppendParameter(info2.UInt32_0, false);
                        message.AppendParameter(info2.Username, false);
                        message.AppendParameter(info2.Motto, false);
                        message.AppendParameter(info2.AvatarType, false);
                        message.AppendParameter(info2.AvatarColors, false);
                        message.AppendParameter(info2.Age, false);
                        message.AppendParameter(info2.City, false);
                        message.AppendNullParameter(false);
                        message.AppendParameter(1, false);
                        message.AppendParameter(true, false);
                    }
                }
            }
            return message;
        }
    }
}

