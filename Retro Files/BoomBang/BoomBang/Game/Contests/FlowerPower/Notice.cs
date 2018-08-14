namespace BoomBang.Game.FlowerPower
{
    using BoomBang;
    using System;

    public class Notice
    {
        /* private scope */
        string string_0;
        /* private scope */
        string string_1;
        /* private scope */
        string string_2;
        /* private scope */
        string string_3;
        /* private scope */
        uint uint_0;

        public Notice(uint uint_1, double Timestamp, string Title, string Content, string Image)
        {
            this.uint_0 = uint_1;
            this.string_0 = UnixTimestamp.GetDateTimeFromUnixTimestamp(Timestamp).ToString("yyyy-MM-dd HH:mm:ss");
            this.string_1 = Title;
            this.string_2 = Content;
            this.string_3 = Image;
        }

        public string Content
        {
            get
            {
                return this.string_2;
            }
        }

        public string Date
        {
            get
            {
                return this.string_0;
            }
        }

        public string Image
        {
            get
            {
                return this.string_3;
            }
        }

        public string Title
        {
            get
            {
                return this.string_1;
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

