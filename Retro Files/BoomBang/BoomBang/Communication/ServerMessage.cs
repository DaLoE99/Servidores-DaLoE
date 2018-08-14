namespace BoomBang.Communication
{
    using BoomBang.Config;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ServerMessage
    {
        /* private scope */ List<byte> list_0;
        /* private scope */ ushort ushort_0;
        /* private scope */ ushort ushort_1;
        private ushort p;
        private string p_2;

        public ServerMessage(ushort MessageFlag, ushort MessageItem, bool Special = false)
        {
            if (MessageItem > 0)
            {
                if (!Special)
                {
                    this.ushort_0 = MessageFlag;
                    this.ushort_1 = MessageItem;
                    this.list_0 = new List<byte>();
                    this.list_0.Add(0xb3);
                    this.list_0.Add(0xb2);
                }
                else
                {
                    this.ushort_0 = MessageFlag;
                    this.ushort_1 = MessageItem;
                    this.list_0 = new List<byte>();
                    this.list_0.Add(0xb3);
                }
            }
            else if (!Special)
            {
                this.ushort_0 = MessageFlag;
                this.list_0 = new List<byte>();
                this.list_0.Add(0xb3);
                this.list_0.Add(0xb2);
            }
            else
            {
                this.ushort_0 = MessageFlag;
                this.list_0 = new List<byte>();
                this.list_0.Add(0xb3);
            }
        }

        public ServerMessage(ushort p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        public ServerMessage(string p_2)
        {
            // TODO: Complete member initialization
            this.p_2 = p_2;
        }

        public void AppendByte(byte byte_0)
        {
            this.list_0.Add(byte_0);
        }

        public void AppendBytes(byte[] Data)
        {
            if ((Data != null) && (Data.Length != 0))
            {
                this.list_0.AddRange(Data);
            }
        }

        public void AppendNullParameter(bool Break = false)
        {
            if (!Break)
            {
                this.AppendByte(0xb3);
                this.AppendByte(0xb2);
            }
            else
            {
                this.AppendByte(0xb3);
            }
        }

        public void AppendParameter(bool value, bool Break = false)
        {
            this.AppendParameter(value, Constants.DefaultEncoding, Break);
        }

        public void AppendParameter(double value, bool Break = false)
        {
            this.AppendParameter(value, Constants.DefaultEncoding, Break);
        }

        public void AppendParameter(int value, bool Break = false)
        {
            this.AppendParameter(value, Constants.DefaultEncoding, Break);
        }

        public void AppendParameter(string String, bool Break = false)
        {
            this.AppendParameter(String, Constants.DefaultEncoding, Break);
        } 

        public void AppendParameter(uint value, bool Break = false)
        {
            this.AppendParameter(value, Constants.DefaultEncoding, Break);
        }

        public void AppendParameter(bool value, Encoding Encoding, bool Break = false)
        {
            int num = 0;
            if (value)
            {
                num = 1;
            }
            this.AppendBytes(Encoding.GetBytes(num.ToString()));
            if (!Break)
            {
                this.AppendByte(0xb3);
                this.AppendByte(0xb2);
            }
            else
            {
                this.AppendByte(0xb3);
            }
        }

        public void AppendParameter(double value, Encoding Encoding, bool Break = false)
        {
            this.AppendBytes(Encoding.GetBytes(value.ToString()));
            if (!Break)
            {
                this.AppendByte(0xb3);
                this.AppendByte(0xb2);
            }
            else
            {
                this.AppendByte(0xb3);
            }
        }

        public void AppendParameter(int value, Encoding Encoding, bool Break = false)
        {
            this.AppendBytes(Encoding.GetBytes(value.ToString()));
            if (!Break)
            {
                this.AppendByte(0xb3);
                this.AppendByte(0xb2);
            }
            else
            {
                this.AppendByte(0xb3);
            }
        }

          public void AppendParameter(string String, Encoding Encoding, bool Break = false)
        {
            this.AppendBytes(Encoding.GetBytes(String));
            if (!Break)
            {
                this.AppendByte(0xb3);
                this.AppendByte(0xb2);
            }
            else
            {
                this.AppendByte(0xb3);
            }
        } 

        public void AppendParameter(uint value, Encoding Encoding, bool Break = false)
        {
            this.AppendBytes(Encoding.GetBytes(value.ToString()));
            if (!Break)
            {
                this.AppendByte(0xb3);
                this.AppendByte(0xb2);
            }
            else
            {
                this.AppendByte(0xb3);
            }
        }

        public string BodyToString()
        {
            return Constants.DefaultEncoding.GetString(this.list_0.ToArray());
        }

        public void ClearBody()
        {
            this.list_0.Clear();
        }

        public byte[] GetBytes()
        {
            byte num = (byte) this.ushort_0;
            byte num2 = (byte) this.ushort_1;
            if (num2 > 0)
            {
                byte[] buffer = new byte[this.Length + 5];
                buffer[0] = 0xb1;
                buffer[1] = num;
                buffer[2] = 0xb3;
                buffer[3] = num2;
                for (int j = 0; j < this.Length; j++)
                {
                    buffer[j + 4] = this.list_0[j];
                }
                buffer[buffer.Length - 1] = 0xb0;
                return buffer;
            }
            byte[] buffer2 = new byte[this.Length + 3];
            buffer2[0] = 0xb1;
            buffer2[1] = num;
            for (int i = 0; i < this.Length; i++)
            {
                buffer2[i + 2] = this.list_0[i];
            }
            buffer2[buffer2.Length - 1] = 0xb0;
            return buffer2;
        }

        public override string ToString()
        {
            return ("\x00b1" + this.FlagString + "\x00b3" + this.ItemString + "\x00b3\x00b2" + Constants.DefaultEncoding.GetString(this.list_0.ToArray()) + "\x00b0");
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
                return this.list_0.Count;
            }
        }
    }
}

