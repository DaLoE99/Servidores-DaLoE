using System;
using System.Data;
using System.Threading;

using MySql.Data.MySqlClient;

namespace Boombang.database
{
    public class DatabaseManager
    {
        private DatabaseServer mServer;
        private Database mDatabase;

        private DatabaseClient[] mClients = new DatabaseClient[0];
        private bool[] mClientAvailable = new bool[0];
        private int mClientStarvationCounter;

        private Thread mClientMonitor;

        public DatabaseManager(DatabaseServer pServer, Database pDatabase)
        {
            mServer = pServer;
            mDatabase = pDatabase;
        }

        public DatabaseManager(string sServer, uint Port, string sUser, string sPassword, string sDatabase, uint minPoolSize, uint maxPoolSize)
        {
            mServer = new DatabaseServer(sServer, Port, sUser, sPassword);
            mDatabase = new Database(sDatabase, minPoolSize, maxPoolSize);

            mClientMonitor = new Thread(MonitorClientsLoop);
            mClientMonitor.Priority = ThreadPriority.Lowest;

            mClientMonitor.Start();
        }

        public void StartMonitor()
        {
            mClientMonitor = new Thread(MonitorClientsLoop);
            mClientMonitor.Priority = ThreadPriority.Lowest;

            mClientMonitor.Start();
        }

        public void StopMonitor()
        {
            if (mClientMonitor != null)
            {
                mClientMonitor.Abort();
            }
        }

        public void DestroyClients()
        {
            lock (this)
            {
                for (int i = 0; i < mClients.Length; i++)
                {
                    mClients[i].Destroy();
                    mClients[i] = null;
                }
            }
        }

        public void DestroyManager()
        {
            mServer = null;
            mDatabase = null;
            mClients = null;
            mClientAvailable = null;

            mClientMonitor = null;
        }

        private void MonitorClientsLoop()
        {
            while (true)
            {
                try
                {
                        DateTime dtNow = DateTime.Now;
                        for (int i = 0; i < mClients.Length; i++)
                        {
                            if (mClients[i].State != ConnectionState.Closed && mClients[i].State != ConnectionState.Fetching && mClients[i].State != ConnectionState.Executing && mClients[i].State != ConnectionState.Broken)
                            {
                                if (mClients[i].Inactivity >= 0.1)
                                {
                                    mClients[i].Destroy();

                                    Console.WriteLine("[SQLMGR] Desconectado cliente #" + mClients[i].Handle+ " de la base de datos.");
                                }
                            }
                        }

                    Thread.Sleep(1);
                }
                catch (ThreadAbortException) { }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public string CreateConnectionString()
        {
            MySqlConnectionStringBuilder pCSB = new MySqlConnectionStringBuilder();


            pCSB.Server = mServer.Host;
            pCSB.Port = mServer.Port;
            pCSB.UserID = mServer.User;
            pCSB.Password = mServer.Password;

            pCSB.Database = mDatabase.Name;
            pCSB.MinimumPoolSize = mDatabase.minPoolSize;
            pCSB.MaximumPoolSize = mDatabase.maxPoolSize;

            return pCSB.ToString();
        }

        public DatabaseClient GetClient()
        {
           
            lock (this)
            {
                for (uint i = 0; i < mClients.Length; i++)
                {
                    
                    if (mClientAvailable[i] == true)
                    {
                        
                        mClientStarvationCounter = 0;

                        if (mClients[i].State == ConnectionState.Broken)
                        {
                            mClients[i] = new DatabaseClient((i + 1), this);
                        }

                       
                        if (mClients[i].State == ConnectionState.Closed)
                        {
                            
                            mClients[i].Connect();

                            Console.WriteLine("[SQLMGR] Abriendo nueva conexión a la base de datos, cliente #" + mClients[i].Handle);
                        }


                        if (mClients[i].State == ConnectionState.Open)
                        {
                            Console.WriteLine("[SQLMGR] Cliente para la base de datos #" + mClients[i].Handle + " preparado.");

                            mClientAvailable[i] = false; 

                            mClients[i].UpdateLastActivity();
                            return mClients[i];
                        }
                    }
                }

                mClientStarvationCounter++;

                if (mClientStarvationCounter >= ((mClients.Length + 1) / 2))
                {
                    mClientStarvationCounter = 0;

                    SetClientAmount((uint)(mClients.Length + 1 * 1.3f));

                    return GetClient();
                }

                DatabaseClient pAnonymous = new DatabaseClient(0, this);
                pAnonymous.Connect();

                Console.WriteLine("[SQLMGR] Desconectado un cliente anónimo.");

                return pAnonymous;
            }
        }
        public void ReleaseClient(uint Handle)
        {
            if (mClients.Length >= (Handle - 1))
            {
                mClientAvailable[Handle - 1] = true;
                Console.WriteLine("[SQLMGR] Liberado cliente #" + Handle);
            }
        }

        public void SetClientAmount(uint Amount)
        {
            lock (this)
            {
                if (mClients.Length == Amount)
                    return;

                if (Amount < mClients.Length)
                {
                    for (uint i = Amount; i < mClients.Length; i++)
                    {
                        mClients[i].Destroy();
                        mClients[i] = null;
                    }
                }

                DatabaseClient[] pClients = new DatabaseClient[Amount];
                bool[] pClientAvailable = new bool[Amount];
                for (uint i = 0; i < Amount; i++)
                {
                    if (i < mClients.Length)
                    {
                        pClients[i] = mClients[i];
                        pClientAvailable[i] = mClientAvailable[i];
                    }
                    else
                    {
                        pClients[i] = new DatabaseClient((i + 1), this);
                        pClientAvailable[i] = true;
                    }
                }

                
                mClients = pClients;
                mClientAvailable = pClientAvailable;
            }
        }

        public bool INSERT(IDataObject obj)
        {
            using (DatabaseClient dbClient = GetClient())
            {
                return obj.INSERT(dbClient);
            }
        }

        public bool DELETE(IDataObject obj)
        {
            using (DatabaseClient dbClient = GetClient())
            {
                return obj.DELETE(dbClient);
            }
        }

        public bool UPDATE(IDataObject obj)
        {
            using (DatabaseClient dbClient = GetClient())
            {
                return obj.UPDATE(dbClient);
            }
        }

        public override string ToString()
        {
            return mServer.ToString() + ":" + mDatabase.Name;
        }
    }
}
