using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Net;
using System.Threading;

using Snowlight.Communication.Incoming;
using Snowlight.Config;
using Snowlight.Network;
using Snowlight.Storage;
//using Snowlight.Plugins;
using Snowlight.Util;

using Snowlight.Game;
using Snowlight.Game.Sessions;
using Snowlight.Game.Handlers;
using System.Text;
using Snowlight.Game.Characters;
using Snowlight.Game.Laptop;
using Snowlight.Game.Contest;
using Snowlight.Game.Catalog;
using Snowlight.Game.Misc;
using Snowlight.Game.Spaces;
using Snowlight.Game.Navigation;


namespace Snowlight
{
    public static class Program
    {
        private static bool mAlive;
        private static SnowTcpListener mServer;
        private static SnowTcpListener musServer;

        /// <summary>
        /// Should be used by all non-worker threads to check if they should remain alive, allowing for safe termination.
        /// </summary>
        public static bool Alive
        {
            get
            {
                return (!Environment.HasShutdownStarted && mAlive);
            }
        }

        public static bool DEBUG
        {
            get
            {
                return true;
            }
        }





        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        public static void Main(string[] args)
        {
            mAlive = true;
            DateTime InitStart = DateTime.Now;

            // Set up basic output            
            Console.WriteLine("Initializing Snowlight..."); // Cannot be localized before config+lang is loaded

            // Load configuration, translation, and re-configure output from config data
            ConfigManager.Initialize(Constants.DataFileDirectory + "server-main.cfg");
			Output.InitializeStream(true, (OutputLevel)ConfigManager.GetValue("output.verbositylevel"));
			Output.WriteLine("Initializing Snowlight...");
			
            Localization.Initialize(Constants.LangFileDirectory  + "lang_" + ConfigManager.GetValue("lang") + ".lang");

            // Process args
            foreach (string arg in args)
            {
                Output.WriteLine(Localization.GetValue("core.init.cmdarg", arg));
                Input.ProcessInput(arg.Split(' '));
            }

            try
            {
                // Initialize and test database
                Output.WriteLine(Localization.GetValue("core.init.mysql"));
                SqlDatabaseManager.Initialize();

                // Initialize network components
                Output.WriteLine(Localization.GetValue("core.init.net", ConfigManager.GetValue("net.bind.port").ToString()));
                mServer = new SnowTcpListener(new IPEndPoint((IPAddress)ConfigManager.GetValue("net.bind.ip"), (int)ConfigManager.GetValue("net.bind.port")),
                    (int)ConfigManager.GetValue("net.backlog"), new OnNewConnectionCallback(
                        SessionManager.HandleIncomingConnection));

                /*Output.WriteLine(Localization.GetValue("core.init.net", ConfigManager.GetValue("net.cmd.bind.port").ToString()));
                musServer = new SnowTcpListener(new IPEndPoint((IPAddress)ConfigManager.GetValue("net.cmd.bind.ip"), (int)ConfigManager.GetValue("net.cmd.bind.port")),
                    (int)ConfigManager.GetValue("net.backlog"), new OnNewConnectionCallback(
                        CommandListener.parse));*/

                using (SqlDatabaseClient MySqlClient = SqlDatabaseManager.GetClient())
                {
                    Output.WriteLine(Localization.GetValue("core.init.dbcleanup"));
                    PerformDatabaseCleanup(MySqlClient);

                    Output.WriteLine(Localization.GetValue("core.init.game"));

                    // Core
                    DataRouter.Initialize();

                    // Sessions, characters
                    SessionManager.Initialize();

                    //
                    RandomGenerator.Initialize();
                    StatisticsSyncUtil.Initialize();

                    //Global Handler
                    Global.Initialize();

                    //Login Handler
                    Login.Initialize();
                    CharacterInfoLoader.Initialize();
                    UserCredentialsAuthenticator.Initialize();
                    //Bpad Handler
                    LaptopHandler.Initialize();

                    //FlowerHandler
                    FlowerPower.Initialize();
                    ContestHandler.Initialize();
                    CatalogManager.Initialize(MySqlClient);
                    NewsCacheManager.Initialize(MySqlClient);
                    SpaceInfoLoader.Initialize();
                    Navigator.Initialize(MySqlClient);
                    LaptopHandler.Initialize();
                    UserCredentialsAuthenticator.Initialize();
                    SpaceManager.Initialize(MySqlClient);

                    SpaceHandler.Initialize();


                    SilverCoinsWorker.Initialize();
                    
                    
                }
            }
            catch (Exception e)
            {
                HandleFatalError(Localization.GetValue("core.init.error.details", new string[] { e.Message, e.StackTrace }));
                return;
            }

            // Init complete
            TimeSpan TimeSpent = DateTime.Now - InitStart;

            Output.WriteLine(Localization.GetValue("core.init.ok", Math.Round(TimeSpent.TotalSeconds, 2).ToString()), OutputLevel.Notification);
            Output.WriteLine((string)Localization.GetValue("core.init.ok.cmdinfo"), OutputLevel.Notification);
			
			Console.Write("$" + Environment.UserName.ToLower() + "@snowlight> ");
            Console.Beep();
            Input.Listen(); // This will make the main thread process console while Program.Alive.
        }

        private static void PerformDatabaseCleanup(SqlDatabaseClient MySqlClient)
        {
            /*MySqlClient.ExecuteNonQuery("UPDATE rooms SET current_users = 0");
            MySqlClient.SetParameter("timestamp", UnixTimestamp.GetCurrent());
            MySqlClient.ExecuteNonQuery("UPDATE room_visits SET timestamp_left = @timestamp WHERE timestamp_left = 0");
            MySqlClient.ExecuteNonQuery("UPDATE characters SET auth_ticket = ''");
            MySqlClient.ExecuteNonQuery("UPDATE characters SET online = '0'");
            MySqlClient.SetParameter("timestamp", UnixTimestamp.GetCurrent());
            MySqlClient.ExecuteNonQuery("UPDATE server_statistics SET sval = @timestamp WHERE skey = 'stamp' LIMIT 1");
            MySqlClient.ExecuteNonQuery("UPDATE server_statistics SET sval = '1' WHERE skey = 'online_state' LIMIT 1");*/

        }

        public static void HandleFatalError(string Message)
        {
            Output.WriteLine(Message, OutputLevel.CriticalError);
            Output.WriteLine((string)Localization.GetValue("core.init.error.pressanykey"), OutputLevel.CriticalError);

            Console.ReadKey(true);

            Stop();
        }
    
        public static void Stop()
        {
            Output.WriteLine(Localization.GetValue("core.uninit"));

            mAlive = false; // Will destroy any threads looping for Program.Alive.
            /*using (SqlDatabaseClient MySqlClient = SqlDatabaseManager.GetClient())
            {
                MySqlClient.ExecuteNonQuery("UPDATE server_statistics SET sval = '0' WHERE skey = 'online_state' LIMIT 1");
            }*/
            SqlDatabaseManager.Uninitialize();

            mServer.Dispose();
            mServer = null;

            Environment.Exit(0);
        }
    }
}
