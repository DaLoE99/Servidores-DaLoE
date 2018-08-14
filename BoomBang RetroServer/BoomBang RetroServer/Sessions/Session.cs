using BoomBang_RetroServer.Game.Handlers;
using BoomBang_RetroServer.Game.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoomBang_RetroServer.Utils;
using BoomBang_RetroServer.Game.Spaces.Areas;
using BoomBang_RetroServer.Game.Spaces.Salas;
using BoomBang_RetroServer.Sockets.Messages;

namespace BoomBang_RetroServer.Sessions
{
    class HandlerCollection
    {
        public HandlerCollection(Type Type, Handler Handler) { this.Type = Type; this.Handler = Handler; }
        public Type Type;
        public Handler Handler;
    }
    class Session
    {
        private readonly long ID;
        private Socket Client;
        private byte[] Buffer;
        private Encryption Encryption;
        private DateTime LastUpdate;
        private Thread Checker;
        public readonly string IP;
        public List<HandlerCollection> Handlers;
        public User User;
        public Session(long ID, Socket Client)
        {
            this.ID = ID;
            this.Client = Client;
            this.Buffer = new byte[2048];
            this.Encryption = new Encryption();
            this.LastUpdate = DateTime.Now;
            this.IP = Client.RemoteEndPoint.ToString().Split(':')[0];
            this.User = null;
            this.Checker = new Thread(new ThreadStart(ConnectionAliveChecker));
            this.Checker.Start();
            Handlers = new List<HandlerCollection>();
            this.RegisterDefaultHandlers();
            WaitData();
        }
        private void ConnectionAliveChecker()
        {
            while (true)
            {
                if (new TimeSpan(DateTime.Now.Ticks - LastUpdate.Ticks).TotalSeconds >= 120)
                {
                    new Thread(new ThreadStart(this.End)).Start();
                }
                Thread.Sleep(120000);
            }
        }
        private void WaitData()
        {
            try
            {
                Client.BeginReceive(this.Buffer,0,this.Buffer.Length, SocketFlags.None, new AsyncCallback(DataReceived), null);
            }
            catch(Exception ex)
            {
                Output.WriteLine("Socket error. Exception: " + ex.ToString(), OutputLevel.Warning);
                this.End();
            }
        }
        private void DataReceived(IAsyncResult Result)
        {
            try
            {
                this.Update();
                int Length = Client.EndReceive(Result);
                if (Length<=0)
                {
                    End();
                    return;
                }
                char[] Chars = new char[Length];
                Constants.Encoding.GetChars(this.Buffer, 0, Length, Chars, 0);
                string Data = new String(Chars);
                if (Data.Contains("<policy-file-request/>"))
                {
                    byte[] XMLPolicy = Utils.Encoding.StringToByteArray("<?xml version=\"1.0\"?>\r\n" +
                                   "<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n" +
                                   "<cross-domain-policy>\r\n" +
                                   "<allow-access-from domain=\"*\" to-ports=\"2001\" />\r\n" +
                                   "</cross-domain-policy>\x0");
                    Client.Send(XMLPolicy);
                }
                else
                {
                    Data = Utils.Encoding.ByteArrayToString(Encryption.Decrypt(Utils.Encoding.StringToByteArray(Data)));
                    if(Data[0] == Convert.ToChar(177))
                    {
                        string[] Datas = Data.Split(Convert.ToChar(177));
                        for(int Pointer = 1; Pointer < Datas.Length; Pointer++)
                        {
                            Invoker MethodInvoker = new Invoker(new ClientMessage(Convert.ToChar(177) + Datas[Pointer]), this);
                        }
                    }
                    else
                    {
                        this.End();
                        return;
                    }
                }
                this.WaitData();
            }
            catch
            {
                this.End();
            }
        }
            
        public void RegisterHandler(Type Type, Handler Instance)
        {
            HandlerCollection HandlerCollection = new HandlerCollection(Type, Instance);
            if(!Handlers.Contains(HandlerCollection))
            {
                Handlers.Add(HandlerCollection);
            }
        }
        public void RegisterDefaultHandlers()
        {
            Handlers.Clear();
            RegisterHandler(typeof(Login), new Login());
            RegisterHandler(typeof(Ping), new Ping());
        }
        public void RegisterFlowerPowerHandlers()
        {
            Handlers.Clear();
            RegisterHandler(typeof(FlowerPower), new FlowerPower());
            RegisterHandler(typeof(Catalog), new Catalog());
            RegisterHandler(typeof(BPad), new BPad());
            RegisterHandler(typeof(Ping), new Ping());
        }
        public void RegisterAreaHandlers()
        {
            Handlers.Clear();
            RegisterHandler(typeof(Catalog), new Catalog());
            RegisterHandler(typeof(Space), new Space());
            RegisterHandler(typeof(ItemsFunctions), new ItemsFunctions());
            RegisterHandler(typeof(Area), new Area());
            RegisterHandler(typeof(BPad), new BPad());
            RegisterHandler(typeof(Ping), new Ping());
        }
        public void Authenticate(User User)
        {
            this.User = User;
            RegisterFlowerPowerHandlers();
        }
        public void SendMessage(ServerMessage Message)
        {
            this.SendEncrypted(Message.GetContent());
        }
        private void SendEncrypted(byte[] Buffer)
        {
            this.SendRaw(Encryption.Encrypt(Buffer));
        }
        private void SendRaw(byte[] Buffer)
        {
            try
            {
                this.Client.Send(Buffer);
            }
            catch
            {
                this.End();
            }
        }
        private void Update()
        {
            this.LastUpdate = DateTime.Now;
        }
        private void Clean()
        {
            try
            {
                this.Checker.Abort();
                if(User != null)
                {
                    if (User.SpaceInstance is AreaInstance)
                    {
                        if(User.SpaceUser.PathFinding != null)
                        {
                            User.SpaceUser.PathFinding.StopWalk();
                        }
                        AreaInstance Area = (AreaInstance)User.SpaceInstance;
                        ServerMessage Message1 = new ServerMessage(new byte[] {128,123},new object[] {User.SpaceUser.ID});
                        Area.SendToAllButMe(Message1, this.ID);
                        AreaInstance Instance = (AreaInstance)User.SpaceInstance;
                        Instance.RemoveUser(this.ID);
                    }
                    if(User.SpaceInstance is SalaInstance)
                    {
                        if(User.SpaceUser.PathFinding != null)
                        {
                            User.SpaceUser.PathFinding.StopWalk();
                        }
                        SalaInstance Sala = (SalaInstance)User.SalaInstance;
                        ServerMessage Message1 = new ServerMessage(new byte[] {128,123},new object[] {User.SpaceUser.ID});
                        Area.SendToAllButMe(Message1, this.ID);
                        SalaInstance Instance = (SalaInstance)User.SalaInstance;
                        Instance.RemoveUser(this.ID);
                    }
                }
            }
            catch { }
        }
        public void End()
        {
            try
            {
                this.Clean();
                this.Client.Close();
                SessionsManager.EndSession(this.ID);
            }
            catch { }
        }
    }
}