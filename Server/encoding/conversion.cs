namespace Boombang.encoding
{
    public static class conversion
    {
        public static byte[] stringToByteArray(string data)
        {
            if (data != "")
            {
                return System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(data);
            }

            return null;
        }
    }
}
