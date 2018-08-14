using System;
using System.Data;

using MySql.Data.MySqlClient;

namespace Boombang.database
{
    public class DatabaseClient : IDisposable
    {
        private uint mHandle;
        private DateTime mLastActivity;

        private DatabaseManager mManager;
        private MySqlConnection mConnection;
        private MySqlCommand mCommand;

        public uint Handle
        {
            get { return mHandle; }
        }

        public bool Anonymous
        {
            get { return (mHandle == 0); }
        }

        public DateTime LastActivity
        {
            get { return mLastActivity; }
        }

        public int Inactivity
        {
            get { return (int)(DateTime.Now - mLastActivity).TotalSeconds; }
        }

        public ConnectionState State
        {
            get { return (mConnection != null) ? mConnection.State : ConnectionState.Broken; }
        }

        public DatabaseClient(uint Handle, DatabaseManager pManager)
        {
            if (pManager == null)
                throw new ArgumentNullException("pManager");

            mHandle = Handle;
            mManager = pManager;

            mConnection = new MySqlConnection(mManager.CreateConnectionString());
            mCommand = mConnection.CreateCommand();

            UpdateLastActivity();
        }

        public void Connect()
        {
            if (mConnection == null)
                throw new DatabaseException("[SQLMGR] La instancia de conexión #" + mHandle + " no tiene ningún valor.");
            if (mConnection.State != ConnectionState.Closed)
                throw new DatabaseException("[SQLMGR] La instancia de conexión #" + mHandle + " requiere ser cerrada antes de volverla a abrir.");

            try
            {
                mConnection.Open();
            }
            catch (MySqlException)
            {
                throw new DatabaseException("\n[SQLMGR] Fallo al crear el cliente #" + mHandle + " para la base de datos.");
            }
        }

        public void Disconnect()
        {
            try
            {
                mConnection.Close();
            }
            catch { }
        }

        public void Destroy()
        {
            Disconnect();

            mConnection.Dispose();
            mConnection = null;

            mCommand.Dispose();
            mCommand = null;

            mManager = null;
        }

        public void UpdateLastActivity()
        {
            mLastActivity = DateTime.Now;
        }

        public DatabaseManager GetManager()
        {
            return mManager;
        }

        public void AddParamWithValue(string sParam, object val)
        {
            mCommand.Parameters.AddWithValue(sParam, val);
        }
        public void ExecuteQuery(string sQuery)
        {
            mCommand.CommandText = sQuery;
            mCommand.ExecuteScalar();
            mCommand.CommandText = null;
        }

        public DataSet ReadDataSet(string sQuery)
        {
            DataSet pDataSet = new DataSet();
            mCommand.CommandText = sQuery;

            using (MySqlDataAdapter pAdapter = new MySqlDataAdapter(mCommand))
            {
                pAdapter.Fill(pDataSet);
            }
            mCommand.CommandText = null;

            return pDataSet;
        }
        public DataTable ReadDataTable(string sQuery)
        {
            DataTable pDataTable = new DataTable();
            mCommand.CommandText = sQuery;

            using (MySqlDataAdapter pAdapter = new MySqlDataAdapter(mCommand))
            {
                pAdapter.Fill(pDataTable);
            }
            mCommand.CommandText = null;

            return pDataTable;
        }
        public DataRow ReadDataRow(string sQuery)
        {
            DataTable pDataTable = ReadDataTable(sQuery);
            if (pDataTable != null && pDataTable.Rows.Count > 0)
                return pDataTable.Rows[0];

            return null;
        }
        public String ReadString(string sQuery)
        {
            mCommand.CommandText = sQuery;
            String result = mCommand.ExecuteScalar().ToString();
            mCommand.CommandText = null;

            return result;
        }
        public Int32 ReadInt32(string sQuery)
        {
            mCommand.CommandText = sQuery;
            Int32 result = Convert.ToInt32(mCommand.ExecuteScalar());
            mCommand.CommandText = null;

            return result;
        }
        public UInt32 ReadUInteger(string sQuery)
        {
            mCommand.CommandText = sQuery;
            UInt32 result = Convert.ToUInt32(mCommand.ExecuteScalar());
            mCommand.CommandText = null;

            return result;
        }

        public void Dispose()
        {
            if (this.Anonymous == false)
            {
                
                mCommand.CommandText = null;
                mCommand.Parameters.Clear();

                mManager.ReleaseClient(mHandle);
            }
            else
            {
                Destroy();
            }
        }
    }
}
