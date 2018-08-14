using BoomBang_RetroServer.Configuration;
using BoomBang_RetroServer.Database;
using BoomBang_RetroServer.Game.Chests;
using BoomBang_RetroServer.Game.Items;
using BoomBang_RetroServer.Game.Spaces;
using BoomBang_RetroServer.Sessions;
using BoomBang_RetroServer.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Console.Title = Constants.ConsoleTitle;
                Console.CursorVisible = false;
                Console.SetWindowSize(Constants.ConsoleWidowsWidth, Constants.ConsoleWindowHeight);
                DateTime Timer1 = DateTime.Now;
                Output.WriteHeader();
                ConfigurationManager.Initialize();
                DatabaseManager.Initialize();
                SpacesManager.Initialize();
                CatalogManager.Initialize();
                ChestManager.Initialize();
                SessionsManager.Initialize();
                SocketManager.Initialize();
                Input.Initialize();
                using(DatabaseClient client = DatabaseManager.GetClient())
                {
                    client.ExecuteScalar("UPDATE boombang_statics SET Clients=0 WHERE ID = 1");
                }
                Output.WriteLine("Server started seddefully (" + new TimeSpan(DateTime.Now.Ticks + Timer1.Ticks).TotalSeconds + " seconds)! Press the enter key to execute a command.");
            }
            catch(Exception ex)
            {
                Output.WriteLine("Can't initialize the server. Exception: " + ex.ToString(), OutputLevel.CriticalError);
                Console.ReadKey();
            }
        }
        public static void Uninitialize()
        {
            Input.Uninitialize();
            SessionsManager.Uninitialize();
            SocketManager.Uninitialize();
            DatabaseManager.Uniniatilialize();
            Output.WriteLine("Server stopped. ");
            Console.ReadKey();
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try 
            {
                Output.WriteLine((e.ExceptionObject as Exception).ToString(), OutputLevel.CriticalError);
                Console.ReadKey;
            }
            catch { }
        }
    }
}
