namespace BoomBang_RetroServer.Utils
{
    class Encoding
    {
        public static byte[] StringToByteArray(string data)
        {
            if (data != "")
            {
                return Constants.Encoding.GetBytes(data);
            }

            return null;
        }
        public static string ByteArrayToString(byte[] data)
        {
            if (data != null)
            {
                return Constants.Encoding.GetString(data);
            }

            return null;
        }
    }
}
