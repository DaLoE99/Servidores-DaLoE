using System;
using System.Text;
using System.Net.Sockets;

using Boombang.encoding;

namespace Boombang.sockets
{
    public class connection
    {
        private long mConnectionID;
        private Socket mSocket;
        private AsyncCallback recCallback;
        private EndConnectionDelegate mEndConnectionCallback;
        private NewDataDelegate mNewDataCallback;
        private byte[] dataBuffer = new byte[2048];

        public delegate void EndConnectionDelegate(long clientid);
        public delegate void NewDataDelegate(long clientid, string data);

        public string GetIP()
        {
            return mSocket.RemoteEndPoint.ToString().Split(':')[0];
        }

        public connection(long socketid, Socket socket, EndConnectionDelegate endConnectionCallback, NewDataDelegate newDataCallback)
        {
            mSocket = socket;
            mConnectionID = socketid;
            mEndConnectionCallback = endConnectionCallback;
            mNewDataCallback = newDataCallback;
            this.WaitForData();
        }

        private void WaitForData()
        {
            try
            {
                if (recCallback == null)
                {
                    CreateCallBacks();
                }
                mSocket.BeginReceive(this.dataBuffer, 0, this.dataBuffer.Length, SocketFlags.None, recCallback, null); //Offset: 0

            }
            catch (SocketException)
            {
                ConnectionEnded();
            }
            catch (ObjectDisposedException)
            {
                ConnectionEnded();
            }
            catch (Exception e)
            {
                Console.WriteLine("[SCKERR] Error de socket: " + e.InnerException.ToString());
                ConnectionEnded();
            }

        }

        private void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                int iRx = mSocket.EndReceive(asyn);
                if (iRx == 0)
                {
                    ConnectionEnded();
                    return;
                }
                char[] chars = new char[iRx];
                System.Text.Decoder d = System.Text.Encoding.GetEncoding("iso-8859-1").GetDecoder();
                int charLen = d.GetChars(dataBuffer, 0, iRx, chars, 0);
                System.String szData = new System.String(chars);

                NewData(szData);

                WaitForData();
            }
            catch (ObjectDisposedException)
            {
                ConnectionEnded();
                return;
            }
            catch (SocketException e)
            {
                Console.WriteLine("[SCKERR] Error de socket: " + e.ToString());

                ConnectionEnded();
            }
        }

        private void NewData(string data)
        {
            mNewDataCallback(mConnectionID, data);
        }

        private void ConnectionEnded()
        {
            mEndConnectionCallback(mConnectionID);
        }

        private void CreateCallBacks()
        {
            recCallback = new AsyncCallback(OnDataReceived);
        }

        public void sendPacket(string data)
        {
            try
            {
                byte[] toSend = conversion.stringToByteArray("\xB1" + data + "\xB0");
                string a = "a";
                if (toSend != null)
                {
                    sendRawPacket(toSend);
                }
            }
            catch (SocketException)
            {
                ConnectionEnded();
            }
            catch (ObjectDisposedException)
            {
                ConnectionEnded();
            }
            catch (Exception e)
            {
                Console.WriteLine("[SCKERR] Error de socket: " + e.InnerException.ToString());
            }
        }

        public void sendPolicy(string data)
        {
            try
            {
                byte[] toSend = conversion.stringToByteArray(data);
                if (toSend != null)
                {
                    sendRawPacket(toSend);
                }
            }
            catch (SocketException)
            {
                ConnectionEnded();
            }
            catch (ObjectDisposedException)
            {
                ConnectionEnded();
            }
            catch (Exception e)
            {
                Console.WriteLine("[SCKERR] Error de socket: " + e.InnerException.ToString());
            }
        }

        public void sendCatalogMessage(string data)
        {
            try
            {
                byte[] toSend = conversion.stringToByteArray("\xB1" + data);
                if (toSend != null)
                {
                    sendRawPacket(toSend);
                }
            }
            catch (SocketException)
            {
                ConnectionEnded();
            }
            catch (ObjectDisposedException)
            {
                ConnectionEnded();
            }
            catch (Exception e)
            {
                Console.WriteLine("[SCKERR] Error de socket: " + e.InnerException.ToString());
            }
        }

        public void sendRawPacket(byte[] data)
        {
            mSocket.Send(data);
        }

        public void Kill()
        {
            try
            {
                mSocket.Close();
            }
            catch
            {
                Console.WriteLine("[SCKERR] Error, el socket se encuentra bloqueado.");
            }
        }
    }
}
