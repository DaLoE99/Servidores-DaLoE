namespace BoomBang.Game.Misc
{
    using BoomBang.Config;
    using System;
    using System.IO;
    using System.Text;

    public static class CrossdomainPolicy
    {
        /* private scope */ static string string_0;

        public static byte[] GetBytes()
        {
            return GetBytes(Constants.DefaultEncoding);
        }

        public static byte[] GetBytes(Encoding Encoding)
        {
            return Encoding.GetBytes(string_0);
        }

        public static void Initialize(string Path)
        {
            if (!File.Exists(Path))
            {
                throw new ArgumentException("Crossdomain policy file not found at: " + Path + ".");
            }
            string_0 = File.ReadAllText(Path);
        }

        public static string PolicyText
        {
            get
            {
                return string_0;
            }
        }
    }
}

