using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Communication.Outgoing.Users
{
    class CharacterCoinsComposer
    {
        public static ServerMessage AddGoldCompose(uint Amount)
        {
            ServerMessage message = new ServerMessage(Opcodes.USERGOLDCREDITSADD);
            message.AppendParameter(Amount, false);
            return message;
        }

        public static ServerMessage AddSilverCompose(uint Amount)
        {
            ServerMessage message = new ServerMessage(Opcodes.USERSILVERCREDITSADD);
            message.AppendParameter(Amount, false);
            return message;
        }

        public static ServerMessage RemoveGoldCompose(uint Amount)
        {
            ServerMessage message = new ServerMessage(Opcodes.USERGOLDCREDITSREMOVE);
            message.AppendParameter(Amount, false);
            return message;
        }

        public static ServerMessage RemoveSilverCompose(uint Amount)
        {
            ServerMessage message = new ServerMessage(Opcodes.USERSILVERCREDITSREMOVE);
            message.AppendParameter(Amount, false);
            return message;
        }
    }
}
