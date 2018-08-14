using BoomBang_RetroServer.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Game.Spaces.Islas
{
    public class IslaInstance
    {
        public IslaData IslaData;
        private Dictionary<int, long> Users = new Dictionary<int, long>();
        private Dictionary<long, Session> Sessions = new Dictionary<long, Session>();
        public Dictionary<int, int> Chests = new Dictionary<int, int>();
        private Thread AreaInteractor;

        public IslaInstance(int ID, IslaData IslaData)
        {
            this.IslaData = (IslaData)IslaData.Clone();
            this.IslaData.Name += " " + ID;
            this.IslaInteractor = new Thread(new ThreadStart(IslaInteractorVoid));
            try
            {
                IslaInteractor.Start();
            }
            catch (Exception Exception)
            {
                Output.WriteLine(Exception.ToString());
                IslaInteractor.Abort();
            }
            IslaData.ID = SpacesManager.AssignID(this);
        }
        private void IslaInteractorVoid()
        {}
    }
}
