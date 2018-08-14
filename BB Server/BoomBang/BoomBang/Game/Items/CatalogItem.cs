namespace BoomBang.Game.Items
{
    using System;

    public class CatalogItem
    {
        /* private scope */ bool bool_0;
        /* private scope */ double double_0;
        /* private scope */ double double_1;
        /* private scope */ double double_2;
        /* private scope */ int int_0;
        /* private scope */ int int_1;
        /* private scope */ int int_2;
        /* private scope */ string string_0;
        /* private scope */ string string_1;
        /* private scope */ string string_10;
        /* private scope */ string string_2;
        /* private scope */ string string_3;
        /* private scope */ string string_4;
        /* private scope */ string string_5;
        /* private scope */ string string_6;
        /* private scope */ string string_7;
        /* private scope */ string string_8;
        /* private scope */ string string_9;
        /* private scope */ uint uint_0;
        /* private scope */ uint uint_1;
        /* private scope */ uint uint_2;
        /* private scope */ uint uint_3;
        /* private scope */ uint uint_4;
        /* private scope */ uint uint_5;
        /* private scope */ uint uint_6;
        /* private scope */ uint uint_7;

        public CatalogItem(uint uint_8, string Base, string Name, string Description, int CostGoldCoins, int CostSilverCoins, int Category, string Color, string ServerColor, string PartColor, string ServerPartColor, string PartName1, string PartName2, string PartName3, string PartName4, double Size1, double Size2, double Size3, uint Type, uint FilledSpace, uint Ubication, bool Active, uint TypeRare, uint Sizable, uint Tradeable, uint MemberRestriction)
        {
            this.uint_0 = uint_8;
            this.string_0 = Base;
            this.string_1 = Name;
            this.string_2 = Description;
            this.int_0 = CostGoldCoins;
            this.int_1 = CostSilverCoins;
            this.int_2 = Category;
            this.string_3 = Color;
            this.string_4 = ServerColor;
            this.string_5 = PartColor;
            this.string_6 = ServerPartColor;
            this.string_7 = PartName1;
            this.string_8 = PartName2;
            this.string_9 = PartName3;
            this.string_10 = PartName4;
            this.double_0 = Size1;
            this.double_1 = Size2;
            this.double_2 = Size3;
            this.uint_1 = Type;
            this.uint_2 = FilledSpace;
            this.uint_3 = Ubication;
            this.bool_0 = Active;
            this.uint_4 = TypeRare;
            this.uint_5 = Sizable;
            this.uint_6 = Tradeable;
            this.uint_7 = MemberRestriction;
        }

        public bool Active
        {
            get
            {
                return this.bool_0;
            }
        }

        public string BaseName
        {
            get
            {
                return this.string_0;
            }
        }

        public int CatalogCategory
        {
            get
            {
                return this.int_2;
            }
        }

        public string Color
        {
            get
            {
                return this.string_3;
            }
        }

        public int CostGoldCoins
        {
            get
            {
                return this.int_0;
            }
        }

        public int CostSilverCoins
        {
            get
            {
                return this.int_1;
            }
        }

        public string DisplayDescription
        {
            get
            {
                return this.string_2;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.string_1;
            }
        }

        public uint FilledSpace
        {
            get
            {
                return this.uint_2;
            }
        }

        public uint MemberRestriction
        {
            get
            {
                return this.uint_7;
            }
        }

        public string PartColor
        {
            get
            {
                return this.string_5;
            }
        }

        public string PartName1
        {
            get
            {
                return this.string_7;
            }
        }

        public string PartName2
        {
            get
            {
                return this.string_8;
            }
        }

        public string PartName3
        {
            get
            {
                return this.string_9;
            }
        }

        public string PartName4
        {
            get
            {
                return this.string_10;
            }
        }

        public string ServerColor
        {
            get
            {
                return this.string_4;
            }
        }

        public string ServerPartColor
        {
            get
            {
                return this.string_6;
            }
        }

        public uint Sizable
        {
            get
            {
                return this.uint_5;
            }
        }

        public double Size1
        {
            get
            {
                return this.double_0;
            }
        }

        public double Size2
        {
            get
            {
                return this.double_1;
            }
        }

        public double Size3
        {
            get
            {
                return this.double_2;
            }
        }

        public uint Tradeable
        {
            get
            {
                return this.uint_6;
            }
        }

        public uint Type
        {
            get
            {
                return this.uint_1;
            }
        }

        public uint TypeRare
        {
            get
            {
                return this.uint_4;
            }
        }

        public uint Ubication
        {
            get
            {
                return this.uint_3;
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

