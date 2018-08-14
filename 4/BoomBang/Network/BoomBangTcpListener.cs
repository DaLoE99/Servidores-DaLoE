namespace BoomBang.Network
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    /// Snowlight simple asynchronous TCP listener.
    /// </summary>
    public class BoomBangTcpListener : IDisposable // Snow prefix to avoid conflicts with System.Net.TcpListener
    {
        private Socket mSocket;
        private OnNewConnectionCallback mCallback;

        public BoomBangTcpListener(IPEndPoint LocalEndpoint, int Backlog, OnNewConnectionCallback Callback)
        {
            mCallback = Callback;

            mSocket = new Socket(LocalEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            mSocket.Bind(LocalEndpoint);
            mSocket.Listen(Backlog);
            mSocket.Blocking = false;

            BeginAccept();
        }

        public void Dispose()
        {
            if (mSocket != null)
            {
                //  mSocket.Dispose(); Not implemented in MONO
                mSocket = null;
            }
        }

        private void BeginAccept()
        {
            try
            {
                mSocket.BeginAccept(OnAccept, null);
            }
            catch (Exception) { }
        }

        private void OnAccept(IAsyncResult Result)
        {
            try
            {
                Socket ResultSocket = (Socket)mSocket.EndAccept(Result);
                mCallback.Invoke(ResultSocket);
            }
            catch (Exception) { }

            BeginAccept();
        }
    }
}
