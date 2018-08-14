using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer
{
    public enum OutputLevel
    {
        CriticalError,
        Warning,
        Notification,
        Information
    }
    public class Output
    {
        private static bool Logging = false;
        private static string LogName;
        public static void WriteLine(string Line, OutputLevel Level = OutputLevel.Information)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("[" + DateTime.Now.ToString("hh:mm:ss:") + "] ");
            switch(Level)
            {
                case OutputLevel.CriticalError:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case OutputLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case OutputLevel.Notification:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case OutputLevel.Information:
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
            StringBuilder Output = new StringBuilder();
            int LinePointer = 0;
            for(int CharPointer = 0; CharPointer < Line.Length; CharPointer++)
            {
                if (Line[CharPointer] == Convert.ToChar(10))
                {
                    LinePointer = 0;
                    Output.Append("\n           ");
                    continue;
                }
                if (LinePointer == Console.WindowLeft - 11)
                {
                    Output.Append("           ");
                    LinePointer = 0;
                }
                Output.Append(Line[LinePointer]);
                LinePointer++;
            }
            Console.WriteLine(Output.ToString());
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }
        public static void WriteHeader()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(@"                                                                               ");
            Console.Write(@"         ____                            ____  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("      Iniciado a las:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("          \n");
            Console.Write(@"        |  _ \                          |  _ \         ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(DateTime.Now.ToString("HH:mm:ss"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("                \n");
            Console.WriteLine(@"        | |_) |  ___    ___   _ __ ___  | |_) |  __ _  _ __    __ _            ");
            Console.WriteLine(@"        |  _ <  / _ \  / _ \ | '_ ` _ \ |  _ <  / _` || '_ \  / _` |           ");
            Console.WriteLine(@"        | |_) || (_) || (_) || | | | | || |_) || (_| || | | || (_| |           ");
            Console.WriteLine(@"        |____/  \___/  \___/ |_| |_| |_||____/  \__,_||_| |_| \__, |           ");
            Console.WriteLine(@"                                                               __/ |           ");
            Console.WriteLine(@"                                                              |___/            ");
            Console.WriteLine(@"                                                                               ");
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void StartLogging()
        {
            if (!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory("Loges");
            }
            LogName = "Logs/Log-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
            Logging = true;
        }
        public static void StopLogging()
        {

        }
    }
}
