using System;
using System.Data;
using System.Collections.Generic;

namespace Boombang.game.scenario
{
    public class dataScenario
    {
        public int id_area;
        public string nombre;
        public string descripcion;

        public int id_principal;
        public int visitantes;
        public int max_visitantes;
        public int categoria;
        public int modelo_area;
        public int permitir_uppercut;
        public int permitir_coco;
        public bool es_publica;

        public static dataScenario parse_area(DataRow area)
        {
            try
            {
                dataScenario escenario = new dataScenario();
                escenario.id_area = Convert.ToInt32(area["id"]);
                escenario.id_principal = Convert.ToInt32(area["id_principal"]);
                escenario.nombre = area["nombre"].ToString();
                escenario.categoria = Convert.ToInt32(area["categoria"]);
                escenario.modelo_area = Convert.ToInt32(area["modelo_area"]);
                escenario.max_visitantes = Convert.ToInt32(area["max_visitantes"]);
                escenario.permitir_uppercut = Convert.ToInt32(area["permitir_uppercut"]);
                escenario.permitir_coco = Convert.ToInt32(area["permitir_coco"]);
                escenario.visitantes = 0;
                escenario.es_publica = true;

                return escenario;
            }
            catch { return null; }
        }
    }
}
