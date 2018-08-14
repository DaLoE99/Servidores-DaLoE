using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snowlight.Config;
using Snowlight.Game.Sessions;
using Snowlight.Communication;
using Snowlight.Communication.ResponseCache;
using Snowlight.Communication.Incoming;
using Snowlight.Storage;
using System.Data;
using Snowlight.Game.Spaces;
using Snowlight.Communication.Outgoing;

namespace Snowlight.Game.Navigation
{
    class Navigator
    {
        /* private scope */
        static Dictionary<string, int> dictionary_0;
        /* private scope */
        static List<NavigatorCategory> list_0;
        /* private scope */
        static List<NavigatorItem> list_1;
        /* private scope */
        static ResponseCacheController responseCacheController_0;

        public static void Initialize(SqlDatabaseClient MySqlClient)
        {
            list_0 = new List<NavigatorCategory>();
            list_1 = new List<NavigatorItem>();
            dictionary_0 = new Dictionary<string, int>();
            if ((bool)ConfigManager.GetValue("cache.navigator.enabled"))
            {
                responseCacheController_0 = new ResponseCacheController(0, 0);
            }
            ReloadOfficialItems(MySqlClient);
            DataRouter.RegisterHandler(Opcodes.NAVIGATORITEMSLOAD, new ProcessRequestCallback(Navigator.smethod_5), false);
            DataRouter.RegisterHandler(Opcodes.NAVIGATORSUBITEMSLOAD, new ProcessRequestCallback(Navigator.smethod_6), false);
        }

        public static void ReloadOfficialItems(SqlDatabaseClient MySqlClient)
        {
            int num = 0;
            lock (list_1)
            {
                list_1.Clear();
                foreach (DataRow row in MySqlClient.ExecuteQueryTable("SELECT * FROM escenarios WHERE tipo_area = 'area' ORDER BY id ASC").Rows)
                {
                    list_1.Add(new NavigatorItem((uint)row["id"], (uint)row["id_principal"], (uint)row["modelo_area"], (string)row["nombre"], ((uint)row["es_categoria"]) == 1, smethod_0((uint)row["categoria"])));
                    num++;
                }
            }
            Output.WriteLine("Loaded " + num + " navigator static item(s).", OutputLevel.DebugInformation);
        }

        private static NavigatorCategory smethod_0(uint uint_0)
        {
            switch (uint_0)
            {
                case 1:
                    return NavigatorCategory.Area;

                case 2:
                    return NavigatorCategory.Island;

                case 3:
                    return NavigatorCategory.Game;

                case 4:
                    return NavigatorCategory.Home;
            }
            return NavigatorCategory.Area;
        }

        private static SpaceType smethod_1(uint uint_0)
        {
            switch (uint_0)
            {
                case 1:
                    return SpaceType.Area;

                case 2:
                    return SpaceType.Island;

                case 3:
                    return SpaceType.Game;

                case 4:
                    return SpaceType.Home;
            }
            return SpaceType.Area;
        }

        private static ServerMessage smethod_2(uint uint_0, ClientMessage clientMessage_0)
        {
            if (!CacheEnabled)
            {
                return null;
            }
            return responseCacheController_0.TryGetResponse(uint_0, clientMessage_0);
        }

        private static void smethod_3(uint uint_0, ClientMessage clientMessage_0, ServerMessage serverMessage_0)
        {
            if (CacheEnabled)
            {
                responseCacheController_0.AddIfNeeded(uint_0, clientMessage_0, serverMessage_0);
            }
        }

        private static void smethod_4(uint uint_0)
        {
            if (CacheEnabled)
            {
                responseCacheController_0.ClearCacheGroup(uint_0);
            }
        }

        private static void smethod_5(Session session_0, ClientMessage clientMessage_0)
        {
            try
            {
                ServerMessage message = smethod_2(0, clientMessage_0);
                if (message != null)
                {
                    session_0.SendData(message);
                }
                else
                {
                    switch (clientMessage_0.ReadUnsignedInteger())
                    {
                        case 1:
                            message = NavigatorItemsComposer.Message(list_1, null, 1);
                            smethod_3(0, clientMessage_0, message);
                            break;

                        case 2:
                            {
                                List<NavigatorItem> itemContainer = new List<NavigatorItem>();
                                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                                {
                                    foreach (DataRow row in client.ExecuteQueryTable("SELECT * FROM islas WHERE id_parent = '0' ORDER BY visitantes LIMIT 25").Rows)
                                    {
                                        itemContainer.Add(new NavigatorItem((uint)row["id"], 0, 0, (string)row["nombre"], false, NavigatorCategory.Island));
                                    }
                                    message = NavigatorItemsComposer.Message(itemContainer, null, 2);
                                    smethod_3(0, clientMessage_0, message);
                                    break;
                                }
                            }
                        case 3:
                            message = NavigatorItemsComposer.Message(list_1, null, 3);
                            smethod_3(0, clientMessage_0, message);
                            break;

                        case 4:
                            message = NavigatorItemsComposer.Message(list_1, null, 4);
                            smethod_3(0, clientMessage_0, message);
                            break;

                        case 5:
                            message = NavigatorItemsComposer.Message(list_1, null, 5);
                            smethod_3(0, clientMessage_0, message);
                            break;

                        default:
                            message = NavigatorItemsComposer.Message(list_1, null, 1);
                            smethod_3(0, clientMessage_0, message);
                            break;
                    }
                    session_0.SendData(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void smethod_6(Session session_0, ClientMessage clientMessage_0)
        {
            ServerMessage message = smethod_2(0, clientMessage_0);
            if (message != null)
            {
                session_0.SendData(message);
            }
            else
            {
                uint categoryId = clientMessage_0.ReadUnsignedInteger();
                clientMessage_0.ReadUnsignedInteger();
                uint mainId = clientMessage_0.ReadUnsignedInteger();
                message = NavigatorSubItemsComposer.Compose(categoryId, mainId, list_1);
                smethod_3(0, clientMessage_0, message);
                session_0.SendData(message);
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

        public static int MaxFavoritesPerUser
        {
            get
            {
                return (int)ConfigManager.GetValue("navigator.maxfavoritesperuser");
            }
        }

        public static int MaxIslandsPerUser
        {
            get
            {
                return (int)ConfigManager.GetValue("navigator.maxSpacesperuser");
            }
        }
    }
}
