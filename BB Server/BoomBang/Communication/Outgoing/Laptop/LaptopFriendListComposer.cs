using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Snowlight.Storage;
using Snowlight.Game.Characters;

namespace Snowlight.Communication.Outgoing
{
    class LaptopFriendListComposer
    {
        public static ServerMessage Compose(ReadOnlyCollection<uint> Friends, List<uint> Requests)
        {
            ServerMessage message = new ServerMessage(Opcodes.LAPTOPLOADFRIENDS);
            message.Append((int)(Friends.Count + Requests.Count));
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                foreach (uint num in Friends)
                {
                    CharacterInfo characterInfo = CharacterInfoLoader.GetCharacterInfo(client, num);
                    if (characterInfo != null)
                    {
                        message.Append(characterInfo.Id);
                        message.Append(characterInfo.Username);
                        message.Append(characterInfo.Motto);
                        message.Append(characterInfo.AvatarType);
                        message.Append(characterInfo.AvatarColors);
                        message.Append(characterInfo.Age);
                        message.Append(characterInfo.City);
                        message.Append("");
                        message.Append(1);
                        message.Append(false);
                    }
                }
                foreach (uint num2 in Requests)
                {
                    CharacterInfo info2 = CharacterInfoLoader.GetCharacterInfo(client, num2);
                    if (info2 != null)
                    {
                        message.Append(info2.Id);
                        message.Append(info2.Username);
                        message.Append(info2.Motto);
                        message.Append(info2.AvatarType);
                        message.Append(info2.AvatarColors);
                        message.Append(info2.Age);
                        message.Append(info2.City);
                        message.Append("");
                        message.Append(1);
                        message.Append(1);
                    }
                }
            }
            return message;
        }
    }
}
