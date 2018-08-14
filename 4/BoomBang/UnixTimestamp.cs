namespace BoomBang
{
    using System;

    public static class UnixTimestamp
    {
        public static double GetCurrent()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - new DateTime(0x7b2, 1, 1, 0, 0, 0));
            return span.TotalSeconds;
        }

        public static DateTime GetDateTimeFromUnixTimestamp(double Timestamp)
        {
            DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0, 0);
            return time.AddSeconds(Timestamp);
        }
    }
}

