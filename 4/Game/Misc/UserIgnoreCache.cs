using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Snowlight.Storage;
using System.Data;

namespace Snowlight.Game.Misc
{
    public class UserIgnoreCache
    {

        List<uint> list_0;
        object object_0;
        uint uint_0;

        public UserIgnoreCache(SqlDatabaseClient MySqlClient, uint UserId)
        {
            this.uint_0 = UserId;
            this.list_0 = new List<uint>();
            this.object_0 = new object();
            this.ReloadCache(MySqlClient);
        }

        public void Dispose()
        {
            lock (this.object_0)
            {
                if (this.list_0 != null)
                {
                    this.list_0.Clear();
                    this.list_0 = null;
                }
            }
        }

        public void MarkUserIgnored(uint UserId)
        {
            lock (this.object_0)
            {
                if (!this.list_0.Contains(UserId))
                {
                    this.list_0.Add(UserId);
                    using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                    {
                        client.SetParameter("user_id", this.uint_0);
                        client.SetParameter("ignore_id", UserId);
                        client.ExecuteNonQuery("INSERT INTO ignorados (id_usuario,id_ignorada) VALUES (@user_id,@ignore_id)");
                    }
                }
            }
        }

        public void MarkUserUnignored(uint UserId)
        {
            lock (this.object_0)
            {
                if (this.list_0.Contains(UserId))
                {
                    this.list_0.Remove(UserId);
                    using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                    {
                        client.SetParameter("user_id", this.uint_0);
                        client.SetParameter("ignore_id", UserId);
                        client.ExecuteNonQuery("DELETE FROM ignorados WHERE id_usuario = @user_id AND id_ignorada = @ignore_id LIMIT 1");
                    }
                }
            }
        }

        public void ReloadCache(SqlDatabaseClient MySqlClient)
        {
            lock (this.object_0)
            {
                this.list_0.Clear();
                MySqlClient.SetParameter("user_id", this.uint_0);
                foreach (DataRow row in MySqlClient.ExecuteQueryTable("SELECT id_ignorada FROM ignorados WHERE id_usuario = @user_id").Rows)
                {
                    list_0.Add((uint)row["id_ignorada"]);
                }
            }
        }

        public bool UserIsIgnored(uint UserId)
        {
            lock (this.object_0)
            {
                return this.list_0.Contains(UserId);
            }
        }

        public ReadOnlyCollection<uint> List
        {
            get
            {
                lock (this.object_0)
                {
                    List<uint> list = new List<uint>();
                    list.AddRange(this.list_0);
                    return list.AsReadOnly();
                }
            }
        }
    }
}
