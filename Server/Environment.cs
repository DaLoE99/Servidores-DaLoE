using System;
using System.Data;
using System.Collections.Generic;

using Boombang.sockets;
using Boombang.game.session;
using Boombang.database;
using Boombang.game;

namespace Boombang
{
    class Environment
    {
        private static manager mManager;
        private static sessionManager mSessionManager;
        private static DatabaseManager mDatabaseManager;
        private static BoomBang mGameManager;

        private static Dictionary<int, game.scenario.scenarioInstance> mIslas = new Dictionary<int, game.scenario.scenarioInstance>();
        private static Dictionary<int, game.scenario.scenarioInstance> mAreas = new Dictionary<int, game.scenario.scenarioInstance>();

        public static class connections
        {
            public static connection GetConnection(long connectionId)
            {
                return mManager.GetInstance(connectionId);
            }

            public static void EndConnection(long connectionid)
            {
                mManager.EndConnection(connectionid);
            }

            public static bool CheckIP(string ip)
            {
                return mManager.CheckIP(ip);
            }
        }

        public static class sessions
        {
            public static sessionHandler GetSession(long sessionid)
            {
                return mSessionManager.GetSession(sessionid);
            }

            public static List<sessionHandler> GetSessionList()
            {
                return mSessionManager.GetSessionList();
            }

            public static void AddSession(long sessionid)
            {
                mSessionManager.StartSession(sessionid);
            }

            public static void EndSession(long sessionid)
            {
                mSessionManager.EndSession(sessionid);
            }

            public static void NewSessionData(long sessionid, string data)
            {
                mSessionManager.NewSessionData(sessionid, data);
            }

            public static long GetSessionFromUser(int userid)
            {
                return mSessionManager.GetSessionForUser(userid);
            }

            public static long GetSessionFromUser(string userName)
            {
                return mSessionManager.GetSessionForUser(userName.ToLower());
            }

            public static void InvokeReactorMethod(long sessionId, string method)
            {
                mSessionManager.GetSession(sessionId).InvokeMethod(method);
            }
        }

        public static DatabaseManager GetDatabase()
        {
            return mDatabaseManager;
        }

        public static BoomBang Game
        {
            get
            {
                return mGameManager;
            }
        }

        public static Dictionary<int, game.scenario.scenarioInstance> islas { get { return mIslas; } }

        public static Dictionary<int, game.scenario.scenarioInstance> areas { get { return mAreas; } }

        public static void InitMySQL()
        {
            try
            {
               
                DatabaseServer bDatabaseServer = new DatabaseServer("127.0.0.1", 3306, "root", "576david");

                Database bDatabase = new Database("boombang", 1, 450);

                mDatabaseManager = new DatabaseManager(bDatabaseServer, bDatabase);
                mDatabaseManager.SetClientAmount(2);
                mDatabaseManager.ReleaseClient(mDatabaseManager.GetClient().Handle);
                mDatabaseManager.StartMonitor();
            }
            catch (Exception e)
            {
                Console.WriteLine("[INIT] Error al inicializar MySQL. Excepción: "+ e.ToString());
            }
        }

        public static void Initialize()
        {
            bool Error = false;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("xxxxxx    xxxxxxxxx xxxxxxxxx xxx    xxxx xxxxxx    xxxxxxxxx xxx   xx xxxxxxxxx");
            Console.WriteLine("xx   xxx  xx     xx xx     xx xx xx xx xx xx   xxx  xx     xx xxxx  xx xx       ");
            Console.WriteLine("xx  xxx   xx     xx xx     xx xx  xxx  xx xx  xxx   xxxxxxxxx xx xx xx xx  xxxxx");
            Console.WriteLine("xx    xxx xx     xx xx     xx xx       xx xx    xxx xx     xx xx   xxx xx     xx");
            Console.WriteLine("xxxxxxxx  xxxxxxxxx xxxxxxxxx xx       xx xxxxxxxx  xx     xx xx    xx xxxxxxxxx");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Retro By  MonsterKing  http://area-monster.tk/");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("[INIT] Iniciando el servidor...");
            try
            {
                InitMySQL();
                mGameManager = new BoomBang();
                mSessionManager = new sessionManager();
                mManager = new manager();
                Environment.Game.areas.cargar_areas();
            }
            catch { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[INIT] Error: Se ha producido un error al iniciar el servidor."); Console.ForegroundColor = ConsoleColor.Gray; Error = true; }
            
            Console.ForegroundColor = ConsoleColor.Green;
            if (!Error)
            Console.WriteLine("[INIT] Servidor iniciado.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
