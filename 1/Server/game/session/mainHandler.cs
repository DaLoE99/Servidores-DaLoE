using System;

using Boombang.sockets;
using Boombang.sockets.messages;
using Boombang.game.user;

namespace Boombang.game.session
{
    public partial class Handler
    {
        protected server fuseResponse;

        public userInfo mUser;
        public client Message;
        public long mSessionID;
    }

    public partial class Handler
    {
        protected void ReportError(string data, bool fatal)
        {
            if (fatal)
            {
                Environment.connections.EndConnection(mSessionID);
            }
        }

        public void SendMessage(server message)
        {
            SendMessage(message.ToString());
        }

        public void SendCatalog(server message)
        {
            SendMessage(message.ToString());
        }

        public void SendMessage(string message, bool logEvent)
        {
            if (logEvent)
            {
                Console.WriteLine("[SCKMGR] -- [SND][" + mSessionID.ToString() + "]: " + message);
            }
            Environment.connections.GetConnection(mSessionID).sendPacket(message);
        }

        public void sendPolicy(string message, bool logEvent)
        {
            if (logEvent) Console.WriteLine("[SCKMGR] -- [SND][" + mSessionID.ToString() + "]: " + message);

        Environment.connections.GetConnection(mSessionID).sendPolicy(message);
        }

        public void sendCatalog(string message, bool logEvent)
        {
            if (logEvent) Console.WriteLine("[SCKMGR] -- [SND][" + mSessionID.ToString() + "]: " + message);

            Environment.connections.GetConnection(mSessionID).sendCatalogMessage(message);
        }

        public void SendMessage(string message)
        {
            SendMessage(message, true);
        }
    }
}
