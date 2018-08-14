using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoomBang_RetroServer
{
    public static class Input
    {
        private static Dictionary<string, string> Commands;
        private static Thread Listener;
        public static void Initialize()
        {
            Commands = new Dictionary<string, string>();
            Commands.Add("help", "Shows all commands");
            Commands.Add("alertarea", "Sends an alert message to all areas");
            Commands.Add("notification", "Send a yellow notification to all areas.");
            Commands.Add("alertmsg", "Sends an alert message to all online users' bpad");
            Commands.Add("stop", "Stops the server safetly.");
            Listener = new Thread(new ThreadStart(Listen));
            try
            {
                Listener.Start();
            }
            catch (Exception ex)
            {
                Output.WriteLine(ex.ToString());
                Listener.Abort();
            }
            Output.WriteLine("Console command reader initialized.", OutputLevel.Information);
        }
        public static void Uninitialize()
        {
            Commands = null;
            Listener.Abort();
            Console.CursorVisible = false;
            Output.WriteLine("Console command reader uninitialized.", OutputLevel.Information);

        }
        private static void Listen()
        {
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {

                }
            }
        }
    }
}
