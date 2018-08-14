using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Game.Items
{
    public class Furni : ICloneable
    {
        public int ID;
        public int ItemID;
        public int Sala_id;
        public int UserID;
        public int X;
        public int Y;
        public string something_3;
        public string size;
        public string Color;
        public string RGB;
        public string something_4;
        public int Height;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
