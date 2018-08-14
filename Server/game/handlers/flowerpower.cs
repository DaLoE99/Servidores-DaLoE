using System;

using Boombang.sockets.messages;
using Boombang.game.session;

namespace Boombang.game.handlers
{
    public partial class flowerpower : Handler
    {
        #region Inicio de FlowerPower
        public void Handler120_type_134()
        {
            server message = new server("x");
            message.Append("³³²1³2³3³4³5³6³23³81³89³90³91³92³93³94³95³96³97³98³99³100³101³102³103³104³105³110³153³154³155³156³157³158³159³160³161³162³163³164³165³166³167³168³169³170³171³172³173³174³175³176³177³178³179³180³181³182³183³184³185³186³187³188³189³²0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³0³²");
            SendMessage(message);
        }

        public void Handler120_type_141()
        {
            //x³³,0³
            server message = new server("x");
            message.Append("³³²0³²");
            SendMessage(message);
        }

        public void Handler132_type_121() //³y³²0³
        {
            server message = new server("");
            message.Append("³y³²0³²");
            SendMessage(message);
        }

        public void Handler189_type_180()
        {
            server message = new server("½");
            message.Append("³´³²");
            SendMessage(message);
        }

        public void Handler120_type_143()
        {
            server message = new server("x");
            message.Append("³³²16³²-1³²-1³²25³²");
            SendMessage(message);
        }

        public void Handler210_type_120()
        {
            server message = new server("Ò³x³²0³1³2³4³9³16³17³18³²");
            SendMessage(message);
        }
        #endregion

    }
}
