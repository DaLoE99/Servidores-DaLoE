namespace BoomBang.Game.Contests
{
    using BoomBang.Specialized;
    using BoomBang.Storage;
    using System;

    public class ContestItem
    {
        /* private scope */ bool bool_0;
        /* private scope */ string string_0;
        /* private scope */ string string_1;
        /* private scope */ uint uint_0;
        /* private scope */ uint uint_1;
        /* private scope */ uint uint_2;
        /* private scope */ uint uint_3;
        /* private scope */ uint uint_4;
        /* private scope */ uint uint_5;
        /* private scope */ uint uint_6;
        /* private scope */ uint uint_7;
        /* private scope */ uint uint_8;
        /* private scope */ Vector2 vector2_0;

        public ContestItem(uint uint_9, uint SpaceId, uint DefinitionId, uint RankingId, Vector2 Position, uint GoldCredits, uint SilverCredits, uint ObjectId, string DefinitionName, string ShowName, uint FallType, uint CatchType)
        {
            this.uint_0 = uint_9;
            this.uint_1 = SpaceId;
            this.vector2_0 = Position;
            this.uint_2 = DefinitionId;
            this.uint_3 = RankingId;
            this.uint_4 = GoldCredits;
            this.uint_5 = SilverCredits;
            this.uint_6 = ObjectId;
            this.string_0 = DefinitionName;
            this.string_1 = ShowName;
            this.uint_7 = FallType;
            this.uint_8 = CatchType;
            this.bool_0 = true;
        }

        public void CatchItem(SqlDatabaseClient MySqlClient, int CharacterId)
        {
            this.bool_0 = false;
            if (this.uint_3 > 0)
            {
                MySqlClient.SetParameter("CharacterId", CharacterId);
                MySqlClient.ExecuteNonQuery(string.Concat(new object[] { "UPDATE rankings SET puntos_", this.uint_3, " = puntos_", this.uint_3, " + 1 WHERE usuario = @CharacterId LIMIT 1" }));
            }
        }

        public uint CatchType
        {
            get
            {
                return this.uint_8;
            }
        }

        public uint DefinitionId
        {
            get
            {
                return this.uint_2;
            }
        }

        public string DefinitionName
        {
            get
            {
                return this.string_0;
            }
        }

        public uint FallType
        {
            get
            {
                return this.uint_7;
            }
        }

        public uint GoldCredits
        {
            get
            {
                return this.uint_4;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.bool_0;
            }
        }

        public uint ObjectId
        {
            get
            {
                return this.uint_6;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.vector2_0;
            }
        }

        public uint RankingId
        {
            get
            {
                return this.uint_3;
            }
        }

        public string ShowName
        {
            get
            {
                return this.string_1;
            }
        }

        public uint SilverCredits
        {
            get
            {
                return this.uint_5;
            }
        }

        public uint SpaceId
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

