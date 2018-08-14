namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;
    using System.Runtime.InteropServices;

    public static class CheckUsernameComposer
    {
        public static ServerMessage Compose(bool Result, string[] RandomUsername = null)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER, ItemcodesOut.REGISTER_CHECK_NAME, false);
            if (Result)
            {
                message.AppendParameter(Result, false);
                message.AppendParameter(RandomUsername[0], false);
                message.AppendParameter(RandomUsername[1], false);
                message.AppendParameter(RandomUsername[2], false);
                message.AppendParameter(RandomUsername[3], false);
                return message;
            }
            message.AppendParameter(2, false);
            return message;
        }
    }
}
/*[Server] [120/139] > ±x³‹³²3rgtf35tgf³²°
[Client] [120/139] > ±x³‹³²2³²°
[Server] [120/139] > ±x³‹³²daloe³²°
[Client] [120/139] > ±x³‹³²1³²daloe1091³²daloe2921³²daloe3202³²daloe3323³²daloe3808³²daloe4632³²daloe4858³²daloe5203³²daloe5352³²daloe6821³²daloe7268³²daloe7771³²daloe7792³²daloe8804³²daloe9501³²°
*/

