using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Game.Spaces.Areas
{
    public class AreaData : ICloneable
    {
        public int ID;
        public string Name;
        public Map Map;
        public int MaxVisitors;
        public int X;
        public int Y;
        public int Type;
        public bool Coconut;
        public bool Uppercut;
        public bool Game = false;
        public int Visitors;
        public bool Chests = false;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
