namespace BoomBang.Game.Achievements
{
    using System;

    public class Achievement
    {
        /* private scope */ uint uint_0;
        /* private scope */ uint uint_1;
        /* private scope */ uint uint_2;
        /* private scope */ uint uint_3;
        /* private scope */ uint uint_4;

        public Achievement(uint uint_5, uint Type, uint Reward1, uint Reward2, uint Reward3)
        {
            this.uint_0 = uint_5;
            this.uint_1 = Type;
            this.uint_2 = Reward1;
            this.uint_3 = Reward2;
            this.uint_4 = Reward3;
        }

        public uint Reward1
        {
            get
            {
                return this.uint_2;
            }
        }

        public uint Reward2
        {
            get
            {
                return this.uint_3;
            }
        }

        public uint Reward3
        {
            get
            {
                return this.uint_4;
            }
        }

        public uint Type
        {
            get
            {
                return this.uint_1;
            }
        }

        public uint UInt32_0
        {
            get
            {
                return this.uint_0;
            }
        }
    }
}

