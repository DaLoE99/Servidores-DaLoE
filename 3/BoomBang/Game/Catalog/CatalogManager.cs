using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Communication;
using Snowlight.Storage;
using Snowlight.Game.Sessions;
using Snowlight.Communication.Incoming;
using Snowlight.Config;
using Snowlight.Communication.ResponseCache;
using Snowlight.Game.Items;
using System.Data;

namespace Snowlight.Game.Catalog
{
    public static class CatalogManager
    {
        public static Dictionary<int, int> objetos = new Dictionary<int, int>();
        public static int obj = 0;
        public static string packetcata;

        public static void Initialize(SqlDatabaseClient MySqlClient)
        {
            cargar_objetos();
            DataRouter.RegisterHandler(Opcodes.CATALOGLOADITEMS, new ProcessRequestCallback(CatalogManager.OnLoadCatalogRequest), false);
            DataRouter.RegisterHandler(Opcodes.CATALOGLOADCONFIRMATION, new ProcessRequestCallback(CatalogManager.OnLoadSuccessRequest), false);
        }

        public static void OnLoadCatalogRequest(Session Session, ClientMessage Message)
        {
            Session.SendData(LoadCatalogComposer.Compose(obj, packetcata));
        }

        public static void OnLoadSuccessRequest(Session Session, ClientMessage Message)
        {
            Session.SendData(ConfirmationCatalogComposer.Compose());
        }

        public static void cargar_objetos()
        {
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                obj = client.ReadInt32("SELECT COUNT(*) FROM catalogo_objetos;");
            }
            cargar_obj();
        }

        public static void cargar_obj()
        {
            int i = 0;
            using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
            {
                DataTable dTable = client.ReadDataSet("SELECT * FROM catalogo_objetos;").Tables[0];
                foreach (DataRow dRow in dTable.Rows)
                {
                    //objetos.Add(int.Parse(dRow["id"].ToString()), int.Parse(dRow["id"].ToString()));
                    if (i++ == obj)
                    {
                        packetcata = packetcata + dRow["id"].ToString() + "³²" + dRow["swf"].ToString() + "³²" + dRow["d1"].ToString() + "³²" + dRow["CP"].ToString() + "³²" + dRow["C"].ToString() + "³²" + dRow["d2"].ToString() + "³²" + dRow["d3"].ToString() + "³²" + dRow["d4"].ToString() + "³²" + dRow["d5"].ToString() + "³²" + dRow["d6"].ToString() + "³²" + dRow["d7"].ToString() + "³²" + dRow["d8"].ToString() + "³²" + dRow["d9"].ToString() + "³²" + dRow["d10"].ToString() + "³²" + dRow["d11"].ToString() + "³²" + dRow["d12"].ToString() + "³²" + dRow["d13"].ToString() + "³²" + dRow["d14"].ToString() + "³²" + dRow["d15"].ToString() + "³²" + dRow["d16"].ToString() + "³²" + dRow["d17"].ToString() + "³²" + dRow["d18"].ToString() + "³²" + dRow["d19"].ToString() + "³²" + dRow["d20"].ToString() + "³²" + dRow["d21"].ToString() + "³²" + dRow["d22"].ToString();
                    }
                    else
                    {
                        packetcata = packetcata + dRow["id"].ToString() + "³²" + dRow["swf"].ToString() + "³²" + dRow["d1"].ToString() + "³²" + dRow["CP"].ToString() + "³²" + dRow["C"].ToString() + "³²" + dRow["d2"].ToString() + "³²" + dRow["d3"].ToString() + "³²" + dRow["d4"].ToString() + "³²" + dRow["d5"].ToString() + "³²" + dRow["d6"].ToString() + "³²" + dRow["d7"].ToString() + "³²" + dRow["d8"].ToString() + "³²" + dRow["d9"].ToString() + "³²" + dRow["d10"].ToString() + "³²" + dRow["d11"].ToString() + "³²" + dRow["d12"].ToString() + "³²" + dRow["d13"].ToString() + "³²" + dRow["d14"].ToString() + "³²" + dRow["d15"].ToString() + "³²" + dRow["d16"].ToString() + "³²" + dRow["d17"].ToString() + "³²" + dRow["d18"].ToString() + "³²" + dRow["d19"].ToString() + "³²" + dRow["d20"].ToString() + "³²" + dRow["d21"].ToString() + "³²" + dRow["d22"].ToString() + "³²";
                    }
                    i++;
                }
            }

            Console.WriteLine("[INIT] Hecho paquete del catalogo [" + i + "]");
        }

        public static string GetObjectData(string data, int id)
        {
            try
            {
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    client.SetParameter("data", id);
                    DataRow userRow = client.ReadDataRow("SELECT * FROM catalogo_objetos WHERE id = @data;");

                    string name = userRow[data].ToString();
                    return name;
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
