using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Sessions;
using Snowlight.Communication;
using Snowlight.Game.Characters;
using Snowlight.Communication.Outgoing.Users;
using Snowlight.Communication.Incoming;
using Snowlight.Communication.Outgoing.Global;

namespace Snowlight.Game.Misc
{
    class SilverCoinsWorker
    {
        public static void Initialize()
        {
            DataRouter.RegisterHandler(Opcodes.SILVERTIMELEFT, new ProcessRequestCallback(SilverCoinsWorker.OnTimeLeftRequest), false);
        }

        public static void OnTimeLeftRequest(Session Session, ClientMessage Message)
        {
            CharacterInfo characterInfo = Session.CharacterInfo;
            if (characterInfo != null)
            {
                characterInfo.TimeSinceLastActivityPointsUpdate -= 100.0;
                if (characterInfo.TimeSinceLastActivityPointsUpdate > 0.0)
                {
                    Session.SendData(SilverCoinsTimeLeftComposer.Compose((int)characterInfo.TimeSinceLastActivityPointsUpdate));
                }
                else
                {
                    characterInfo.TimeSinceLastActivityPointsUpdate = 900.0;
                    Session.SendData(SilverCoinsTimeLeftComposer.Compose(900));
                    Session.SendData(CharacterCoinsComposer.AddSilverCompose(50));
                }
            }
        }
    }
}
