namespace BoomBang.Utils
{
    using System;

    public static class ByteUtil
    {
        public static byte[] SubByte(byte[] Bytes, int Offset, int ByteCount)
        {
            int length = Offset + ByteCount;
            if (length > Bytes.Length)
            {
                length = Bytes.Length;
            }
            if (ByteCount > Bytes.Length)
            {
                ByteCount = Bytes.Length;
            }
            if (ByteCount < 0)
            {
                ByteCount = 0;
            }
            byte[] buffer = new byte[ByteCount];
            for (int i = 0; i < ByteCount; i++)
            {
                buffer[i] = Bytes[Offset++];
            }
            return buffer;
        }
    }
}

