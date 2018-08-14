using System;
using System.Text;

namespace Boombang.sockets.messages
{
    public class server
    {
        private StringBuilder content_toparse;
        public readonly string id;

        public server(string message_id)
        {
            this.content_toparse = new StringBuilder(message_id);
            id = message_id;
        }

        public void Append(string s) { this.content_toparse.Append(s); }
        public void Append(int i) { this.content_toparse.Append(i); }
        public void appendChar(int x) { this.content_toparse.Append(Convert.ToChar(x)); }
        
        public override string ToString() { return content_toparse.ToString(); }
    }
}
