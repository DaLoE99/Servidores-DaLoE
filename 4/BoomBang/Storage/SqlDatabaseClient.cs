namespace BoomBang.Storage
{
    using BoomBang;
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;

    public class SqlDatabaseClient : IDisposable
    {
        /* private scope */ bool bool_0;
        /* private scope */ double double_0;
        internal int int_0;
        /* private scope */ MySqlCommand mySqlCommand_0;
        /* private scope */ MySqlConnection mySqlConnection_0;

        public SqlDatabaseClient(int int_1, MySqlConnection Connection)
        {
            this.int_0 = int_1;
            this.mySqlConnection_0 = Connection;
            this.mySqlCommand_0 = new MySqlCommand();
            this.mySqlCommand_0.Connection = this.mySqlConnection_0;
            this.bool_0 = true;
            this.method_0();
        }

        public void ClearParameters()
        {
            this.mySqlCommand_0.Parameters.Clear();
        }

        public void Close()
        {
            this.mySqlConnection_0.Close();
            this.mySqlCommand_0.Dispose();
            this.mySqlConnection_0 = null;
            this.mySqlCommand_0 = null;
        }

        public void Dispose()
        {
            this.bool_0 = true;
            this.method_0();
            Output.WriteLine("(Sql) Released client " + this.int_0 + " for availability.", OutputLevel.DebugInformation);
        }

        public int ExecuteNonQuery(string CommandText)
        {
            int num2;
            try
            {
                this.mySqlCommand_0.CommandText = CommandText;
                int num = this.mySqlCommand_0.ExecuteNonQuery();
                this.ResetCommand();
                num2 = num;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception);
                Output.WriteLine("(Sql) An exception has been throw while a query executon.\nDisposing MySql client #" + this.int_0 + "  and retrying the NonQueryExecution.", OutputLevel.Notification);
                SqlDatabaseManager.dictionary_0.Remove(this.int_0);
                this.Close();
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    num2 = client.ExecuteNonQuery(CommandText);
                }
            }
            return num2;
        }

        public DataRow ExecuteQueryRow(string CommandText)
        {
            DataRow row;
            try
            {
                DataTable table = this.ExecuteQueryTable(CommandText);
                row = (table.Rows.Count > 0) ? table.Rows[0] : null;
            }
            catch (Exception)
            {
                Output.WriteLine("(Sql) An exception has been throw while a query executon.\nDisposing MySql client #" + this.int_0 + "  and retrying the QueryRowExecution.", OutputLevel.Notification);
                SqlDatabaseManager.dictionary_0.Remove(this.int_0);
                this.Close();
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    row = client.ExecuteQueryRow(CommandText);
                }
            }
            return row;
        }

        public DataSet ExecuteQuerySet(string CommandText)
        {
            DataSet set2;
            try
            {
                DataSet dataSet = new DataSet();
                this.mySqlCommand_0.CommandText = CommandText;
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(this.mySqlCommand_0))
                {
                    adapter.Fill(dataSet);
                }
                this.ResetCommand();
                set2 = dataSet;
            }
            catch (Exception)
            {
                Output.WriteLine("(Sql) An exception has been throw while a query executon.\nDisposing MySql client #" + this.int_0 + "  and retrying the QuerySetExecution.", OutputLevel.Notification);
                SqlDatabaseManager.dictionary_0.Remove(this.int_0);
                this.Close();
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    set2 = client.ExecuteQuerySet(CommandText);
                }
            }
            return set2;
        }

        public DataTable ExecuteQueryTable(string CommandText)
        {
            DataTable table;
            try
            {
                DataSet set = this.ExecuteQuerySet(CommandText);
                table = (set.Tables.Count > 0) ? set.Tables[0] : null;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception);
                Output.WriteLine("(Sql) An exception has been throw while a query executon.\nDisposing MySql client #" + this.int_0 + "  and retrying the QueryTableExecution.", OutputLevel.Notification);
                SqlDatabaseManager.dictionary_0.Remove(this.int_0);
                this.Close();
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    table = client.ExecuteQueryTable(CommandText);
                }
            }
            return table;
        }

        public object ExecuteScalar(string CommandText)
        {
            object obj3;
            try
            {
                this.mySqlCommand_0.CommandText = CommandText;
                object obj2 = this.mySqlCommand_0.ExecuteScalar();
                this.ResetCommand();
                obj3 = obj2;
            }
            catch (Exception)
            {
                Output.WriteLine("(Sql) An exception has been throw while a query executon.\nDisposing MySql client #" + this.int_0 + "  and retrying the QueryScalarExecution.", OutputLevel.Notification);
                SqlDatabaseManager.dictionary_0.Remove(this.int_0);
                this.Close();
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    obj3 = client.ExecuteScalar(CommandText);
                }
            }
            return obj3;
        }

        private void method_0()
        {
            this.double_0 = UnixTimestamp.GetCurrent();
        }

        public void ResetCommand()
        {
            this.mySqlCommand_0.CommandText = null;
            this.ClearParameters();
        }

        public void SetParameter(string Key, object Value)
        {
            this.mySqlCommand_0.Parameters.Add(new MySqlParameter(Key, Value));
        }

        public bool Available
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public int Int32_0
        {
            get
            {
                return this.int_0;
            }
        }

        public double TimeInactive
        {
            get
            {
                return (UnixTimestamp.GetCurrent() - this.double_0);
            }
        }
    }
}

