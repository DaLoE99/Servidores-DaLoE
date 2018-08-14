namespace BoomBang.Game.Moderation
{
    using System;

    public class BanDetails
    {
        /* private scope */ double double_0;
        /* private scope */ double double_1;
        /* private scope */ string string_0;
        /* private scope */ string string_1;
        string string_2;
        /* private scope */ uint uint_0;
        /* private scope */ uint uint_1;

        public BanDetails(uint UserId, uint BanType, string Reason, double Timestamp, double TimestampEx, string Moderator)
        {
            this.uint_0 = UserId;
            this.uint_1 = BanType;
            this.string_0 = Reason;
            this.double_0 = Timestamp;
            this.double_1 = TimestampEx;
            this.string_1 = Moderator;
        }

        public BanDetails(uint UserId, uint BanType, string Reason, double Timestamp, double TimestampEx, string Moderator, string IP)
        {
            this.uint_0 = UserId;
            this.uint_1 = BanType;
            this.string_0 = Reason;
            this.double_0 = Timestamp;
            this.double_1 = TimestampEx;
            this.string_1 = Moderator;
            this.string_2 = IP;
        }

        public string IP
        {
            get
            {
                return this.string_2;
            }
        }

        public uint BanType
        {
            get
            {
                return this.uint_1;
            }
        }

        public string Moderator
        {
            get
            {
                return this.string_1;
            }
        }

        public string Reason
        {
            get
            {
                return this.string_0;
            }
        }

        public double Timestamp
        {
            get
            {
                return this.double_0;
            }
        }

        public double TimestampEx
        {
            get
            {
                return this.double_1;
            }
        }

        public uint UserId
        {
            get
            {
                return this.uint_0;
            }
        }
    }
}

