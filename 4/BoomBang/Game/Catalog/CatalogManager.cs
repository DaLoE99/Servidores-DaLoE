namespace BoomBang.Game.Catalog
{
    using BoomBang;
    using BoomBang.Communication;
    using BoomBang.Communication.Incoming;
    using BoomBang.Communication.Outgoing;
    using BoomBang.Communication.ResponseCache;
    using BoomBang.Config;
    using BoomBang.Game.Items;
    using BoomBang.Game.Sessions;
    using BoomBang.Storage;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public static class CatalogManager
    {
        /* private scope */ static Dictionary<int, List<CatalogItem>> dictionary_0;
        /* private scope */ static Dictionary<uint, CatalogItem> dictionary_1;
        /* private scope */ static Dictionary<string, CatalogItem> dictionary_2;
        /* private scope */ static List<CatalogItem> list_0;
        /* private scope */ static ResponseCacheController responseCacheController_0;
        public static Dictionary<int, int> objetos = new Dictionary<int, int>();
        public static int obj = 0;
        public static string packetcata;

        public static void ClearCacheGroup(uint GroupId)
        {
            if (CacheEnabled)
            {
                responseCacheController_0.ClearCacheGroup(GroupId);
            }
        }

        public static CatalogItem GetCatalogItemByAbsoluteId(uint ItemId)
        {
            lock (dictionary_1)
            {
                if (dictionary_1.ContainsKey(ItemId))
                {
                    return dictionary_1[ItemId];
                }
            }
            return null;
        }

        public static CatalogItem GetCatalogItemByPage(int PageId, uint ItemId)
        {
            lock (dictionary_0)
            {
                if (dictionary_0.ContainsKey(PageId))
                {
                    foreach (CatalogItem item in dictionary_0[PageId])
                    {
                        if (item.UInt32_0 == ItemId)
                        {
                            return item;
                        }
                    }
                }
            }
            return null;
        }

        public static void Initialize(SqlDatabaseClient MySqlClient)
        {
            dictionary_0 = new Dictionary<int, List<CatalogItem>>();
            dictionary_1 = new Dictionary<uint, CatalogItem>();
            dictionary_2 = new Dictionary<string, CatalogItem>();
            list_0 = new List<CatalogItem>();
            if ((bool) ConfigManager.GetValue("cache.catalog.enabled"))
            {
                responseCacheController_0 = new ResponseCacheController((int) ConfigManager.GetValue("cache.catalog.lifetime"), (uint) ((int) ConfigManager.GetValue("cache.catalog.maximaldata")));
            }
            DataRouter.RegisterHandler(FlagcodesIn.CATALOG, ItemcodesIn.CATALOG_LOAD_ITEMS, new ProcessRequestCallback(CatalogManager.OnLoadCatalogRequest), false);
            DataRouter.RegisterHandler(FlagcodesIn.CATALOG, ItemcodesIn.CATALOG_LOAD_CONFIRMATION, new ProcessRequestCallback(CatalogManager.OnLoadSuccessRequest), false);
            RefreshCatalogData(MySqlClient, false);
        }

        public static void OnLoadCatalogRequest(Session Session, ClientMessage Message)
        {
            Session.SendData(LoadCatalogComposer.Compose(obj, packetcata));
        }

               public static void OnLoadSuccessRequest(Session Session, ClientMessage Message)
        {
            Session.SendData(ConfirmationCatalogComposer.Compose(), false);
        }

        public static void RefreshCatalogData(SqlDatabaseClient MySqlClient, bool NotifyUsers = true)
        {
            int num = 0;
            dictionary_0.Clear();
            dictionary_1.Clear();
            dictionary_2.Clear();
            MySqlClient.SetParameter("enabled", "1");
            foreach (DataRow row in MySqlClient.ExecuteQueryTable("SELECT * FROM catalogo_objetos WHERE activado = @enabled ORDER BY id ASC").Rows)
            {
                int key = (int) row["pagina_catalogo"];
                if (!dictionary_0.ContainsKey(key))
                {
                    dictionary_0[key] = new List<CatalogItem>();
                }
                CatalogItem item = new CatalogItem((uint) row["id"], (string) row["nombre_base"], (string) row["nombre"], (string) row["descripcion"], (int) row["precio_oro"], (int) row["precio_plata"], (int) row["pagina_catalogo"], (string) row["color"], (string) row["color_servidor"], (string) row["color_partes"], (string) row["color_partes_servidor"], (string) row["parte_1"], (string) row["parte_2"], (string) row["parte_3"], (string) row["parte_4"], (double) row["tam_1"], (double) row["tam_2"], (double) row["tam_3"], (uint) row["tipo"], (uint) row["tam_relleno"], (uint) row["ubicacion"], row["activado"].ToString() == "1", (uint) row["tipo_rare"], (uint) row["sizable"], (uint) row["intercambiable"], (uint) row["vip"]);
                if (string.IsNullOrEmpty(item.BaseName))
                {
                    Output.WriteLine("Warning: Catalog item " + ((uint) row["id"]) + " has an invalid base_id reference.", OutputLevel.Warning);
                }
                else
                {
                    dictionary_0[key].Add(item);
                    dictionary_1[item.UInt32_0] = item;
                    dictionary_2[item.DisplayName] = item;
                    list_0.Add(item);
                    num++;
                }
            }
            Output.WriteLine("Loaded " + num + " Catalog items in 11 Catalog pages.", OutputLevel.DebugInformation);
        }

        private static ServerMessage smethod_0(uint uint_0, ClientMessage clientMessage_0)
        {
            if (!CacheEnabled)
            {
                return null;
            }
            return responseCacheController_0.TryGetResponse(uint_0, clientMessage_0);
        }

        private static void smethod_1(uint uint_0, ClientMessage clientMessage_0, ServerMessage serverMessage_0)
        {
            if (CacheEnabled)
            {
                responseCacheController_0.AddIfNeeded(uint_0, clientMessage_0, serverMessage_0);
            }
        }

        public static ResponseCacheController CacheController
        {
            get
            {
                return responseCacheController_0;
            }
        }

        public static bool CacheEnabled
        {
            get
            {
                return (responseCacheController_0 != null);
            }
        }
    }
}

