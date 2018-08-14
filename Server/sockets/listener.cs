using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using Boombang.sockets;

namespace Boombang.sockets
{
    public class listener
    {
        private Socket mListener = null;
        private long mClientCount = 0;
        private NewConnectionDelegate mConnectionCallback;
       
        public delegate void NewConnectionDelegate(long clientid, Socket clientsocket);

        public listener(NewConnectionDelegate connectionCallback)
        {
            mConnectionCallback = connectionCallback;
            this.StartListening();
        }

        private void StartListening()
        {
            mListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, 2001);
            mListener.Bind(ipLocal);
            mListener.Listen(5);
            mListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            Console.WriteLine("[SCKMGR] Escuchando en el puerto 2001, a la espera de conexiones...");
        }

        private void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                mClientCount++;
                Socket tmpSock = mListener.EndAccept(asyn);
                Console.WriteLine("[SCKMGR] Cliente " + tmpSock.RemoteEndPoint.ToString() + " conectado, ID de sesión: " + mClientCount.ToString() + ".");

                mListener.BeginAccept(new AsyncCallback(OnClientConnect), null);

                mConnectionCallback(mClientCount, tmpSock);

            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("[SCKERR] Socket cerrado antes de completarse la conexión.");
            }
            catch (SocketException e)
            {
                Console.WriteLine("[SCKERR] Error de socket: " + e.ToString());
            }
        }
    }
}
