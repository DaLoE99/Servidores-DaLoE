namespace BoomBang.Game.Pathfinding
{
    using BoomBang.Config;
    using System;

    public static class PathfinderManager
    {
        public static Pathfinder CreatePathfinderInstance()
        {
            string str = ConfigManager.GetValue("pathfinder.mode").ToString().ToLower();
            if (str != null)
            {
                bool flag1 = str == "simple";
            }
            return new Parsing();
        }
    }
}

