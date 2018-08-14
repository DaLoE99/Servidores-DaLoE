namespace BoomBang.Game.Misc
{
    using BoomBang;
    using BoomBang.Game.FlowerPower;
    using BoomBang.Storage;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class NewsCacheManager
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
                        list_0.Add(new Notice((uint)row["id"], (double)row["fecha"], (string)row["titulo"], (string)row["contenido"], (string)row["imagen"]));
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
                    list_0.Add(new Notice((uint)row["id"], (double)row["fecha"], (string)row["titulo"], (string)row["contenido"], (string)row["imagen"]));
                    uint_0++;
                }
            }
            Output.WriteLine("Loaded " + uint_0 + " news in to news cache.", OutputLevel.DebugInformation);
        }

    }
}