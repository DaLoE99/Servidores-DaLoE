namespace BoomBang.Communication
{
    using BoomBang.Config;
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ClientMessage
    {
        /* private scope */
        byte[] byte_0;
        /* private scope */
        int int_0;
        /* private scope */
        int int_1;
        /* private scope */
        ushort ushort_0;
        /* private scope */
        ushort ushort_1;

        public ClientMessage(ushort MessageFlag, ushort MessageItem, byte[] Body)
        {
            if (Body == null)
            {
                Body = new byte[0];
            }
            this.ushort_0 = MessageFlag;
            this.ushort_1 = MessageItem;
            this.byte_0 = Body;
            this.int_0 = 0;
            this.int_1 = 0;
        }

        public string BodyToString()
        {
            return Constants.DefaultEncoding.GetString(this.byte_0);
        }

        public string GetParameter(Encoding Encoding)
        {
            return Encoding.GetString(this.ReadBytes(false));
        }

        private void method_0(int int_2)
        {
            this.int_0 += int_2;
        }

        public byte[] PlainReadBytes(int Bytes)
        {
            if (Bytes > this.RemainingLength)
            {
                Bytes = this.RemainingLength;
            }
            byte[] buffer = new byte[Bytes];
            int index = 0;
            for (int i = this.int_0; index < Bytes; i++)
            {
                buffer[index] = this.byte_0[i];
                index++;
            }
            return buffer;
        }

        public byte[] ReadBytes(bool IsBreakString = false)
        {
            if (IsBreakString)
            {
                string[] strArray2 = Constants.DefaultEncoding.GetString(this.byte_0).Split(new char[] { '\x00b3', '\x00b2' }, StringSplitOptions.RemoveEmptyEntries)[this.int_0].Split(new char[] { '\x00b3' }, StringSplitOptions.RemoveEmptyEntries);
                return Constants.DefaultEncoding.GetBytes(strArray2[this.int_1++]);
            }
            string[] strArray3 = Constants.DefaultEncoding.GetString(this.byte_0).Split(new char[] { '\x00b3', '\x00b2' }, StringSplitOptions.RemoveEmptyEntries);
            return Constants.DefaultEncoding.GetBytes(strArray3[this.int_0++]);
        }

        public int ReadInteger()
        {
            int result = 0;
            int.TryParse(this.GetParameter(Constants.DefaultEncoding).ToString(), out result);
            return result;
        }

        public string ReadString()
        {
            return this.GetParameter(Constants.DefaultEncoding);
        }

        public uint ReadUnsignedInteger()
        {
            uint result = 0;
            uint.TryParse(this.GetParameter(Constants.DefaultEncoding).ToString(), out result);
            return result;
        }

        public void ResetPointer()
        {
            this.int_0 = 0;
        }

        public override string ToString()
        {
            if (this.ushort_1.Equals((ushort)0))
            {
                return (this.FlagString + "\x00b3\x00b2" + this.BodyToString());
            }
            return (this.FlagString + "\x00b3" + this.ItemString + "\x00b3\x00b2" + this.BodyToString());
        }

        public ushort Flag
        {
            get
            {
                return this.ushort_0;
            }
        }

        public string FlagString
        {
            get
            {
                return Convert.ToChar(this.ushort_0).ToString();
            }
        }

        public ushort Item
        {
            get
            {
                return this.ushort_1;
            }
        }

        public string ItemString
        {
            get
            {
                return Convert.ToChar(this.ushort_1).ToString();
            }
        }

        public int Length
        {
            get
            {
                return this.byte_0.Length;
            }
        }

        public int RemainingLength
        {
            get
            {
                return (this.byte_0.Length - this.int_0);
            }
        }
    }
}

