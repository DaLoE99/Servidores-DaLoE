using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snowlight.Game.Pathfinding
{
    class PathfinderManager
    {
        public static Pathfinder CreatePathfinderInstance()
        {
            string str = "simple".ToLower();
            if (str != null)
            {
                bool flag1 = str == "simple";
            }
            return new Parsing();
        }
    }
}
