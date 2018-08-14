using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace Boombang.sockets
{
    public class manager
    {
        private listener mListener;
        private Dictionary<long, connection> mConnectionList;
        int MaxConnections;

        public manager()
        {
            mConnectionList = new Dictionary<long, connection>();
            mListener = new listener(new listener.NewConnectionDelegate(NewConnection));
            MaxConnections = 2500;
        }

        public bool CheckIP(string ip)
        {
            int count = 0;
            foreach (connection conn in mConnectionList.Values)
            {
                if (conn.GetIP() == ip)
                {
                    count++;
                }
            }

            if (count > MaxConnections) return false;
            return true;
        }

        public void NewConnection(long clientid, Socket clientsocket)
        {
            Environment.sessions.AddSession(clientid);

            connection tmpConnection = new connection(clientid, clientsocket, 
                new connection.EndConnectionDelegate(EndConnection), 
                new connection.NewDataDelegate(NewData));
            
            mConnectionList.Add(clientid, tmpConnection);

            Environment.sessions.GetSession(clientid).ConnectionReady();
        }

        public void EndConnection(long clientid)
        {
            if (mConnectionList.ContainsKey(clientid))
            {
                Environment.sessions.EndSession(clientid);

                Console.WriteLine("[SCKMGR] Conexión " + clientid.ToString() + " cerrada.");

                mConnectionList[clientid].Kill();
                mConnectionList.Remove(clientid);
            }
        }

        public void NewData(long clientid, string data)
        {
            Environment.sessions.NewSessionData(clientid, data);
        }

        public connection GetInstance(long clientid)
        {
            if (mConnectionList.ContainsKey(clientid))
            {
                return mConnectionList[clientid];
            }
            else
            {
                return null;
            }
        }
    }
}
