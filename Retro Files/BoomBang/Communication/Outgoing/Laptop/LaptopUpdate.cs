using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Game.Characters;

namespace Snowlight.Communication.Outgoing
{
    class LaptopUpdate
    {
        private CharacterInfo characterInfo_0;
        private int int_0;

        public LaptopUpdate(int Mode, CharacterInfo CharacterInfo)
        {
            this.int_0 = Mode;
            this.characterInfo_0 = CharacterInfo;
        }

        public CharacterInfo CharacterInfo
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
