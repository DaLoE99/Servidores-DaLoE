﻿namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class ContestInitComposer
    {
        public static ServerMessage Compose(string type)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.USER, ItemcodesOut.CONTEST_INIT, false);
            switch (type)
            {
                case "Easter":
                    message.AppendParameter(1, true);
                    message.AppendParameter(2, true);
                    message.AppendParameter(3, true);
                    message.AppendParameter(4, true);
                    message.AppendParameter(5, true);
                    message.AppendParameter(6, true);
                    message.AppendParameter(0x17, true);
                    message.AppendParameter(0x51, true);
                    message.AppendParameter(0x59, true);
                    message.AppendParameter(90, true);
                    message.AppendParameter(0x5b, true);
                    message.AppendParameter(0x5c, true);
                    message.AppendParameter(0x5d, true);
                    message.AppendParameter(0x5e, true);
                    message.AppendParameter(0x5f, true);
                    message.AppendParameter(0x60, true);
                    message.AppendParameter(0x61, true);
                    message.AppendParameter(0x62, true);
                    message.AppendParameter(0x63, true);
                    message.AppendParameter(100, true);
                    message.AppendParameter(0x65, true);
                    message.AppendParameter(0x66, true);
                    message.AppendParameter(0x67, true);
                    message.AppendParameter(0x68, true);
                    message.AppendParameter(0x69, true);
                    message.AppendParameter(110, true);
                    message.AppendParameter(0x99, false);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, false);
                    return message;

                case "Aliens":
                    message.AppendParameter(1, true);
                    message.AppendParameter(2, true);
                    message.AppendParameter(3, true);
                    message.AppendParameter(4, true);
                    message.AppendParameter(5, true);
                    message.AppendParameter(6, true);
                    message.AppendParameter(0x17, true);
                    message.AppendParameter(0x51, true);
                    message.AppendParameter(0x59, true);
                    message.AppendParameter(90, true);
                    message.AppendParameter(0x5b, true);
                    message.AppendParameter(0x5c, true);
                    message.AppendParameter(0x5d, true);
                    message.AppendParameter(0x5e, true);
                    message.AppendParameter(0x5f, true);
                    message.AppendParameter(0x60, true);
                    message.AppendParameter(0x61, true);
                    message.AppendParameter(0x62, true);
                    message.AppendParameter(0x63, true);
                    message.AppendParameter(100, true);
                    message.AppendParameter(0x65, true);
                    message.AppendParameter(0x66, true);
                    message.AppendParameter(0x67, true);
                    message.AppendParameter(0x68, true);
                    message.AppendParameter(0x69, true);
                    message.AppendParameter(110, true);
                    message.AppendParameter(0x99, false);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, false);
                    return message;

                case "Halloween":
                    message.AppendParameter(1, true);
                    message.AppendParameter(2, true);
                    message.AppendParameter(3, true);
                    message.AppendParameter(4, true);
                    message.AppendParameter(5, true);
                    message.AppendParameter(6, true);
                    message.AppendParameter(0x17, true);
                    message.AppendParameter(0x51, true);
                    message.AppendParameter(0x59, true);
                    message.AppendParameter(90, true);
                    message.AppendParameter(0x5b, true);
                    message.AppendParameter(0x5c, true);
                    message.AppendParameter(0x5d, true);
                    message.AppendParameter(0x5e, true);
                    message.AppendParameter(0x5f, true);
                    message.AppendParameter(0x60, true);
                    message.AppendParameter(0x61, true);
                    message.AppendParameter(0x62, true);
                    message.AppendParameter(0x63, true);
                    message.AppendParameter(100, true);
                    message.AppendParameter(0x65, true);
                    message.AppendParameter(0x66, true);
                    message.AppendParameter(0x67, true);
                    message.AppendParameter(0x68, true);
                    message.AppendParameter(0x69, true);
                    message.AppendParameter(110, true);
                    message.AppendParameter(0x99, false);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, false);
                    return message;

                case "Oriental":
                    message.AppendParameter(1, true);
                    message.AppendParameter(2, true);
                    message.AppendParameter(3, true);
                    message.AppendParameter(4, true);
                    message.AppendParameter(5, true);
                    message.AppendParameter(6, true);
                    message.AppendParameter(0x17, true);
                    message.AppendParameter(0x51, true);
                    message.AppendParameter(0x59, true);
                    message.AppendParameter(90, true);
                    message.AppendParameter(0x5b, true);
                    message.AppendParameter(0x5c, true);
                    message.AppendParameter(0x5d, true);
                    message.AppendParameter(0x5e, true);
                    message.AppendParameter(0x5f, true);
                    message.AppendParameter(0x60, true);
                    message.AppendParameter(0x61, true);
                    message.AppendParameter(0x62, true);
                    message.AppendParameter(0x63, true);
                    message.AppendParameter(100, true);
                    message.AppendParameter(0x65, true);
                    message.AppendParameter(0x66, true);
                    message.AppendParameter(0x67, true);
                    message.AppendParameter(0x68, true);
                    message.AppendParameter(0x69, true);
                    message.AppendParameter(110, true);
                    message.AppendParameter(0x99, false);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, false);
                    return message;

                case "Christmas":
                    message.AppendParameter(1, true);
                    message.AppendParameter(2, true);
                    message.AppendParameter(3, true);
                    message.AppendParameter(4, true);
                    message.AppendParameter(5, true);
                    message.AppendParameter(6, true);
                    message.AppendParameter(0x17, true);
                    message.AppendParameter(0x51, true);
                    message.AppendParameter(0x59, true);
                    message.AppendParameter(90, true);
                    message.AppendParameter(0x5b, true);
                    message.AppendParameter(0x5c, true);
                    message.AppendParameter(0x5d, true);
                    message.AppendParameter(0x5e, true);
                    message.AppendParameter(0x5f, true);
                    message.AppendParameter(0x60, true);
                    message.AppendParameter(0x61, true);
                    message.AppendParameter(0x62, true);
                    message.AppendParameter(0x63, true);
                    message.AppendParameter(100, true);
                    message.AppendParameter(0x65, true);
                    message.AppendParameter(0x66, true);
                    message.AppendParameter(0x67, true);
                    message.AppendParameter(0x68, true);
                    message.AppendParameter(0x69, true);
                    message.AppendParameter(110, true);
                    message.AppendParameter(0x99, false);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, true);
                    message.AppendParameter(0, false);
                    return message;
            }
            message.AppendParameter(1, true);
            message.AppendParameter(2, true);
            message.AppendParameter(3, true);
            message.AppendParameter(4, true);
            message.AppendParameter(5, true);
            message.AppendParameter(6, true);
            message.AppendParameter(0x17, true);
            message.AppendParameter(0x51, true);
            message.AppendParameter(0x59, true);
            message.AppendParameter(90, true);
            message.AppendParameter(0x5b, true);
            message.AppendParameter(0x5c, true);
            message.AppendParameter(0x5d, true);
            message.AppendParameter(0x5e, true);
            message.AppendParameter(0x5f, true);
            message.AppendParameter(0x60, true);
            message.AppendParameter(0x61, true);
            message.AppendParameter(0x62, true);
            message.AppendParameter(0x63, true);
            message.AppendParameter(100, true);
            message.AppendParameter(0x65, true);
            message.AppendParameter(0x66, true);
            message.AppendParameter(0x67, true);
            message.AppendParameter(0x68, true);
            message.AppendParameter(0x69, true);
            message.AppendParameter(110, true);
            message.AppendParameter(0x99, true);
            message.AppendParameter(0xc1, true);
            message.AppendParameter(0xc2, true);
            message.AppendParameter(0xc3, true);
            message.AppendParameter(0xc4, false);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, true);
            message.AppendParameter(0, false);
            return message;
        }
    }
}

