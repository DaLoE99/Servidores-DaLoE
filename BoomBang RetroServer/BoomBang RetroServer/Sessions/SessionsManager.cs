using BoomBang_RetroServer.Configuration;
using BoomBang_RetroServer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Sessions
{
    class SessionsManager
    {
        public static void Uninitialize()
        {
            try
            {
                SessionID = 0;
                foreach (KeyValuePair<long, Session> Session in Sessions)
                {
                    Session.Value.End();
                }
                Sessions.Clear();
                Sessions = null;
            }
            catch { }
            Output.WriteLine("Sessions manager uninitialized.", OutputLevel.Information);
        }
        public static int GetNumOfSessions()
        {
            int n = 0;
            foreach(KeyValuePair<long, Session> SessionB in SessionsManager.Sessions)
            {
                if(SessionB.Value.User != null)
                {
                    n++;
                }
            }
            return n;
        }
        public static void AssSession(long ID)
        {
            if (CheckConnection(Client))
            {
                Sessions.Add(SessionID, new Session(SessionID, Client));
                SessionID++;
                using (DatabaseClient client = DatabaseManager.GetClient())
                {
                    client.ExecuteScalar("UPDATE boombang_statistics SET Clients=Clients+1 WHERE ID = 1");
                }
            }
            else
            {
                Client.Close();
            }
        }
        public static void EndSession(long ID)
        {
            if (CheckConnection(Client))
            {
                Sessions.Remove(ID);
                using (DatabaseClient client = DatabaseManager.GetClient())
                {
                    client.ExecuteScalar("UPDATE boombang_statistics SET Clients=Clients-1 WHERE ID = 1");
                }
            }
        }
        public static Session GetSession(string UserName)
        {
            foreach(Session Session in Sessions.Value)
            {
                if(Session.User != null)
                {

                }
            }
        }
        public static bool Online2(string ID)
        {
            foreach(Session Session in Sessions.Values)
            {
                if(Session.User != null)
                {
                    if(Session.User.UserName.ToLower() == ID.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private static bool CheckConnection(Socket Socket)
        {
            string IP = Socket.RemoteEndPoint.ToString().Split(':')[0];
            int LocalCount = 0;
            int GlobalCount = Sessions.Count;
            if(GlobalCount < (int(ConfigurationManager.GetValue("server.maxglobalconnections"))
            {
                foreach (Session Session in Sessions.Values)
                {
                    if(Session.IP == IP)
                    {
                        LocalCount++;
                    }
                    if(LocalCount >=(int)ConfigurationManager.GetValue("server.maxglobalconnections"))
                    {
                        return false;
                    }
                }
                if(LocalCount >= (int)ConfigurationManager.GetValue("server.maxglobalconnections"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
