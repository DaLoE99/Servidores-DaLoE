using System;
using System.Collections.ObjectModel;

using Snowlight.Game.Sessions;
using Snowlight.Communication;
using Snowlight.Communication.Outgoing;
using Snowlight.Util;
using System.Collections.Generic;
using Snowlight.Communication.Incoming;
using Snowlight.Communication.Outgoing.Global;

namespace Snowlight.Game.Handlers
{
    public static class Global
    {
        public static void Initialize()
        {
            DataRouter.RegisterHandler(Opcodes.PING, new ProcessRequestCallback(Ping), true);
            DataRouter.RegisterHandler(Opcodes.UNKNOW2, new ProcessRequestCallback(Unknow2), false);
        }

        private static void Ping(Session Session, ClientMessage Message)
        {
            Session.SendData(PingComposer.Compose());
        }
        private static void Unknow2(Session Session, ClientMessage Message)
        {

            Session.SendData(Unknow2Composer.Compose());
        }
    }
}
