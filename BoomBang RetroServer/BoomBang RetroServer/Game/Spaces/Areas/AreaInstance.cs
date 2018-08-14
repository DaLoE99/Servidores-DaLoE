using BoomBang_RetroServer.Sessions;
using BoomBang_RetroServer.Game.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BoomBang_RetroServer.Game.Chests;
using BoomBang_RetroServer.Sockets.Messages;

namespace BoomBang_RetroServer.Game.Spaces.Areas
{
    public class AreaInstance
    {
        public AreaData AreaData;
        private Dictionary<int, long> Users = new Dictionary<int, long>();
        private Dictionary<long, Session> Sessions = new Dictionary<long, Session>();
        public Dictionary<int, int> Chests = new Dictionary<int, int>();
        private Thread AreaInteractor;

        public AreaInstance(int ID, AreaData AreaData)
        {
            this.AreaData = (AreaData)AreaData.Clone();
            this.AreaData.Name += " " + ID;
            this.AreaInteractor = new Thread(new ThreadStart(AreaInteractorVoid));
            try
            {
                AreaInteractor.Start();
            }
            catch (Exception Exception)
            {
                Output.WriteLine(Exception.ToString());
                AreaInteractor.Abort();
            }
            AreaData.ID = SpacesManager.AssignID(this);
        }
        private void AreaInteractorVoid()
        {
            Thread.Sleep(4000);
            Random Rand = new Random();
            while (true)
            {
                int[] num = ChestManager.ChestsIDS;
                int num5 = new Random().Next(0, num.Length - 1);
                ChestData Chest = ChestManager.Chests[num[num5]];
                Output.WriteLine(Chest.ID.ToString(), OutputLevel.Information);
                Thread.Sleep(Rand.Next(4000, 40000));
                int ID = Rand.Next(1000, 9999);
                while (this.Chests.ContainsKey(ID))
                {
                    ID = Rand.Next(1000, 9999);
                }
                Point Position = AreaData.Map.GetRandomPlace();
                this.Chests.Add(ID, Chest.ID);
                this.SendToAll(new ServerMessage(new byte[] { 200, 120 }, new object[] { ID, 2, Position.X, Position.Y, Chest.ID, Chest.Appear, 0, Chest.Speed }));
                Thread.Sleep(Chest.Time);
                if (this.Chests.ContainsKey(ID))
                {
                    this.SendToAll(new ServerMessage(new byte[] { 200, 123 }, new object[] { 1, ID }));
                    this.Chests.Remove(ID);
                }
            }
        }
        public User GetUser(long SessionID)
        {
            if(Sessions.ContainsKey(SessionID))
            {
                return SessionsManager.GetSession(SessionID).User;
            }
            else
            {
                return null;
            }
        }
        public long GetSession(string UserName)
        {
            foreach(long SessionID in Sessions.Keys)
            {
                User ActualUser = SessionsManager.GetSession)//numero 37
            }
        }
    }
}
