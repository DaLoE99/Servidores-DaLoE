using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

using Boombang.sockets.messages;
using Boombang.game.handlers;
using Boombang.game.user;

namespace Boombang.game.session
{
    public class sessionHandler : Handler
    {
        private class HandlerCollection
        {
            public HandlerCollection(Type rType, Handler rInstance) { HandlerType = rType; HandlerInstance = rInstance; }
            public Type HandlerType;
            public Handler HandlerInstance;
        }

        //private SessionCommunicationStack mStack;
        private Dictionary<int, HandlerCollection> mHandlers;
        public string area_actual = string.Empty;
        public int id_areaActual = 0;

        public bool en_areapublica = true;

        private int mHandlerOffset = 0;

        private bool mDirtySession = false;

        protected DateTime mLastUpdate = DateTime.Now;

        public sessionHandler(long sessionid)
        {
            mSessionID = sessionid;
            //mStack = new SessionCommunicationStack();
            mHandlers = new Dictionary<int, HandlerCollection>();
            RegisterDefaultHandler();
        }

        public void UpdateLastTime()
        {
            mLastUpdate = DateTime.Now;
        }

        public DateTime LastTime
        {
            get
            {
                return mLastUpdate;
            }
        }

        public void MakeSessionDirty()
        {
            mDirtySession = true;
        }

        private void RegisterDefaultHandler()
        {
            mHandlers.Clear();
            HandlerCollection rCol = new HandlerCollection(typeof(login), new login());
            mHandlers.Add(0, rCol);
            rCol = new HandlerCollection(typeof(ping), new ping());
            mHandlers.Add(1, rCol);
            mHandlerOffset = 2;
        }

        public void SessionAuthenticated(userInfo userInfo)
        {
            mUser = userInfo;
            HandlerCollection rCol = new HandlerCollection(typeof(flowerpower), new flowerpower());
            mHandlers.Add(mHandlerOffset++, rCol);
            rCol = new HandlerCollection(typeof(bPad), new bPad());
            mHandlers.Add(mHandlerOffset++, rCol);
            rCol = new HandlerCollection(typeof(areas), new areas());
            mHandlers.Add(mHandlerOffset++, rCol);
            rCol = new HandlerCollection(typeof(catalogo), new catalogo());
            mHandlers.Add(mHandlerOffset++, rCol);
        }

        public void UpdateUserInfo(userInfo userInfo)
        {
            mUser = userInfo;
        }

        public int AddListener(Type listenerType, Handler listenerObject)
        {
            int listenerId = mHandlerOffset++;
            HandlerCollection rCol = new HandlerCollection(listenerType, listenerObject);
            mHandlers.Add(listenerId, rCol);
            return listenerId;
        }

        public void RemoveListener(int listenerId)
        {
            mHandlers.Remove(listenerId);
        }

        public void ConnectionReady()
        {
            GreetClient();
        }

        private void GreetClient()
        {
            if (!Environment.connections.CheckIP(Environment.connections.GetConnection(mSessionID).GetIP()))
            {
                mDirtySession = true;
                Environment.connections.EndConnection(mSessionID);
            }
        }

        public void ProcessData(string data)
        {
            if (data.Contains("<policy-file-request/>"))
            {
                string xmlPolicy =
                                   "<?xml version=\"1.0\"?>\r\n" +
                                   "<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n" +
                                   "<cross-domain-policy>\r\n" +
                                   "<allow-access-from domain=\"*\" to-ports=\"2001\" />\r\n" +
                                   "</cross-domain-policy>\x0";
                Environment.sessions.GetSession(mSessionID).sendPolicy(xmlPolicy, true);
            }
            else
            {
                string[] _data = data.Split(Convert.ToChar(176));
                int i = 0;
                while (i < _data.Length)
                {
                    if(!(string.IsNullOrEmpty(_data[i])))
                    NewMessage(_data[i]);

                    i++;
                }
            }
        }

        public void NewMessage(string data)
        {
            Message = new client(data);
            ProcessMessage();
        }

        private void ProcessMessage()
        {
            Console.WriteLine("[SCKMGR] -- [RCV][" + mSessionID.ToString() + "]: " +  Message.Tostring());
            if (!mDirtySession)
            {
                InvokeMethod("Handler" + Message.decoded_id + "_type_" + Message.decoded_type);
            }
        }

        public void InvokeMethod(string methodName)
        {
            StringBuilder sbListeners = new StringBuilder();
            Dictionary<int, HandlerCollection>.Enumerator myEnum = mHandlers.GetEnumerator();

            while (myEnum.MoveNext())
            {
                sbListeners.Append(myEnum.Current.Value.HandlerType.Name + ", ");

                MethodInfo methodInfo = myEnum.Current.Value.HandlerType.GetMethod(methodName);

                if (methodInfo != null)
                {
                    myEnum.Current.Value.HandlerInstance.Message = Message;
                    myEnum.Current.Value.HandlerInstance.mSessionID = mSessionID;
                    myEnum.Current.Value.HandlerInstance.mUser = mUser;

                    try
                    {
                        methodInfo.Invoke(myEnum.Current.Value.HandlerInstance, null);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[SCKERR] Error: El cliente " + mSessionID.ToString() + " ha causado un error. \r\nStack: " + e.ToString());
                    }

                    return;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No hay handler en la clase '" + methodName + "' para '" + Message.id + "' (" + Message.decoded_id + ").");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void clean() { if (!string.IsNullOrEmpty(area_actual)) Environment.Game.areas.get_scenarioInstance(id_areaActual, en_areapublica).quitar_usuario(mSessionID); }
    }
}
