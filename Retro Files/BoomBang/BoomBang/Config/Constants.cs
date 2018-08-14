namespace BoomBang.Config
{
    using System;
    using System.Text;

    public static class Constants
    {
        public static readonly string ConsoleTitle = "BoomBang";
        public static readonly int ConsoleWindowHeight = 30;
        public static readonly int ConsoleWindowWidth = 0x5f;
        public static readonly string DataFileDirectory = (Environment.CurrentDirectory + @"\data");
        public static readonly Encoding DefaultEncoding = Encoding.Default;
        public static readonly char LineBreakChar = Convert.ToChar(13);
        public static readonly string LogFileDirectory = (Environment.CurrentDirectory + @"\logs");
    }
}

