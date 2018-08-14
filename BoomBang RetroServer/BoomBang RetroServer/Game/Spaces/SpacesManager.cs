using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoomBang_RetroServer.Game.Spaces.Areas;
using BoomBang_RetroServer.Game.Spaces.Salas;
using BoomBang_RetroServer.Game.Spaces.Islas;
using BoomBang_RetroServer.Database;
using System.Data;

namespace BoomBang_RetroServer.Game.Spaces
{
    public static class SpacesManager
    {
        public static Dictionary<int, AreaGroup> Areas = new Dictionary<int, AreaGroup>();
        public static Dictionary<int, AreaInstance> AreasIDS = new Dictionary<int, AreaInstance>();
        public static Dictionary<int, AreaGroup> Games = new Dictionary<int, AreaGroup>();

        public static Dictionary<int, IslaGroup> Islas = new Dictionary<int, IslaGroup>();
        public static Dictionary<int, IslaInstance> IslasIDS = new Dictionary<int, IslaInstance>();
        public static Dictionary<int, IslaGroup> Gamese = new Dictionary<int, IslaGroup>();

        public static Dictionary<int, SalaGroup> Salas = new Dictionary<int, SalaGroup>();
        public static Dictionary<int, SalaInstance> SalasIDS = new Dictionary<int, SalaInstance>();
        public static Dictionary<int, SalaGroup> Gamesed = new Dictionary<int, SalaGroup>();
        private static int LastID;
        public static void ReloadIslands(int ID)
        {
            bool flag;
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@id", id);
                DataTable Table = DatabaseClient.ExecuteScalarSet("SELECT * FROM boombang_islas WHERE id = @id").Tables[0];
                foreach (DataRow Row in Table.Rows)
                {
                    IslaData Islande = new IslaData();
                    try
                    {
                        Islande.IDe = Convert.ToInt32(Row["id"]);
                        //Islande.MaxVisitors = Convert.ToInt32(Row["MaxVisitors"]);
                        //Islande.X = Convert.ToInt32(Row["X"]);
                        //Islande.Y = Convert.ToInt32(Row["Y"]);
                        Islande.Name = Convert.ToString(Row["nombre"]);
                        Islande.Creator = Convert.ToInt32(Row["id_user"]);
                        Islande.Descripcion = Convert.ToString(Row["descripcion"]);
                        Islande.Type = Convert.ToInt32(Row["tipo"]);
                        Islande.Uppercut = (Convert.ToInt32(Row["punch"]) == 0) ? 0 : 1;
                    }
                    catch (Exception Exception)
                    {
                        Console.WriteLine(Exception.ToString());
                        Islande = null;
                    }
                    if (Islande != null)
                    {
                        int num1 = LastID;
                        flag = 1 == 0;
                        if (Islande.IDe > LastID)
                        {
                            LastID = Islande.ID;
                        }
                        Islas.Add(Islande.IDe, new IslaGroup(Islande));
                    }
                }
            }
            Output.WriteLine(Islas.Count + " loaded Islands.", OutputLevel.Notification);
        }
        public static void ReloadSalas(int id)
        {
            bool flag;
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@id", id);
                DataTable Table = DatabaseClient.ExecuteScalarSet("SELECT * FROM boombang_zonas WHERE id = @id").Tables[0];
                foreach (DataRow Row in Table.Rows)
                {
                    SalaData Islande = new SalaData();
                    try
                    {
                        Islande.IDe = Convert.ToInt32(Row["id"]);
                        Islande.id_isla = Convert.ToInt32(Row["id_isla"]);
                        Islande.Type = Convert.ToInt32(Row["zona"]);
                        DatabaseClient.SetParameter("@type", Convert.ToInt32(Row["zona"]));
                        DataTable tTable = DatabaseClient.ExecuteScalarSet("SELECT * FROM boombang_zonas_modelos WHERE id_eprivado = @type").Tables[0];
                        foreach(DataRow tRow in tTable.Rows)
                        {
                            Islande.MaxVisitors = Convert.ToInt32(tRow["MaxVisitors"]);
                            Islande.X = Convert.ToInt32(tRow["X"]);
                            Islande.Y = Convert.ToInt32(tRow["Y"]);
                            Islande.Map = new Map(tRow["Map"].ToString());
                        }
                        Islande.Name = Convert.ToString(Row["name"]);
                        Islande.Pass = Convert.ToString(Row["pass"]);
                        Islande.Colors = Convert.ToString(Row["colores"]);
                        Islande.ColorsRGB = Convert.ToString(Row["RGB"]);
                    }
                    catch (Exception Exception)
                    {
                        Console.WriteLine(Exception.ToString());
                        Islande = null;
                    }
                    if (Islande != null)
                    {
                        int num1 = LastID;
                        flag = 1 == 0;
                        if (Islande.IDe > LastID)
                        {

                        }
                    }
                }
            }
        }
    }
}