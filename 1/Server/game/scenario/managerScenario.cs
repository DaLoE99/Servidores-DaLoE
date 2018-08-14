using System;
using System.Data;
using System.Collections.Generic;

using Boombang.database;

namespace Boombang.game.scenario
{
    public class managerScenario
    {
        public Dictionary<int, dataScenario> areas = new Dictionary<int, dataScenario>();

        public void cargar_areas()
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                DataTable dTable = dbClient.ReadDataSet("SELECT * FROM areas_publicas;").Tables[0];
                foreach (DataRow dRow in dTable.Rows)
                {
                    dataScenario area = dataScenario.parse_area(dRow);
                    
                    if (area != null)
                        areas.Add(area.id_area, area);
                }
            }

            Console.WriteLine("[INIT] Se han cargado " + areas.Count + " areas públicas.");
        }


        public dataScenario area(int id_area)
        {
            dataScenario escenario = Environment.Game.areas.areas[id_area];
            if (escenario != null)
                escenario.es_publica = true;
            return escenario;
        }

        public dataScenario isla(int id_area)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@id_area", id_area);
                DataRow area_row = dbClient.ReadDataRow("SELECT * FROM areas_privadas WHERE id = @id_area;");
                dataScenario escenario = dataScenario.parse_area(area_row);
                if (escenario != null)
                {
                    DataRow data_escenario = dbClient.ReadDataRow("SELECT * FROM areas_privadas WHERE id = '" + escenario.id_area + "'");

                    escenario.es_publica = false;
                }
                return escenario;
            }
        }

        public bool existe_instancia(int id_area, bool es_publica)
        {
            if (es_publica) return Environment.areas.ContainsKey(id_area); else return Environment.islas.ContainsKey(id_area);
        }

        public scenarioInstance get_scenarioInstance(int id_area, bool es_publica)
        {
            if (!existe_instancia(id_area, es_publica))
            {
                Console.WriteLine("[DEBUG] Cargando area: " + Environment.Game.areas.areas[id_area].nombre);
                if (!es_publica) Environment.islas.Add(id_area, new scenarioInstance(id_area, false));
                else Environment.areas.Add(id_area, new scenarioInstance(id_area, true));
            }
            if (es_publica) return Environment.areas[id_area];
            else
            return Environment.islas[id_area];
        }

        public int usuarios_enarea(int id_area, bool es_publica)
        {
            if (existe_instancia(id_area, es_publica)) if (es_publica) return Environment.areas[id_area].Usuarios.Count; else return Environment.islas[id_area].Usuarios.Count; else return 0;
        }
    }
}
