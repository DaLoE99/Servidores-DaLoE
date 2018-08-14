using BoomBang_RetroServer.Sockets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Sessions
{
    class Invoker
    {
        private ClientMessage Message;
        private Session Session;

        public Invoker(ClientMessage Message, Session Session)
        {
            this.Message = Message;
            this.Session = Session;
            new Thread(new ThreadStart(Invoke)).Start();
        }
        private void Invoke()
        {
            List<HandlerCollection>.Enumerator Enumerator = Session.Handlers.GetEnumerator();
            while(Enumerator.MoveNext())
            {
                MethodInfo Method = Enumerator.Current.Type.GetMethod(this.Message.GetHandler());
                if(Method != null)
                {
                    Enumerator.Current.Handler.Message = this.Message;
                    Enumerator.Current.Handler.Session = this.Session;
                    Enumerator.Current.Handler.User = this.Session.User;
                    try
                    {
                        Method.Invoke(Enumerator.Current.Handler, null);
                    }
                    catch(Exception ex)
                    {
                        Output.WriteLine("Error: the client #" + this.Session.User.UserName.ToString() + " caused an error. Exception: " + ex.ToString(), OutputLevel.Warning);
                    }
                    return;
                }
            }
            Output.WriteLine("Handler '" + this.Message.GetHandler() + "' not found. " + Message.GetData() + " produced by: " + this.Session.User.UserName.ToString(), OutputLevel.Warning);
            if(this.Message.GetHandler() == "Handler_145" || this.Message.GetHandler() == "Handler_146" || this.Message.GetHandler() == "Handler_125_120" || this.Message.GetHandler() == "Handler_137_122")
            {
                Session.End();
            }
        }
    }
}
