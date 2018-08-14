using BoomBang_RetroServer.Game.Users;
using BoomBang_RetroServer.Sessions;
using BoomBang_RetroServer.Sockets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Sessions
{
    public partial class Handler
    {
        public User User;
        public ClientMessage Message;
        public Session Session;

        public void SendMessage(ServerMessage Message)
        {
            Session.SendMessage(Message);
        }
    }
}
