using System;
using System.Collections.Generic;
using System.Threading;

namespace Boombang.game.session
{
    public class sessionManager
    {
        private Dictionary<long, sessionHandler> mSessions;
        private Thread mIdleTimer;

        public sessionManager()
        {
            mSessions = new Dictionary<long, sessionHandler>();
            mIdleTimer = new Thread(IdleTimer);
            mIdleTimer.Start();
        }

        public void StartSession(long sessionid)
        {
            mSessions.Add(sessionid, new sessionHandler(sessionid));
        }

        public void EndSession(long sessionid)
        {
            mSessions[sessionid].clean();
            mSessions.Remove(sessionid);
        }

        public sessionHandler GetSession(long sessionid)
        {
            if (mSessions.ContainsKey(sessionid))
            {
                return mSessions[sessionid];
            }
            else
            {
                return null;
            }
        }

        public void NewSessionData(long sessionid, string data)
        {
            mSessions[sessionid].ProcessData(data);
        }

        public void KillExistingSessions(int userId)
        {
            long session = GetSessionForUser(userId);

            if (session != -1)
            {
                Environment.connections.EndConnection(session);
            }
        }

        public long GetSessionForUser(int userId)
        {
            Dictionary<long, sessionHandler>.Enumerator myEnum = mSessions.GetEnumerator();

            while(myEnum.MoveNext())
            {
                sessionHandler session = myEnum.Current.Value;
                if (session.mUser != null)
                {
                    if (session.mUser.id == userId)
                    {
                        return session.mSessionID;
                    }
                }
            }

            return -1;
        }

        public long GetSessionForUser(string userName)
        {
            Dictionary<long, sessionHandler>.Enumerator myEnum = mSessions.GetEnumerator();

            while (myEnum.MoveNext())
            {
                sessionHandler session = myEnum.Current.Value;
                if (session.mUser != null)
                {
                    if (session.mUser.usuario.ToLower() == userName)
                    {
                        return session.mSessionID;
                    }
                }
            }

            return -1;
        }

        public List<sessionHandler> GetSessionList()
        {
            Dictionary<long, sessionHandler>.Enumerator myEnum = mSessions.GetEnumerator();
            List<sessionHandler> result = new List<sessionHandler>();

            while (myEnum.MoveNext())
            {
                result.Add(myEnum.Current.Value);
            }

            return result;
        }

        public void IdleTimer()
        {
            while (true)
            {
                List<long> toRemove = new List<long>();

                foreach (long sessid in toRemove)
                {
                    Environment.connections.EndConnection(sessid);
                }
                
                Thread.Sleep(60000);
            }
        }
    }
}
