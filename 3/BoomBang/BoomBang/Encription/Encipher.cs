namespace BoomBang.Encription
{
    using System;

    public class Encipher
    {
        /* private scope */ int int_0 = 0x87;
        /* private scope */ int int_1 = 0x87;
        /* private scope */ int int_2;
        /* private scope */ int int_3;

        public byte[] Deciphe(byte[] Data, int Length)
        {
            int num = 0;
            int num2 = 0;
            byte[] buffer = new byte[Length];
            int index = 0;
            while (Length-- > 0)
            {
                num = Data[index];
                num2 = (num ^ this.int_0) ^ this.int_2;
                buffer[index] = (byte) num2;
                index++;
                this.int_0 = this.method_0(this.int_0);
                this.int_2 = num2;
            }
            return buffer;
        }

        public byte[] Enciphe(byte[] Data, int Length)
        {
            int num = 0;
            int num2 = 0;
            byte[] buffer = new byte[Length];
            int index = 0;
            while (Length-- > 0)
            {
                num = Data[index];
                num2 = (num ^ this.int_1) ^ this.int_3;
                buffer[index] = (byte) num2;
                index++;
                this.int_1 = this.method_0(this.int_1);
                this.int_3 = num;
            }
            return buffer;
        }

        private int method_0(int int_4)
        {
            return ((0x41c64e6d * int_4) + 0x3039);
        }
    }
}

