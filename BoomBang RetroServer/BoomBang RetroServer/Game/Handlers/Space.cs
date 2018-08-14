using BoomBang_RetroServer.Sessions;
using BoomBang_RetroServer.Sockets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Game.Handlers
{
    public partial class Space : Handler
    {
        public void Handler_131()
        {
            int Coconut = Convert.ToInt32(Message.Parameters[0, 0]);
            if(Coconut >= 0 && Coconut <= User.CoconutLevel)
            {
                User.SelectedCoconut = Coconut;
                ServerMessage message1 = new ServerMessage(new byte[] { 131 }, new object[] { User.ID, Coconut });
                SendMessage(message1);
            }
        }
    }
}
