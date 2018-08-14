namespace BoomBang.Game.Sessions
{
    using BoomBang;
    using BoomBang.Communication;
    using BoomBang.Communication.Incoming;
    using BoomBang.Communication.Outgoing;
    using BoomBang.Config;
    using BoomBang.Encription;
    using BoomBang.Game.Characters;
    using BoomBang.Game.Laptop;
    using BoomBang.Game.Misc;
    using BoomBang.Game.Moderation;
    using BoomBang.Game.Spaces;
    using BoomBang.Storage;
    using BoomBang.Utils;
    using System;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;

    public class Session : IDisposable
    {
        /* private scope */
        bool bool_0;
        /* private scope */
        bool bool_1;
        /* private scope */
        bool bool_2;
        /* private scope */
        bool bool_3;
        /* private scope */
        bool bool_4;
        /* private scope */
        byte[] byte_0;
        /* private scope */
        BoomBang.Game.Characters.CharacterInfo characterInfo_0;
        /* private scope */
        double double_0;
        /* private scope */
        Encipher encipher_0;
        /* private scope */
        int int_0;
        /* private scope */
        SessionLaptopFriendCache sessionLaptopFriendCache_0;
        /* private scope */
        Socket socket_0;
        /* private scope */
        string string_0;
        /* private scope */
        string string_1;
        internal uint uint_0;
        /* private scope */
        uint uint_1;
        /* private scope */
        uint uint_2;
        /* private scope */
        UserIgnoreCache userIgnoreCache_0;

        public Session(uint uint_3, Socket Socket)
        {
            this.uint_0 = uint_3;
            this.socket_0 = Socket;
            this.byte_0 = new byte[0x400];
            this.encipher_0 = new Encipher();
            this.bool_0 = true;
            this.socket_0.Blocking = false;
            Output.WriteLine("Started client #" + uint_3 + ".", OutputLevel.DebugInformation);
            this.method_0();
        }
        
        public void Dispose()
        {
            if (!Stopped)
            {
                throw new InvalidOperationException("Cannot dispose of a session that has not been stopped");
            }

            Output.WriteLine("Disposed and released client " + this.uint_0 + " and associated resources.", OutputLevel.DebugInformation);
        }

        private void method_0()
        {
            try
            {
                if (this.socket_0 != null)
                {
                    this.socket_0.BeginReceive(this.byte_0, 0, this.byte_0.Length, SocketFlags.None, new AsyncCallback(this.method_1), null);
                }
            }
            catch (Exception)
            {
                SessionManager.StopSession(this.uint_0);
            }
        }

        private void method_1(IAsyncResult iasyncResult_0)
        {
            int byteCount = 0;
            byte[] bytes = null;
            try
            {
                if (this.socket_0 != null)
                {
                    byteCount = this.socket_0.EndReceive(iasyncResult_0);
                }
            }
            catch (Exception)
            {
            }
            if ((byteCount >= 1) && (byteCount < this.byte_0.Length))
            {
                bytes = ByteUtil.SubByte(this.byte_0, 0, byteCount);
                Output.WriteLine(string.Concat(new object[] { "[RCV][", this.uint_0, "]: ", Constants.DefaultEncoding.GetString(bytes) }), OutputLevel.DebugInformation);
                if ((bytes[0] == 60) && (bytes[1] == 0x70))
                {
                    this.method_3(bytes);
                }
                else
                {
                    bytes = this.encipher_0.Deciphe(bytes, bytes.Length);
                    this.method_3(bytes);
                }
                this.method_0();
            }
            else
            {
                SessionManager.StopSession(this.uint_0);
            }
        }

        private void method_2(IAsyncResult iasyncResult_0)
        {
            try
            {
                if (this.socket_0 != null)
                {
                    this.socket_0.EndSend(iasyncResult_0);
                }
            }
            catch (Exception)
            {
                SessionManager.StopSession(this.uint_0);
            }
        }

        private void method_3(byte[] byte_1)
        {
            if ((byte_1[0] == 60) && (byte_1[1] == 0x70))
            {
                this.SendData(CrossdomainPolicy.GetBytes(), true);
                SessionManager.StopSession(this.uint_0);
            }
            else if (byte_1[0] == 0xb1)
            {
                string[] strArray = Constants.DefaultEncoding.GetString(byte_1).Split(new char[] { '\x00b0' });
                try
                {
                    byte[] bytes;
                    foreach (string str in strArray)
                    {
                        bytes = Constants.DefaultEncoding.GetBytes(str + 1);
                        if (bytes.Length > 3)
                        {
                            bytes[bytes.Length - 1] = 0xb0;
                            if (bytes[3] == 0xb2)
                            {
                                if (bytes[1] == 0xa3)
                                {
                                    this.SendData(PingComposer.Compose(), false);
                                    if (bytes.Length <= 5)
                                    {
                                        continue;
                                    }
                                    goto Label_0238;
                                }
                                int num = 4;
                                while (num < bytes.Length)
                                {
                                    ClientMessage message = null;
                                    try
                                    {
                                        ushort messageFlag = bytes[1];
                                        byte[] body = new byte[bytes.Length - 4];
                                        for (int i = 0; i < body.Length; i++)
                                        {
                                            body[i] = bytes[num++];
                                        }
                                        message = new ClientMessage(messageFlag, 0, body);
                                    }
                                    catch (Exception)
                                    {
                                        Output.WriteLine("Packet formatting exception on packet: " + Constants.DefaultEncoding.GetString(bytes), OutputLevel.DebugNotification);
                                        break;
                                    }
                                    if (message != null)
                                    {
                                        try
                                        {
                                            DataRouter.HandleData(this, message);
                                            continue;
                                        }
                                        catch (Exception exception)
                                        {
                                            Output.WriteLine("Critical error in HandleData stack: " + exception.Message + "\n\n" + exception.StackTrace, OutputLevel.CriticalError);
                                            break;
                                        }
                                    }
                                }
                                continue;
                            }
                            int num4 = 6;
                            while (num4 < bytes.Length)
                            {
                                ClientMessage message2 = null;
                                try
                                {
                                    ushort num5 = bytes[1];
                                    ushort messageItem = bytes[3];
                                    byte[] buffer3 = new byte[bytes.Length - 6];
                                    for (int j = 0; j < buffer3.Length; j++)
                                    {
                                        buffer3[j] = bytes[num4++];
                                    }
                                    message2 = new ClientMessage(num5, messageItem, buffer3);
                                }
                                catch (Exception)
                                {
                                    Output.WriteLine("Packet formatting exception on packet: " + Constants.DefaultEncoding.GetString(bytes), OutputLevel.DebugNotification);
                                    break;
                                }
                                if (message2 != null)
                                {
                                    try
                                    {
                                        DataRouter.HandleData(this, message2);
                                        continue;
                                    }
                                    catch (Exception exception2)
                                    {
                                        Output.WriteLine("Critical error in HandleData stack: " + exception2.Message + "\n\n" + exception2.StackTrace, OutputLevel.CriticalError);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    return;
                Label_0238:
                    if (bytes[5] == 120)
                    {
                        this.SendData(FacebookInitComposer.Compose(), false);
                    }
                }
                catch (Exception exception3)
                {
                    Console.WriteLine(exception3.ToString());
                }
            }
            else
            {
                SessionManager.StopSession(this.uint_0);
            }
        }

        public void SendData(byte[] Data, bool XmlPolicy)
        {
            try
            {
                if (this.socket_0 != null)
                {
                    if (!XmlPolicy)
                    {
                        Data = this.encipher_0.Enciphe(Data, Data.Length);
                    }
                    Output.WriteLine(string.Concat(new object[] { "[SND][", this.uint_0, "]: ", Constants.DefaultEncoding.GetString(Data) }), OutputLevel.DebugInformation);
                    this.socket_0.BeginSend(Data, 0, Data.Length, SocketFlags.None, new AsyncCallback(this.method_2), null);
                }
            }
            catch (Exception)
            {
                SessionManager.StopSession(this.uint_0);
            }
        }

        public void SendData(ServerMessage Message, bool XmlPolicy = false)
        {
            if (Message != null)
            {
                this.SendData(Message.GetBytes(), XmlPolicy);
            }
        }

        public void Stop(SqlDatabaseClient MySqlClient)
        {
            if (!this.Stopped)
            {
                this.socket_0.Close();
                this.socket_0 = null;
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
            }
        }

        public void TryAuthenticate(string Username, string Password, string RemoteAddress, bool Register = false)
        {
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                uint characterId = UserCredentialsAuthenticator.TryAuthenticate(client, Username, Password, RemoteAddress);
                if (characterId == 0)
                {
                    this.SendData(AuthenticationKoComposer.Compose(false), false);
                }
                else
                {
                    BoomBang.Game.Characters.CharacterInfo info = CharacterInfoLoader.GetCharacterInfo(client, characterId, this.uint_0, true);
                    if (ModerationBanManager.IsUserIdBlacklisted(info.UInt32_0))
                    {
                        this.SendData(ModerationBanComposer.Compose(ModerationBanManager.GetBanDetails(info.UInt32_0)), false);
                        SessionManager.StopSession(this.uint_0);
                    }
                    else if ((info != null) && info.HasLinkedSession)
                    {
                        this.characterInfo_0 = info;
                        this.characterInfo_0.TimestampLastOnline = UnixTimestamp.GetCurrent();
                        CharacterResolverCache.AddToCache(this.characterInfo_0.UInt32_0, this.characterInfo_0.Username, true);
                        this.sessionLaptopFriendCache_0 = new SessionLaptopFriendCache(client, this.CharacterId);
                        this.userIgnoreCache_0 = new UserIgnoreCache(client, this.CharacterId);
                        this.bool_1 = true;
                        if (Register)
                        {
                            this.SendData(RegisterComposer.Compose(this.characterInfo_0), false);
                        }
                        else
                        {
                            this.SendData(AuthenticationOkComposer.Compose(this.characterInfo_0), false);
                        }
                        LaptopHandler.MarkUpdateNeeded(this, 0, true);
                    }
                    else
                    {
                        SessionManager.StopSession(this.uint_0);
                    }
                }
            }
        }

        public uint AbsoluteSpaceId
        {
            get
            {
                return this.uint_1;
            }
            set
            {
                this.uint_1 = value;
            }
        }

        public string AbsoluteSpaceName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public bool Authenticated
        {
            get
            {
                return ((this.characterInfo_0 != null) && this.bool_1);
            }
        }

        public uint CharacterId
        {
            get
            {
                if (this.characterInfo_0 == null)
                {
                    return 0;
                }
                return this.characterInfo_0.UInt32_0;
            }
        }

        public BoomBang.Game.Characters.CharacterInfo CharacterInfo
        {
            get
            {
                return this.characterInfo_0;
            }
        }

        public int CurrentEffect
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public uint CurrentSpaceId
        {
            get
            {
                if (this.bool_3 && this.bool_2)
                {
                    return this.uint_1;
                }
                return 0;
            }
        }

        public UserIgnoreCache IgnoreCache
        {
            get
            {
                return this.userIgnoreCache_0;
            }
        }

        public bool InSpace
        {
            get
            {
                return (this.CurrentSpaceId > 0);
            }
        }

        public bool IsTeleporting
        {
            get
            {
                return this.bool_4;
            }
            set
            {
                this.bool_4 = value;
            }
        }

        public SessionLaptopFriendCache LaptopFriendCache
        {
            get
            {
                return this.sessionLaptopFriendCache_0;
            }
        }

        public bool LatencyTestOk
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public string RemoteAddress
        {
            get
            {
                if ((this.socket_0 != null) && this.socket_0.Connected)
                {
                    return this.socket_0.RemoteEndPoint.ToString().Split(new char[] { ':' })[0];
                }
                return string.Empty;
            }
        }

        public bool SpaceAuthed
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                this.bool_2 = value;
            }
        }

        public bool SpaceJoined
        {
            get
            {
                return this.bool_3;
            }
            set
            {
                this.bool_3 = value;
            }
        }

        public bool Stopped
        {
            get
            {
                return (this.socket_0 == null);
            }
        }

        public uint TargetTeleporterId
        {
            get
            {
                return this.uint_2;
            }
            set
            {
                this.uint_2 = value;
            }
        }

        public double TimeStopped
        {
            get
            {
                return (UnixTimestamp.GetCurrent() - this.double_0);
            }
        }

        public uint UInt32_0
        {
            get
            {
                return this.uint_0;
            }
        }

        public string UserAgent
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }
    }
}

