using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoomBang_RetroServer.Database;
using BoomBang_RetroServer.Utils;
using System.Data;

namespace BoomBang_RetroServer.Game.Chests
{
    public static class ChestManager
    {
        public static Dictionary<int, ChestData> Chests = new Dictionary<int, ChestData>();
        public static List<int> ChestsIDS = new List<int>();
        private static int asas = 0;
        private static int LastID;
        public static void Initialize()
        {
            int[] ChestsID = { };
            using(DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DataTable Table = DatabaseClient.ExecuteScalarSet("SELECT * FROM boombang_chests WHERE activated = 1").Tables[0];
                foreach(DataRow Row in Table.Rows)
                {
                    ChestData Chest = new ChestData();
                    try
                    {
                        Chest.ID1 = Convert.ToInt32(Row["id1"]);
                        Chest.ID = Convert.ToInt32(Row["id"]);
                        Chest.Appear = Convert.ToInt32(Row["Appear"]);
                        Chest.Speed = Convert.ToInt32(Row["Speed"]);
                        Chest.Amount = Convert.ToInt32(Row["Amount"]);
                        Chest.Amount_type = Convert.ToString(Row["Amount_type"]);
                        Chest.ItemID = Convert.ToInt32(Row["ItemID"]);
                        Chest.ContestID = Convert.ToInt32(Row["ContestID"]);
                        Chest.Message = Convert.ToString(Row["Message"]);
                        Chest.Time = Convert.ToInt32(Row["Time"]);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Chest = null;
                    }
                    if (Chest != null)
                    {
                        Chests.Add(Chest.ID1, Chest);
                        ChestsIDS.Add(Chest.ID1);
                    }
                }
            }
            Output.WriteLine(Chests.Count + " loades chests.", OutputLevel.Notification);
        }
    }
}
