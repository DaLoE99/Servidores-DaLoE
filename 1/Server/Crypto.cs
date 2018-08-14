using System;
using System.Data;
using System.Collections.Generic;

using Boombang.sockets;
using Boombang.game.session;
using Boombang.database;
using Boombang.game;

namespace Boombang
{
    class Crypto
    {
        private static int A = 0x87;
        private static int B = 0;
        private static int C = 0x87;
        private static int DecipherConstant = 135;
        private static int DecipherMorph = 0;
        private static int EncipherConstant = 0x87;
        private static int EncipherMorph = 0;
        private static int M = 0;
        private static int PebeteCOL = 0;
        private static int PebeteCUL = 0x87;

        public static byte[] Deciphe(byte[] Data, int Length)
        {
            int num = 0;
            int num2 = 0;
            byte[] buffer = new byte[Length];
            int index = 0;
            while (Length-- > 0)
            {
                num = Data[index];
                num2 = num ^ DecipherConstant ^ DecipherMorph;
                buffer[index] = (byte)num2;
                index++;
                DecipherConstant = Rand(DecipherConstant);
                DecipherMorph = num2;
            }
            return buffer;
        }

        public static byte[] Encrypt2(byte[] Data, int Length)
        {
            int num = 0;
            int num2 = 0;
            byte[] buffer = new byte[Length];
            int index = 0;
            while (Length-- > 0)
            {
                num = Data[index];
                num2 = (num ^ C) ^ M;
                buffer[index] = (byte)num2;
                index++;
                C = Rand(C);
                M = num;
            }
            return buffer;
        }

        private static int Rand(int n)
        {
            return ((1103515245 * n) + 12344);
        }
    }
}