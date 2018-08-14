using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Snowlight.Util;
using Snowlight.Game.Sessions;
using Snowlight.Communication.Outgoing;
using Snowlight.Communication;
using Snowlight.Storage;

namespace Snowlight.Network
{
    public class  CommandListener
    {
        private Socket mSocket;
        private byte[] mBuffer; 
        private static uint mCounter = 0;
        private static Dictionary<uint, CommandListener> mSessions = new Dictionary<uint, CommandListener>();
        private uint mId;
        private bool is_human = false;

        public CommandListener(uint Id)
        {
            mId = Id;
        }
        
        public static void parse(Socket IncomingSocket)
        {
            uint Id = mCounter++;

            IncomingSocket.Blocking = false;
            Output.WriteLine("Started Command client " + Id + ".", OutputLevel.DebugInformation);

            CommandListener mus = new CommandListener(Id);
            mus.mBuffer = new byte[512];
            mus.mSocket = IncomingSocket;
            //mus.SendData("Welcome. For not being disconnected after a command type human.\r\n");
            mus.BeginReceive();           

            mSessions.Add(Id,mus);
        }

        private void BeginReceive()
        {
            try
            {
                if (mSocket != null)
                {
                    mSocket.BeginReceive(mBuffer, 0, mBuffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveData), null);
                }
            }
            catch (Exception)
            {
                stop(mId);
            }
        }

        private void OnReceiveData(IAsyncResult Result)
        {
            int ByteCount = 0;

            try
            {
                if (mSocket != null)
                {
                    ByteCount = mSocket.EndReceive(Result);
                }
            }
            catch (Exception) { }

            if (ByteCount < 1 || ByteCount >= mBuffer.Length)
            {
                stop(mId);
                return;
            }

            ProcessData(ByteUtil.Subbyte(mBuffer, 0, ByteCount));
            BeginReceive();
        }

        private void ProcessData(byte[] Data)
        {
            if (Data.Length == 0)
            {
                return;
            }

            ASCIIEncoding enc = new ASCIIEncoding();
            String command = enc.GetString(Data);

            command = command.Replace("\r\n", "").Trim();
            String[] bits = command.Split(Convert.ToChar(1));

            command = bits[0];
            Session Target = null;
            
            switch (command)
            {
                case "status":
                    SendData("1");
                    break;

                case "human":
                    is_human = true;
                    SendData("Welcome. To get a list of commands type commands.");
                    break;  

                case "close":
                case "exit":
                    SendData("Bye");
                    stop(mId);
                    break;
                default:
                    SendData("Unknown Command.");
                    break;
            }

            if (!is_human)
            {
                stop(mId);
            }
            
        }

        private void SendData(String command)
        {
            ASCIIEncoding enc = new ASCIIEncoding();
            if (is_human) { command = command + "\r\n"; }
            Byte[] Data = enc.GetBytes(command);
            try
            {
                if (mSocket != null)
                {
                    mSocket.BeginSend(Data, 0, Data.Length, SocketFlags.None, new AsyncCallback(OnDataSent), null);
                }
            }
            catch (Exception e)
            {
                Output.WriteLine("[SND] Socket is null!\n\n" + e.StackTrace, OutputLevel.CriticalError);
            }
        }

        private void OnDataSent(IAsyncResult Result)
        {
            try
            {
                if (mSocket != null)
                {
                    mSocket.EndSend(Result);
                }
            }
            catch (Exception)
            {
                stop(mId);
            }
        }

        private void stop(uint Id)
        {
            mSocket.Close();
            stop2(Id);
        }
        
        private static void stop2(uint Id)
        {
            if(mSessions.ContainsKey(Id)) {
            mSessions.Remove(Id);
            Output.WriteLine("Stopped Command client " + Id + ".", OutputLevel.DebugInformation);
            }
        }
    }
}
