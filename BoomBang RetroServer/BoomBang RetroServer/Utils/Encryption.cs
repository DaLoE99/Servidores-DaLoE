using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Utils
{
    class Encryption
    {
        private int DecipheConstant = 135;
        private int DecipheMorph = 0;
        private int EncipheConstant = 135;
        private int EncipheMorph = 0;

        public byte[] Decrypt(byte[] Data)
        {
            int Lenght = Data.Length;
            int ActualByte = 0;
            int Morph = 0;
            byte[] Buffer = new byte[Lenght];
            int Index = 0;
            while (Lenght-- > 0)
            {
                ActualByte = Data[Index];
                Morph = (ActualByte ^ this.DecipheConstant ^ this.DecipheMorph);
                Buffer[Index] = (byte)Morph;
                Index++;
                this.DecipheConstant = Rand(this.DecipheConstant);
                this.DecipheMorph = Morph;
            }
            return Buffer;
        }
        public byte[] Encrypt(byte[] Data)
        {
            int Lenght = Data.Length;
            int ActualByte = 0;
            int Morph = 0;
            byte[] Buffer = new byte[Lenght];
            int Index = 0;
            while (Lenght-- > 0)
            {
                ActualByte = Data[Index];
                Morph = (ActualByte ^ this.EncipheConstant) ^ this.EncipheMorph;
                Buffer[Index] = (byte)Morph;
                Index++;
                this.EncipheConstant = Rand(this.EncipheConstant);
                this.EncipheMorph = Morph;
            }
            return Buffer;
        }
        private int Rand(int n)
        {
            return ((11035245 * n) + 12344);
        }
    }
}
