using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Game.Items
{
    class Furnil : ICloneable
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
        
        public Furnil(int ID, int ItemID, int UserID, int Sala_id, int X, int Y, string something_3, string size, string Color, string RGB, string something_4, int Height)
        {
            this.ID = ID;
            this.ItemID = ItemID;
            this.Sala_id = Sala_id;
            this.UserID = UserID;
            this.something_3 = something_3;
            this.size = size;
            this.Color = Color;
            this.RGB = RGB;
            this.something_4 = something_4;
            this.X = X;
            this.Y = Y;
            this.Height = Height;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
