using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Sockets.Messages
{
    class ServerMessage
    {
        public void AppendParameter(object Parameter)
        {
            this.Parameters.Add(new object[] { Parameter });
        }
        public string GetData()
        {
            return this.Data;
        }
        public void AppendParameter(object[] ParameterGroup)
        {
            this.Parameters.Add(ParameterGroup);
        }
        public byte[] GetContent()
        {
            List<byte> Message = new List<byte>();
            Message.Add(0xb1);
            foreach(byte ActualHeader in Header)
            {
                Message.Add(ActualHeader);
                Message.Add(0xb3);
            }
            Message.Add(0xb2);
            foreach(object[] ParametersGroup in Parameters)
            {
                if(ParametersGroup != null)
                {
                    foreach(object Parameter in ParametersGroup)
                    {
                        if(Parameter != null)
                        {
                            foreach(byte ParameterByte in Utils.Encoding.StringToByteArray(Parameter.ToString()))
                            {
                                Message.Add(ParameterByte);
                            }
                        }
                        Message.Add(0xb3);
                    }
                }
                else
                {
                    Message.Add(0xb3);
                }
                Message.Add(0xb2);
            }
            Message.Add(0xb0);
            return Message.ToArray();
        }
        public string ToString()
        {
            //32N
        }
    }
}