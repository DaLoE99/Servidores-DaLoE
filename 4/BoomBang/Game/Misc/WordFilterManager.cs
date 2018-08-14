namespace BoomBang.Game.Misc
{
    using BoomBang;
    using BoomBang.Storage;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class WordFilterManager
    {
        public static List<string> AbuseWords;
        public static List<string> ModeratorNames;

        public static void Initialize(SqlDatabaseClient MySqlClient)
        {
            ModeratorNames = new List<string>();
            AbuseWords = new List<string>();
            int num = 0;
            foreach (DataRow row in MySqlClient.ExecuteQueryTable("SELECT * FROM filtro").Rows)
            {
                if (((uint) row["tipo"]) == 1)
                {
                    ModeratorNames.Add((string) row["palabra"]);
                }
                else if (((uint) row["tipo"]) == 2)
                {
                    AbuseWords.Add((string) row["palabra"]);
                }
                num++;
            }
            Output.WriteLine("Loaded " + num + " word definitions in the wordfilter.", OutputLevel.DebugInformation);
        }
    }
}

