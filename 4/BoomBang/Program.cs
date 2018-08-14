namespace BoomBang
{
    using BoomBang.Communication.Incoming;
    using BoomBang.Config;
    using BoomBang.Game.Advertisements;
    using BoomBang.Game.Catalog;
    using BoomBang.Game.Characters;
    using BoomBang.Game.Contests;
    using BoomBang.Game.FlowerPower;
    using BoomBang.Game.Games;
    using BoomBang.Game.Handlers;
    using BoomBang.Game.Laptop;
    using BoomBang.Game.Misc;
    using BoomBang.Game.Moderation;
    using BoomBang.Game.Navigation;
    using BoomBang.Game.Register;
    using BoomBang.Game.Sessions;
    using BoomBang.Game.Spaces;
    using BoomBang.Network;
    using BoomBang.Storage;
    using ns0;
    using System;
    using System.Net;
    using System.Security.Permissions;

    public static class Program
    {
        /* private scope */ static bool bool_0;
        /* private scope */ static BoomBangTcpListener boomBangTcpListener_0;

        public static void HandleFatalError(string Message)
        {
            Output.WriteLine(Message, OutputLevel.CriticalError);
            Output.WriteLine("Cannot proceed; press any key to stop the server.", OutputLevel.CriticalError);
            Console.ReadKey(true);
            Stop();
        }

        [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.ControlAppDomain)]
        public static void Main(string[] args)
        {
            bool_0 = true;
            DateTime now = DateTime.Now;
            Output.InitializeStream(true, OutputLevel.DebugInformation);
            Output.WriteLine("Initializing BoomBang game environment...");
            ConfigManager.Initialize(Constants.DataFileDirectory + @"\server-main.cfg");
            Output.SetVerbosityLevel((OutputLevel) ConfigManager.GetValue("output.verbositylevel"));
            foreach (string str in args)
            {
                Output.WriteLine("Command line argument: " + str);
                Input.ProcessInput(str.Split(new char[] { ' ' }));
            }
            try
            {
                Output.WriteLine("Initializing MySQL manager...");
                SqlDatabaseManager.Initialize();
                Output.WriteLine("Setting up server listener on port " + ((int) ConfigManager.GetValue("net.bind.port")) + "...");
                boomBangTcpListener_0 = new BoomBangTcpListener(new IPEndPoint(IPAddress.Any, (int) ConfigManager.GetValue("net.bind.port")), (int) ConfigManager.GetValue("net.backlog"), new OnNewConnectionCallback(SessionManager.HandleIncomingConnection));
                using (SqlDatabaseClient client = SqlDatabaseManager.GetClient())
                {
                    Output.WriteLine("Resetting database counters and statistics...");
                    smethod_0(client);
                    Output.WriteLine("Initializing game components and workers...");
                    DataRouter.Initialize();
                    GlobalHandler.Initialize();
                    SessionManager.Initialize();
                    CharacterInfoLoader.Initialize();
                    UserCredentialsAuthenticator.Initialize();
                    RegisterManager.Initialize();
                    Class1.smethod_0();
                    LaptopHandler.Initialize();
                    CatalogManager.Initialize(client);
                    FlowerPowerManager.Initialize();
                    NewsCacheManager.Initialize(client);
                    Navigator.Initialize(client);
                    SpaceManager.Initialize(client);
                    SpaceInfoLoader.Initialize();
                    SpaceHandler.Initialize();
                    GameHandler.Initialize();
                    CrossdomainPolicy.Initialize(@"Data\crossdomain.xml");
                    WordFilterManager.Initialize(client);
                    AdvertisementManager.Initialize();
                    ContestHandler.Initialize();
                    SilverCoinsWorker.Initialize();
                    ModerationBanManager.Initialize(client);
                }
            }
            catch (Exception exception)
            {
                HandleFatalError("Could not initialize BoomBang game environment: " + exception.Message + "\nStack trace: " + exception.StackTrace);
                return;
            }
            TimeSpan span = (TimeSpan) (DateTime.Now - now);
            Output.WriteLine("The server has initialized successfully (" + Math.Round(span.TotalSeconds, 2) + " seconds). Ready for connections.", OutputLevel.Notification);
            Output.WriteLine("Press the ENTER key for command input. Shut down server with 'STOP' command.", OutputLevel.Notification);
            Console.Beep();
       }

        private static void smethod_0(SqlDatabaseClient sqlDatabaseClient_0)
        {
        }

        public static void Stop()
        {
            Output.WriteLine("Stopping Server...");
            bool_0 = false;
            SqlDatabaseManager.Uninitialize();
            boomBangTcpListener_0.Dispose();
            boomBangTcpListener_0 = null;
            Output.WriteLine("Bye!");
            Environment.Exit(0);
        }

        public static bool Alive
        {
            get
            {
                return (!Environment.HasShutdownStarted && bool_0);
            }
        }
    }
}

