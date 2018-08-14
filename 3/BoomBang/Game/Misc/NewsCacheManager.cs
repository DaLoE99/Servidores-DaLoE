using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Storage;
using System.Data;
using Snowlight.Game.Handlers;

namespace Snowlight.Game.Misc
{
    class NewsCacheManager
    {
        internal static List<Notice> list_0;
        /* private scope */
        static uint uint_0;

        public static void Initialize(SqlDatabaseClient MySqlClient)
        {
            list_0 = new List<Notice>();
            uint_0 = 0;
            smethod_0(MySqlClient);
        }

        public static void ReCacheNews()
        {
            list_0.Clear();
            uint_0 = 0;
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                DataTable table = client.ExecuteQueryTable("SELECT * FROM site_noticias ORDER BY fecha DESC LIMIT 40");
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        list_0.Add(new Notice(
                            uint.Parse(row["id"].ToString()),
                            double.Parse(row["fecha"].ToString()),
                            (string)row["titulo"],
                            (string)row["contenido"],
                            (string)row["imagen"]));
                        uint_0++;
                    }
                }
            }
            Output.WriteLine("Reloaded " + uint_0 + " news in to news cache.", OutputLevel.DebugInformation);
        }

        private static void smethod_0(SqlDatabaseClient sqlDatabaseClient_0)
        {
            DataTable table = sqlDatabaseClient_0.ExecuteQueryTable("SELECT * FROM site_noticias ORDER BY id DESC LIMIT 40");
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    list_0.Add(new Notice(
                        uint.Parse(row["id"].ToString()),
                        double.Parse(row["fecha"].ToString()),
                        (string)row["titulo"],
                        (string)row["contenido"],
                        (string)row["imagen"]));
                    uint_0++;
                }
            }
            Output.WriteLine("Loaded " + uint_0 + " news in to news cache.", OutputLevel.DebugInformation);
        }

        public static List<Notice> News
        {
            get
            {
                return list_0;
            }
        }
    }
}
