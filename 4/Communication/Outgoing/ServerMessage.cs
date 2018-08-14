using System;
using System.Collections.Generic;
using System.Text;

using Snowlight.Util;
using Snowlight.Config;

namespace Snowlight.Communication
{
    public class ServerMessage
    {
        private uint mMessageId;
        private List<byte> mBody;

        public uint Id
        {
            get
            {
                return mMessageId;
            }
        }

        public byte[] HeaderString
        {
            get
            {
                //000111
                string ID = mMessageId.ToString().Substring(0, 3);
                string Typee;
                try
                {
                    Typee = (mMessageId.ToString().Length > 4) ? mMessageId.ToString().Substring(3, 3) : "" + 0;
                }
                catch
                {
                    Typee = (mMessageId.ToString().Length > 4) ? mMessageId.ToString().Substring(3, 2) : "" + 0;
                }

                byte id = (byte)int.Parse(ID);
                byte type = (byte)int.Parse(Typee);

                byte[] IDS = { id, type };
                string trol = Encoding.Default.GetString(IDS);
                //Console.WriteLine("TRLOTLROTLOR -> " + trol + "sadasfasf-> " + id + "" + type);
                
                if (mMessageId.ToString().Length < 5)
                    return new byte[] { 177, id, 179, 178 };
                    //return "±" + trol.Substring(0,1) + "³²";
                else
                    return new byte[] { 177, id, 179, type, 179, 178 };
                    //return "±" + trol.Substring(0, 1) + "³" + trol.Substring(1, 1) + "³²";
            }
        }

        public int Length
        {
            get
            {
                return mBody.Count;
            }
        }

        public ServerMessage(uint MessageId)
        {
            mMessageId = MessageId;
            mBody = new List<byte>();
        }

        public override string ToString()
        {
            return Constants.DefaultEncoding.GetString(mBody.ToArray()) + "°";
        }

        public byte[] GetBytes()
        {
            byte[] b = new byte[Encoding.Default.GetBytes(ToString()).Length + HeaderString.Length];
            System.Buffer.BlockCopy( HeaderString, 0, b, 0, HeaderString.Length );
            System.Buffer.BlockCopy( Encoding.Default.GetBytes(ToString()), 0, b, HeaderString.Length, Encoding.Default.GetBytes(ToString()).Length );
            return b;
        }

        public string BodyToString()
        {
            return Constants.DefaultEncoding.GetString(mBody.ToArray());
        }

        public void ClearBody()
        {
            mBody.Clear();
        }

        public void AppendByte(byte b)
        {
            mBody.Add(b);
            byte i = Convert.ToByte("³²");
            mBody.Add(i);
        }

        public void AppendBytes(byte[] Data)
        {
            if (Data == null || Data.Length == 0)
            {
                return;
            }

            mBody.AddRange(Data);
        }

        public void Append(string String)
        {

            AppendBytes(Encoding.Default.GetBytes(String + "³²"));
        }

        public void Append(Int64 i)
        {
            AppendBytes(Encoding.Default.GetBytes(i + "³²"));
        }

        public void Append(bool b)
        {
            if (b)
                AppendBytes(Encoding.Default.GetBytes(1 + "³²"));
            else
                AppendBytes(Encoding.Default.GetBytes(0 + "³²"));   
        }

        public void AppendParameter(string s, bool b)
        {
            if (b)
                AppendBytes(Encoding.Default.GetBytes(s + "³"));
            else
                AppendBytes(Encoding.Default.GetBytes(s + "³²"));
        }
        public void AppendParameter(Int64 s, bool b)
        {
            if (b)
                AppendBytes(Encoding.Default.GetBytes(s + "³"));
            else
                AppendBytes(Encoding.Default.GetBytes(s + "³²"));
        }
        public void AppendParameter(bool s, bool b)
        {
            if (b && s)
                AppendBytes(Encoding.Default.GetBytes(1 + "³"));
            else if (!b && s)
                AppendBytes(Encoding.Default.GetBytes(1 + "³²"));
            else if (b && !s)
                AppendBytes(Encoding.Default.GetBytes(0 + "³"));
            else
                AppendBytes(Encoding.Default.GetBytes(0 + "³²"));
        }
        public void AppendNullParameter(bool b)
        {
            if (b)
                AppendBytes(Encoding.Default.GetBytes("³"));
            else
                AppendBytes(Encoding.Default.GetBytes("³²"));
        }
    }
}
