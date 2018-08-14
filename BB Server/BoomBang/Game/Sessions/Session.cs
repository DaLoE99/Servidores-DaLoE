using System;
using System.Linq;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Reflection;


using Snowlight.Util;
using Snowlight.Communication;
using Snowlight.Communication.Outgoing;
using Snowlight.Storage;
using System.Data;
using Snowlight.Communication.Incoming;
using Snowlight.Config;
using System.Text;
using Snowlight.Game.Characters;
using Snowlight.Game.Laptop;
using Snowlight.Game.Misc;

namespace Snowlight.Game.Sessions
{
    public class Session : IDisposable
    {
        private uint mId;
        private Socket mSocket;
        private byte[] mBuffer;
        private double mStoppedTimestamp;
        private bool mIsAuth;

        private CharacterInfo mCharacterInfo;
        private SessionLaptopFriendCache sessionLaptopFriendCache;
        private bool mSpaceJoined;
        private string mAbsoluteSpaceName;
        UserIgnoreCache userIgnoreCache;
        bool mSpaceAuthed;
        int mCurrentEffect;
        uint mCurrentSpaceId;

        public uint Id
        {
            get
            {
                return mId;
            }
        }

        public uint CurrentSpaceId
        {
            get
            {
                if (this.mSpaceJoined && this.mSpaceAuthed)
                {
                    return this.mCurrentSpaceId;
                }
                return 0;
            }
        }

        public int CurrentEffect
        {
            get
            {
                return this.mCurrentEffect;
            }
            set
            {
                this.mCurrentEffect = value;
            }
        }

        public bool SpaceAuthed
        {
            get
            {
                return this.mSpaceAuthed;
            }
            set
            {
                this.mSpaceAuthed = value;
            }
        }

        public uint AbsoluteSpaceId
        {
            get
            {
                return this.mCurrentSpaceId;
            }
            set
            {
                this.mCurrentSpaceId = value;
            }
        }

        public UserIgnoreCache IgnoreCache
        {
            get
            {
                return this.userIgnoreCache;
            }
        }

        public SessionLaptopFriendCache LaptopFriendCache
        {
            get
            {
                return this.sessionLaptopFriendCache;
            }
        }

        public string AbsoluteSpaceName
        {
            get
            {
                return this.mAbsoluteSpaceName;
            }
            set
            {
                this.mAbsoluteSpaceName = value;
            }
        }

        public bool SpaceJoined
        {
            get
            {
                return this.mSpaceJoined;
            }
            set
            {
                this.mSpaceJoined = value;
            }
        }

        public string RemoteAddress
        {
            get
            {
                return ((mSocket != null && mSocket.Connected ?
                    mSocket.RemoteEndPoint.ToString().Split(':')[0] : string.Empty));
            }
        }

        public uint CharacterId
        {
            get
            {
                return (mCharacterInfo != null ? mCharacterInfo.Id : 0);
            }
        }

        public double TimeStopped
        {
            get
            {
                return (UnixTimestamp.GetCurrent() - mStoppedTimestamp);
            }
        }

        public bool Stopped
        {
            get
            {
                return (mSocket == null);
            }
        }

        public bool Authenticated
        {
            get
            {
                return mIsAuth;
            }
            set
            {
                mIsAuth = value;
            }
        }

        public Characters.CharacterInfo CharacterInfo
        {
            get
            {
                return mCharacterInfo;
            }
        }

        public Session(uint Id, Socket Socket)
        {
            mId = Id;
            mSocket = Socket;
            mBuffer = new byte[512];

            mSocket.Blocking = false;
            Output.WriteLine("Started client " + Id + ".", OutputLevel.DebugInformation);

            BeginReceive();
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
                SessionManager.StopSession(mId);
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
                SessionManager.StopSession(mId);
                return;
            }

            ProcessData(ByteUtil.Subbyte(mBuffer, 0, ByteCount)); 
            BeginReceive();
        }

        public void SendData(ServerMessage Message)
        {
            SendData(Message.GetBytes());
        }

        public void SendData(byte[] Data)
        {
            try
            {
                if (mSocket != null)
                {
                    byte ID = Data[1];
                    byte Type = (Data[3] == 178) ? (byte)0 : Data[3];
                    Output.WriteLine("[SND][" + mId + "] ["+ ID + "" + Type + "]: " + Constants.DefaultEncoding.GetString(Data), OutputLevel.DebugInformation);
                    
                    mSocket.BeginSend(Data, 0, Data.Length, SocketFlags.None, new AsyncCallback(OnDataSent), null);
                }
            }
            catch (Exception e)
            {
                Output.WriteLine("[SND] Socket is null!\n\n" + e.StackTrace, OutputLevel.CriticalError);
            }
            /*
             * TODO: catch all exceptions => Stop()
             */
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
                SessionManager.StopSession(mId);
            }
        }

        private void ProcessData(byte[] Data)
        {
            if (Data.Length == 0)
            {
                return;
            }
            
            if (Data[0] == 177)
            {
                string[] sp = Encoding.Default.GetString(Data).Split('±');
                int i = 1;
                while (i <= sp.Length - 1)
                {
                    int Pos = 0;
                    byte[] lol = Encoding.Default.GetBytes("±" + sp[i]);
                    while (Pos < lol.Length)
                    {
                        ClientMessage Message = null;

                        try
                        {
                            byte ID = lol[1];
                            byte Type = (lol[3] == 178) ? (byte)0 : lol[3];
                            uint MessageId = uint.Parse(lol[1] + "" + Type);
                            int MessageLength = (Type > 0) ? lol.Length - 4 : lol.Length - 2;

                            Pos = (Type > 0) ? 4 : 2;

                            byte[] Content = new byte[MessageLength];

                            for (int ii = 0; ii < Content.Length; ii++)
                            {
                                Content[ii] = lol[Pos++];
                            }

                            Message = new ClientMessage(MessageId, Content);
                        }
                        catch (Exception)
                        {
                            SessionManager.StopSession(mId); // packet formatting exception
                            return;
                        }

                        if (Message != null)
                        {
                            if (Program.DEBUG)
                            {
                                Output.WriteLine("[RCV][" + mId + "]: [" + Message.Id + "] " + Encoding.Default.GetString(lol), OutputLevel.DebugInformation);
                            }

                            try
                            {
                                DataRouter.HandleData(this, Message);
                            }
                            catch (Exception e)
                            {
                                Output.WriteLine("Critical error in HandleData stack: " + e.Message + "\n\n" + e.StackTrace,
                                    OutputLevel.CriticalError);
                                SessionManager.StopSession(mId);
                                return;
                            }
                        }
                        i++;
                    }
                }
            }
            else if (Data[0] == 60)
            {
                Output.WriteLine("Sent crossdomain policy to client " + mId + ".", OutputLevel.DebugInformation);
                string xmlPolicy =
                                   "<?xml version=\"1.0\"?>\r\n" +
                                   "<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n" +
                                   "<cross-domain-policy>\r\n" +
                                   "<allow-access-from domain=\"*\" to-ports=\"2002\" />\r\n" +
                                   "</cross-domain-policy>\x0";
                SendData(Encoding.Default.GetBytes(xmlPolicy));
                SessionManager.StopSession(mId);
            }
            else
            {
                Output.WriteLine("[RCV][" + mId + "]: " + Encoding.Default.GetString(Data), OutputLevel.DebugInformation);
                SessionManager.StopSession(mId);
            }
        }

        public void TryAuthenticate(string Username, string Password, string RemoteAddress, bool Register = false)
        {
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                uint characterId = UserCredentialsAuthenticator.TryAuthenticate(client, Username, Password, RemoteAddress);
                if (characterId == 0)
                {
                    SendData(LoginKoComposer.Compose());
                }
                else
                {
                    Game.Characters.CharacterInfo info = CharacterInfoLoader.GetCharacterInfo(client, characterId, mId, true);
                    /*if (ModerationBanManager.IsUserIdBlacklisted(info.Id))
                    {
                        SendData(ModerationBanComposer.Compose(ModerationBanManager.GetBanDetails(info.UInt32_0)), false);
                        SessionManager.StopSession(this.Id);
                    }*/
                    if ((info != null) && info.HasLinkedSession)
                    {
                        mCharacterInfo = info;
                        mCharacterInfo.TimestampLastOnline = UnixTimestamp.GetCurrent();
                        CharacterResolverCache.AddToCache(this.mCharacterInfo.Id, this.mCharacterInfo.Username, true);
                        sessionLaptopFriendCache = new SessionLaptopFriendCache(client, this.CharacterId);
                        this.userIgnoreCache = new UserIgnoreCache(client, this.CharacterId);
                        Authenticated = true;
                        
                        SendData(LoginOkComposer.Compose(this.mCharacterInfo));
                    }
                    else
                    {
                        SessionManager.StopSession(this.mId);
                    }
                    LaptopHandler.MarkUpdateNeeded(this, 0, true);
                }
            }
        }

        public void Stop(SqlDatabaseClient MySqlClient)
        {
            if (Stopped)
            {
                return;
            }

            mSocket.Close();
            mSocket = null;

            if (Authenticated)
            {
                /*mCharacterInfo.SynchronizeStatistics(MySqlClient);

                if (CurrentRoomId > 0)
                {
                    RoomManager.RemoveUserFromRoom(this, false);
                }

                MessengerHandler.MarkUpdateNeeded(this, 0, true);
                MySqlClient.ExecuteNonQuery("UPDATE characters SET online = '0' WHERE id = " + CharacterId + " LIMIT 1");*/
            }

            Output.WriteLine("Stopped and disconnected client " + Id + ".", OutputLevel.DebugInformation);

            mStoppedTimestamp = UnixTimestamp.GetCurrent();
        }

        public void Dispose()
        {
            if (!Stopped)
            {
                throw new InvalidOperationException("Cannot dispose of a session that has not been stopped");
            }

            Output.WriteLine("Disposed and released client " + Id + " and associated resources.", OutputLevel.DebugInformation);
        }

        public void SendInfoUpdate()
        {
            if (!Authenticated)
            {
                return;
            }

            
        }
    }
}
