namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Game.Characters;
    using System;

    public class LaptopUpdate
    {
        /* private scope */ BoomBang.Game.Characters.CharacterInfo characterInfo_0;
        /* private scope */ int int_0;

        public LaptopUpdate(int Mode, BoomBang.Game.Characters.CharacterInfo CharacterInfo)
        {
            this.int_0 = Mode;
            this.characterInfo_0 = CharacterInfo;
        }

        public BoomBang.Game.Characters.CharacterInfo CharacterInfo
        {
            get
            {
                return this.characterInfo_0;
            }
        }

        public int Mode
        {
            get
            {
                return this.int_0;
            }
        }
    }
}

