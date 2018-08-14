using System;
using System.Text.RegularExpressions;

namespace Boombang.sockets.messages
{
    public struct client
    {
        private string data_total;
        private string data_toprocess;
        public readonly string id;
        public readonly string type;

        public string content;
        private int pointer;

        public client(string data)
        {
            data = data + "°";
            this.data_total = data;
            this.data_toprocess = data.Substring(1, data.Length - 4);
            if (data_toprocess.Length > 2)
            {
                this.id = data_toprocess.Substring(0, 1);
                this.type = data_toprocess.Substring(2, 1);
                if(data_toprocess.Length > 5)
                    this.content = data_toprocess.Substring(5);
                else
                    this.content = string.Empty;
            }
            else
            {
                this.id = data_toprocess.Substring(0, 1);
                this.type = string.Empty;
                this.content = string.Empty;
            }

            this.pointer = 0;
        }

        public string get_content() { return content; }
        public string client_content { get { return get_content(); } }
        public string Tostring() { return data_total; }
        public string getParameter()
        {
            string[] data = Regex.Split(content, "³²");
            int length = data.Length;
            int pointer_now = pointer;

            pointer++;

            if (pointer_now < length)
                return data[pointer_now];
            else
                return string.Empty;
        }

        public int decoded_id { get { return Convert.ToByte(Convert.ToChar(id)); } }

        public int decoded_type
        {
            get
            {
                if (!(string.IsNullOrEmpty(type)))
                    return Convert.ToByte(Convert.ToChar(type));
                else
                    return 0;
            }
        }
    }
}

/*using System;
using System.Text.RegularExpressions;

namespace Boombang.sockets.messages
{
    public struct client
    {
            public readonly string ID;
            public readonly string Type;
            public string Content;
            private int Pointer;
            private string data;

            public client(string Data)
            {
                this.ID = Data.Substring(1, 1);
                this.Type = Data.Substring(3, 1);
                if (Data.Length > 9)
                    this.Content = Data.Substring(6, Data.Length - 9);
                else
                    this.Content = null;
                this.data = Data.Substring(1);

                this.Pointer = 0;
            }

            public string getBody()  {  return Content.Substring(Pointer);  }

            public string Body  {  get { return getBody(); }  }

            public int decodedID
            {
                get { return Convert.ToByte(Convert.ToChar(ID)); }
            }

            public int decodedType
            {
                get { return Convert.ToByte(Convert.ToChar(Type)); }
            }

            public string getParameter()
            {
                string[] data = Regex.Split(Content, "³²");
                int len = data.Length;
                int cur_pointer = Pointer;
                Pointer++;
                if (cur_pointer < len)
                {
                    return data[cur_pointer];
                }
                else
                {
                    return null;
                }
            }

            public string dataToString() {  return data; }
    }
}*/
