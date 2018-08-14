namespace BoomBang.Game.Achievements
{
    using BoomBang.Storage;
    using System;
    using System.Collections.Generic;

    public static class AchievementManager
    {
        /* private scope */
         /* private scope */

        public static void Initialize(SqlDatabaseClient MySqlClient)
        {
            smethod_0(MySqlClient);
        }

        private static void smethod_0(SqlDatabaseClient sqlDatabaseClient_0)
        {
            sqlDatabaseClient_0.ExecuteQueryTable("SELECT * FROM recompensas");
        }
    }
}

