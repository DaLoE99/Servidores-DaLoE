using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using BoomBang_RetroServer.Sessions;
using BoomBang_RetroServer.Sockets.Messages;


namespace BoomBang_RetroServer.Game.Spaces.Salas
{
    public class SalaInstance
    {
        public SalaData SalaData;
        private Dictionary<int, long> Users = new Dictionary<int, long>();
        private Dictionary<long, Session> Sessions = new Dictionary<long,Session>();
        public Dictionary<int, int> Chests = new Dictionary<int, int>();
        private Thread SalaInteractor;

        public SalaInstance(int ID, SalaData SalaData)
        {
            this.SalaData = (SalaData)SalaData.Clone();
            this.SalaData.Name += " " + ID;
            this.SalaInteractor = new Thread(new ThreadStart(SalaInteractorVoid));
            try
            {
                SalaInteractor.Start();
            }
            catch(Exception Exception)
            {
                Output.WriteLine(Exception.ToString());
                SalaInteractor.Abort();
            }
            SalaData.ID = SpacesManager.AssignID(this);
        }
        private void SalaInteractorVoid()
        {
            //Thread.Sleep(4000);
            //if(this.SalaData.Visitors >= 3)
            //{
            //    Random Rand = new Random();
            //    while(true)
            //    {
            //        int[] num = new int[] { 0, 1, 2, 300, 301, 302, 303 };
            //        int num5 = new Random().Next(0, num.Length - 1);

            //        Thread.Sleep(Rand.Next(0, 200000));
            //        int ID = Rand.Next(1000, 9999);
            //        while (this.Chests.ContainsKey(ID))
            //        {
            //            ID = Rand.Next(1000, 9999);
            //        }
            //        Point Position = SalaData.Map.GetRandomPlace();
            //        this.Chests.Add(ID, num[num5]);
            //        this.SendToAll(new ServerMessage(new byte[] { 200, 120 }, new object[] { ID, 2, Position.X, Position.Y, num[num5], 1, 0, 2 }));
            //        Thread.Sleep(7500);
            //        if(this.Chests.ContainsKey(ID))
            //        {
            //            this.SendToAll(new ServerMessage(new byte[] { 200, 123 }, new object[] { 1, ID }));
            //            this.Chests.Remove(ID);
            //        }
            //    }
            //}
        }
        public void RemoveSala()
        {
            SalaInteractor.Abort();
            ServerMessage Message1 = new ServerMessage(new byte[] { 135 });
            SendToAll(Message1);
        }
        public void SendToAll(ServerMessage ServerMessage)
        {
            foreach (Session Session in Sessions.Values)
            {
                Session.SendMessage(ServerMessage);
            }
        }
    }
}
