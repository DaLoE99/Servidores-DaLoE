using BoomBang_RetroServer.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Database
{
    class DatabaseManager//MIRAR VIDEO DEL 4 DE ABRIL
    {
        internal static Dictionary<int, DatabaseClient> mClients;
        static int mMinPoolSize;
        static int mMaxPoolSize;
        static int mPoolLifetime;
        static object mSyncRoot;

        public static int ClientCount
        {
            get
            {
                return mClients.Count;
            }
        }
        public static DatabaseClient GetClient()
        {
            return GetClient();
        }
        public static void Initialize()
        {
            mClients = new Dictionary<int, DatabaseClient>();
            mMinPoolSize = (int)ConfigurationManager.GetValue("mysql.pool.min");
            mMaxPoolSize = (int)ConfigurationManager.GetValue("mysql.pool.max");
            mPoolLifetime = (int)ConfigurationManager.GetValue("mysql.pool.lifetime");
            mSyncRoot = new object();
            mPoolWait = new ManualResetEvent(true);
            mMonitosThread = new Timer(new TimerCallback(ProcessMonitorThread));
            if (mMinPoolSize < 0)
            {
                throw new ArgumentException("(SQL) Invalid database pool");
            }
            SetClientAmount(mMinPoolSize, "server init");
            Output.WriteLine("Database manager initialized.", OutputLevel.Warning);
        }
        public static void Uninitialize()
        {
            int Attempts = 0;
            while (mClients.Count > 0)
            {
                lock (mSyncRoot)
                {
                    List<int> Removable = new List<int>();
                    foreach (DatabaseClient Client in mClients.Values)
                    {
                        if (!Client.Available && Attempts <= 15)
                        {
                            continue;
                        }
                        Removable.Add(Client.Id);
                    }
                    foreach (int RemoveID in Removable)
                    {
                        mClients[RemoveID].Close();
                        mClients.Remove(RemoveID);
                    }
                }
                if (mClients.Count > 0)
                {
                    Thread.Sleep(100);
                }
            }
            Output.WriteLine("Database manayer uninitialized.", OutputLevel.Warning);
        }
        public static void ProcessMonitorThread(object state)
        {
            if (ClientCount > mMinPoolSize)
            {
                lock (mSyncRoot)
                {
                    List<int> ToDisconnect = new List<int>();
                    foreach (DatabaseClient Client in mClients.Values)
                    {
                        if (Client.Available && Client.TimeInactive >= mPoolLifetime)
                        {
                            ToDisconnect.Add(Client.Id);
                        }
                    }
                    foreach (int DisconnectID in ToDisconnect)
                    {
                        mClients[DisconnectID].Close();
                        mClients.Remove(DisconnectID);
                    }
                }
            }
        }
        public static void SetClientAmount(int ClientAmount, string LogReason = "Unknown")
        {
            int Diff = 0;
            lock (mSyncRoot)
            {
                int ToDestroy = -Diff;
                int Destroyed = 0;
                foreach (DatabaseClient Client in mClients.Values)
                {
                    //falta
                    if (!Client.Available)
                    {
                        continue;
                    }
                    if (Destroyed >= ToDestroy || ClientCount <= mMinPoolSize)
                    {
                        break;
                    }
                    Client.Close();
                    mClients.Remove(Client.Id);
                    Destroyed++;
                }
            }
        }

        public static DatabaseClient GetClient()
        {
            lock(mSyncRoot)
            {
                foreach(DatabaseClient Client in mClients.Values)
                {
                    if(!Client.Available)
                    {
                        continue;
                    }
                    Client.Available = false;
                    return Client;
                }
                if (mMaxPoolSize <= 0 || ClientCount < mMaxPoolSize)
                {
                    SetClientAmount(ClientCount + 1, "out of assignable clients in GetClient()");
                    return GetClient();
                }
                mStarvationCounter++;
                Monitor.Wait(mSyncRoot);
                return GetClient();
            }
        }
        public static void PokeAllAwaiting()
        {
            lock (mSyncRoot)
            {
                Monitor.PulseAll(mSyncRoot);
            }
        }
        public static void GenerateClientId()
        {
            lock (mSyncRoot)
            {
                return mClientIdGenerator++;
            }
        }
        private static DatabaseClient CreateClient(int Id)
        {
            MySqlConnection Connection = new MySqlConnection(GenerateConnectionString());
            Connection.Open();
            return new DatabaseClient(Id, Connection);
        }
        public static string GenerateConnectionString()
        {
            MySqlConnectionStringBuilder ConnectionStringBuilder = new MySqlConnectionStringBuilder();
            ConnectionStringBuilder.Server = (string)ConfigurationManager.GetValue("mysql.host");
        }
    }
}