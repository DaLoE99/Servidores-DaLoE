using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Game.Chests
{
    class ChestData : ICloneable
    {
        public int ID1;
        public int ID;
        public int Appear;
        public int Speed;
        public int Amount;
        public string Amount_type;
        public int ItemID;
        public int ContestID;
        public string Message;
        public int Time;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
