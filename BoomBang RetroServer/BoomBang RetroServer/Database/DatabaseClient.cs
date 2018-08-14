using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Database
{
    class DatabaseClient : IDisposable
    {
        MySqlCommand mCommand;
        public void ResetCommand()
        {
            mCommand.CommandText = null;
            ClearParameters();
        }
        public void SetParameter(string Key, object Value)
        {
            this.mCommand.Parameters.Add(new MySqlParameter(Key, Value));
        }

        public int ExecuteNonQuery(string CommandText)
        {
            int Affected = mCommand.ExecuteNonQuery();
            ResetCommand();
            return Affected;
        }
        public DataSet ExecuteScalarSet(string CommandText)
        {
            DataSet DataSet = new DataSet();
            mCommand.CommandText = CommandText;
            using(MySqlDataAdapter Adapter = new MySqlDataAdapter(mCommand))
            {
                Adapter.Fill(DataSet);
            }
            ResetCommand();
            return DataSet;
        }
        public DataTable ExecuteScalarTable(string CommandText)
        {
            DataSet DataSet = ExecuteScalarSet(CommandText);
            return DataSet.Tables.Count > 0 ? DataSet.Tables[0] : null;
        }
        public DataRow ExecuteScalarRow(string CommandText)
        {
            DataTable DataTable = ExecuteScalarTable(CommandText);
            return DataTable.Rows.Count > 0 ? DataTable.Rows[0] : null;
        }
        public object ExecuteScalar(string CommandText)
        {
            mCommand.CommandText = CommandText;
            object ReturnValue = mCommand.ExecuteScalar();
            ResetCommand();

            return ReturnValue;
        }
    }
}
