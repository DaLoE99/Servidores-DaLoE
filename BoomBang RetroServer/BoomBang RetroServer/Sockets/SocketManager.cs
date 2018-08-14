using BoomBang_RetroServer.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Sockets
{
    public static class SocketManager
    {
        private static Socket Server;
        private static IPEndPoint Address;
        public static void Initialize()
        {
            Address = new IPEndPoint(IPAddress.Any, (int)ConfigurationManager.GetValue("server.port"));
            Server = new Socket(Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Server.Bind(Address);
            Server.Listen(50);
            Server.Blocking = false;
            WaitClient();
            Output.WriteLine("Socket manager initialized.", OutputLevel.Information);
        }
        public static void Uninitialize()
        {
            try
            {
                Server.Close();
            }
            catch { }
            Output.WriteLine("Socket manager uninitialized.", OutputLevel.Information);
        }
        public static void WaitClient()
        {
            try
            {
                Server.BeginAccept(new AsyncCallback(ClientConnected), null);
            }
            catch(Exception ex)
            {
                Output.WriteLine("Socket error. Exception: " + ex.ToString(), OutputLevel.Warning);
            }
        }
    }
}
