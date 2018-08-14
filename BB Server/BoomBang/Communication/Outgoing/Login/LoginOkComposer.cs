using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Characters;

namespace Snowlight.Communication.Outgoing
{
    class LoginOkComposer
    {
        public static ServerMessage Compose(CharacterInfo Info)
        {
            /*
             * ±x³y³²1³²
             * .:XULO:.³²
             * 3³²
             * A17850FFCC00FF9900CC0033FF9933FFFFFF0099CC³²
             * contactorobercid@gmail.com³²
             * 18³²
             * 10³²
             * BoomBang³²
             * 1³²
             * 43554³²
             * 0³²
             * 12825³²
             * 2798³²
             * 200³²
             * 5³²
             * 0³²
             * 1800³²
             * 2³3³²
             * 2³1³²
             * 0³²
             * US³²
             * 2³²
             * 0³²
             * 0³²
             * ³²
             * 1³²
             * 1³²
             * 214³²
             * 0³²
             * 1³0³²
             * 0³²
             * 0³²°
            */
            //³²2³1³²0³²US³²2³²0³²0³²³²1³²1³²214³²0³²1³0³²0³²0³²°
            ServerMessage message = new ServerMessage(Opcodes.LOGIN);
            message.Append(1);
            message.Append(Info.Username);
            message.Append(Info.AvatarType);
            message.Append(Info.AvatarColors);
            message.Append(Info.Email);
            message.Append(Info.Age);
            message.Append(10);
            message.Append(Info.City);
            message.Append(1);
            message.Append(Info.Id);
            message.Append(Info.Staff);
            message.Append(Info.GoldCoins);
            message.Append(Info.SilverCoins);
            message.Append(200);
            message.Append(5);
            message.Append(0);
            message.Append(1800);
            message.Append("2³3");
            message.Append("2³1");
            message.Append(0);
            message.Append("ES");
            message.Append(2);
            message.Append(0);
            message.Append(0);
            message.Append("");
            message.Append(1);
            message.Append(1);
            message.Append(214);
            message.Append(0);
            message.Append(1);
            message.Append(0);
            message.Append(0);

            return message;
        }
    }
}
