namespace BoomBang.Game.Sessions
{
    using BoomBang;
    using BoomBang.Communication;
    using BoomBang.Game.Moderation;
    using BoomBang.Storage;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Threading;

    public static class SessionManager
    {
        /* private scope */
        static ConcurrentDictionary<uint, Session> concurrentDictionary_0;
        /* private scope */
        static List<uint> list_0;
        /* private scope */
        static object object_0;
        public static bool RejectIncomingConnections;
        /* private scope */
        static Thread thread_0;
        /* private scope */
        static uint uint_0;

        public static void BroadcastPacket(ServerMessage Message)
        {
            BroadcastPacket(Message.GetBytes());
        }

        public static void BroadcastPacket(byte[] Data)
        {
            foreach (Session session in concurrentDictionary_0.Values)
            {
                if (((session != null) && !session.Stopped) && session.Authenticated)
                {
                    session.SendData(Data, false);
                }
            }
        }

        public static void BroadcastPacket(ServerMessage Message, string RequiredRight)
        {
            BroadcastPacket(Message.GetBytes());
        }

        public static bool ContainsCharacterId(uint Uid)
        {
            foreach (Session session in concurrentDictionary_0.Values)
            {
                if (!session.Stopped && (session.CharacterId == Uid))
                {
                    return true;
                }
            }
            return false;
        }

        public static Session GetSessionByCharacterId(uint uint_1)
        {
            foreach (Session session in concurrentDictionary_0.Values)
            {
                if (!session.Stopped && (session.CharacterId == uint_1))
                {
                    return session;
                }
            }
            return null;
        }

        public static void HandleIncomingConnection(Socket IncomingSocket)
        {
            bool flag;
            Output.WriteLine(((flag = ModerationBanManager.IsRemoteAddressBlacklisted(IncomingSocket.RemoteEndPoint.ToString().Split(new char[] { ':' })[0])) ? "Rejected" : "Accepted") + " incoming connection from " + IncomingSocket.RemoteEndPoint.ToString() + ".", OutputLevel.Informational);
            if (!flag && !RejectIncomingConnections)
            {
                lock (object_0)
                {
                    uint key = uint_0++;
                    concurrentDictionary_0.TryAdd(key, new Session(key, IncomingSocket));
                }
            }
            else
            {
                try
                {
                    IncomingSocket.Close();
                }
                catch (Exception)
                {
                }
            }
        }

        public static void Initialize()
        {
            RejectIncomingConnections = false;
            concurrentDictionary_0 = new ConcurrentDictionary<uint, Session>();
            list_0 = new List<uint>();
            uint_0 = 0;
            thread_0 = new Thread(new ThreadStart(SessionManager.smethod_0));
            thread_0.Priority = ThreadPriority.BelowNormal;
            thread_0.Name = "GameClientMonitor";
            thread_0.Start();
            object_0 = new object();
        }

        private static void smethod_0()
        {
            try
            {
                while (Program.Alive)
                {
                    List<Session> list = new List<Session>();
                    List<Session> list2 = new List<Session>();
                    lock (list_0)
                    {
                        foreach (uint num in list_0)
                        {
                            if (concurrentDictionary_0.ContainsKey(num))
                            {
                                list2.Add(concurrentDictionary_0[num]);
                            }
                        }
                        list_0.Clear();
                    }
                    foreach (Session session in concurrentDictionary_0.Values)
                    {
                        if ((!list2.Contains(session) && session.Stopped) && (session.TimeStopped > 15.0))
                        {
                            list.Add(session);
                        }
                    }
                    if (list2.Count > 0)
                    {
                        using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                        {
                            foreach (Session session2 in list2)
                            {
                                session2.Stop(client);
                            }
                        }
                    }
                    foreach (Session session3 in list)
                    {
                        session3.Dispose();
                        if (concurrentDictionary_0.ContainsKey(session3.UInt32_0))
                        {
                            bool flag = false;
                            Session session4 = null;
                            while (!flag)
                            {
                                concurrentDictionary_0.TryRemove(session3.UInt32_0, out session4);
                                if (session4 != null)
                                {
                                    flag = true;
                                }
                            }
                        }
                    }
                    Thread.Sleep(100);
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
            catch (Exception exception)
            {
                Output.WriteLine("Error thrown by SessionManager.Monitor()\r\nStack trace: " + exception.ToString(), OutputLevel.CriticalError);
            }
        }

        public static void StopSession(uint SessionId)
        {
            lock (list_0)
            {
                list_0.Add(SessionId);
            }
        }

        public static int ActiveConnections
        {
            get
            {
                return concurrentDictionary_0.Count;
            }
        }

        public static ConcurrentDictionary<uint, string> ConnectedUserData
        {
            get
            {
                ConcurrentDictionary<uint, string> dictionary = new ConcurrentDictionary<uint, string>();
                foreach (Session session in concurrentDictionary_0.Values)
                {
                    if (session.Authenticated)
                    {
                        dictionary.TryAdd(session.CharacterId, session.CharacterInfo.Username);
                    }
                }
                return dictionary;
            }
        }

        public static ConcurrentDictionary<uint, Session> Sessions
        {
            get
            {
                Dictionary<uint, Session> collection = new Dictionary<uint, Session>();
                foreach (KeyValuePair<uint, Session> pair in concurrentDictionary_0)
                {
                    if (!pair.Value.Stopped)
                    {
                        collection.Add(pair.Key, pair.Value);
                    }
                }
                return new ConcurrentDictionary<uint, Session>(collection);
            }
        }
    }
}

