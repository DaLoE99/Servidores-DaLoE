using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Communication.Incoming;
using Snowlight.Communication;
using Snowlight.Game.Sessions;
using Snowlight.Communication.Outgoing;
using Snowlight.Communication.Outgoing.FlowerPower;
using Snowlight.Game.Misc;
using System.Text.RegularExpressions;

namespace Snowlight.Game.Handlers
{
    class FlowerPower
    {
        public static void Initialize()
        {
            DataRouter.RegisterHandler(Opcodes.INITFLOWER, new ProcessRequestCallback(InitFlower), false);
            DataRouter.RegisterHandler(Opcodes.ADVERTISEMENT, new ProcessRequestCallback(Advertisement), false);
            DataRouter.RegisterHandler(Opcodes.FLOWERPOWER, new ProcessRequestCallback(Flowerpower), false);
            DataRouter.RegisterHandler(Opcodes.NEWSLOAD, new ProcessRequestCallback(LoadNews), false);
            DataRouter.RegisterHandler(Opcodes.NEWSREAD, new ProcessRequestCallback(ReadNews), false);
            DataRouter.RegisterHandler(Opcodes.UNKNOW1, new ProcessRequestCallback(Unknow1), false);
        }

        private static void InitFlower(Session Session, ClientMessage Message)
        {
            
        }

        private static void Advertisement(Session Session, ClientMessage Message)
        {
            Session.SendData(LoaderAdvertisementComposer.Compose(true));
        }

        private static void Flowerpower(Session Session, ClientMessage Message)
        {
            Session.SendData(FlowerPowerLoadComposer.Compose());
        }

        private static void LoadNews(Session Session, ClientMessage Message)
        {
            Session.SendData(NewsInitComposer.Compose(NewsCacheManager.list_0));
        }

        private static void ReadNews(Session Session, ClientMessage Message)
        {
            string[] GetParameter = Regex.Split(Message.ToString(), "³²");
            uint num = uint.Parse(GetParameter[2]);
            for (int i = 0; i < NewsCacheManager.list_0.Count; i++)
            {
                if (NewsCacheManager.list_0[i].Id.Equals(num))
                {
                    Session.SendData(NewsContentComposer.Compose(NewsCacheManager.list_0[i]));
                }
            }
        }

        private static void Unknow1(Session Session, ClientMessage Message)
        {
            //±x³“³x³²1³²4294967295³²-1³²°
            ServerMessage message = new ServerMessage(Opcodes.UNKNOW1);
            message.Append(1);
            message.Append(4294967295);
            message.Append(-1);

            Session.SendData(message);
        }
    }
}
