namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using BoomBang.Game.Characters;
    using System;

    public static class RegisterComposer
    {
        public static ServerMessage Compose(CharacterInfo Info)
        {
            ServerMessage message = new ServerMessage(120, 121, false);
//³²1³²daloe223³²1³²FFD797CC5806FFFFFF6633000066CCFFFFFF000000³²a@ee.ii³²33³²2³²Boombang³²0³²7196398³²0³²0³²200³²200³²5³²0³²-1³²³²³²1³²ES³²0³²0³²0³²³²0³²0³²0³²0³²1³0³²0³²0³²°
            message.AppendParameter(true, false);
            message.AppendParameter(Info.Username, false);
            message.AppendParameter(Info.AvatarType, false);
            message.AppendParameter(Info.AvatarColors, false);
            message.AppendParameter(Info.Email, false);
            message.AppendParameter(Info.Age, false);
            message.AppendParameter(Info.MonthsRegistered, false);
            message.AppendParameter("España", false);//Ciudad
            message.AppendParameter(Info.Vip, false);
            message.AppendParameter(Info.UInt32_0, false);//³²7196398³²
            message.AppendParameter(Info.Staff, false);
            message.AppendParameter(Info.GoldCoins, false);
            message.AppendParameter(Info.SilverCoins, false);
            message.AppendParameter(200, false);
            message.AppendParameter(5, false);
            message.AppendParameter(0, false);
            message.AppendParameter(-1, false);
            message.AppendNullParameter(false);
            message.AppendNullParameter(false);
            message.AppendParameter(1, false);
            message.AppendParameter("ES", false);
            message.AppendParameter(false, false);
            message.AppendParameter(false, false);
            message.AppendParameter(false, false);
            message.AppendNullParameter(false);
            message.AppendParameter(false, false);
            message.AppendParameter(false, false);
            message.AppendParameter(false, false);
            message.AppendParameter(false, false);
            message.AppendParameter("1³0", true);
            message.AppendParameter(false, false);
            message.AppendParameter(false, false);
            return message;
        }
    }
}

