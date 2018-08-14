using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Game.Items
{
    class CatalogManager
    {
        public static Dictionary<int, CatalogGroup> Items = new Dictionary<int, CatalogGroup>();
        public static Dictionary<int, FurniGroup> Furnis = new Dictionary<int, FurniGroup>();
        public static int total_load_items;
        public static void Initialize()
        {
            
        }
    }
}
