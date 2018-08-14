using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Communication.Incoming;
using Snowlight.Communication;
using Snowlight.Game.Sessions;
using Snowlight.Communication.Outgoing;

namespace Snowlight.Game.Contest
{
    class ContestHandler
    {
        public static void Initialize()
        {
            DataRouter.RegisterHandler(Opcodes.CONTESTINIT, new ProcessRequestCallback(ContestHandler.OnContestInit), false);
        }

        public static void OnContestInit(Session Session, ClientMessage Message)
        {
            Session.SendData(ContestInitComposer.Compose("Easter"));
        }
    }
}
