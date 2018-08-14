namespace BoomBang
{
    using BoomBang.Config;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public static class Output
    {
        /* private scope */ static bool bool_0;
        /* private scope */ static object object_0;
        /* private scope */ static OutputLevel outputLevel_0;
        /* private scope */ static string string_0;

        public static void ClearStream()
        {
            Console.Clear();
            Console.Title = Constants.ConsoleTitle;
            Console.WindowWidth = Constants.ConsoleWindowWidth;
            Console.WindowHeight = Constants.ConsoleWindowHeight;
            smethod_0();
        }

        public static void InitializeStream(bool EnableLogging, OutputLevel VerbosityLevel)
        {
            bool_0 = EnableLogging;
            outputLevel_0 = VerbosityLevel;
            object_0 = new object();
            if (EnableLogging)
            {
                DateTime now = DateTime.Now;
                string path = Environment.CurrentDirectory + Constants.LogFileDirectory + @"\";
                string_0 = string.Concat(new object[] { path, now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second, ".log" });
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    File.WriteAllText(string_0, smethod_4(), Constants.DefaultEncoding);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error thrown by StreamWriter. Stack trace:\r\n" + exception.ToString());
                    bool_0 = false;
                }
            }
            ClearStream();
            WriteBanner();
        }

        public static void SetVerbosityLevel(OutputLevel OutputLevel)
        {
            outputLevel_0 = OutputLevel;
        }

        private static void smethod_0()
        {
            smethod_2(ConsoleColor.Gray, ConsoleColor.Black);
        }

        private static void smethod_1(ConsoleColor consoleColor_0)
        {
            smethod_2(consoleColor_0, ConsoleColor.Black);
        }

        private static void smethod_2(ConsoleColor consoleColor_0, ConsoleColor consoleColor_1)
        {
            Console.ForegroundColor = consoleColor_0;
            Console.BackgroundColor = consoleColor_1;
        }

        private static void smethod_3(OutputLevel outputLevel_1)
        {
            switch (outputLevel_1)
            {
                case OutputLevel.DebugInformation:
                    smethod_1(ConsoleColor.DarkGray);
                    return;

                case OutputLevel.DebugNotification:
                    smethod_1(ConsoleColor.DarkGreen);
                    return;

                case OutputLevel.Notification:
                    smethod_1(ConsoleColor.Green);
                    return;

                case OutputLevel.Warning:
                    smethod_1(ConsoleColor.Yellow);
                    return;

                case OutputLevel.CriticalError:
                    smethod_1(ConsoleColor.Red);
                    return;
            }
            smethod_0();
        }

        private static string smethod_4()
        {
            StringBuilder builder = new StringBuilder("## BoomBang" + Constants.LineBreakChar);
            builder.Append("## Server output log file" + Constants.LineBreakChar);
            builder.Append(string.Concat(new object[] { "## ", DateTime.Now.ToLongDateString(), " ", DateTime.Now.ToLongTimeString(), Constants.LineBreakChar }));
            builder.Append(Constants.LineBreakChar);
            return builder.ToString();
        }

        private static void smethod_5(string string_1)
        {
            if (bool_0)
            {
                lock (object_0)
                {
                    File.AppendAllText(string_0, smethod_6() + string_1 + Constants.LineBreakChar, Constants.DefaultEncoding);
                }
            }
        }

        private static string smethod_6()
        {
            return ("[" + DateTime.Now.ToLongTimeString() + "] ");
        }

        public static void WriteBanner()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (string str in new List<string> { " ", "  ______                     ______                           ", @"  | ___ \                    | ___ \                        ", "  | |_/ /___  ___  _ __ ___  | |_/ / __ _ _ __   ___          ", @"  | ___\/ _ \/ _ \| '_ ` _ \ | ___ \/ _` | '_ `./ _ \    Burbian Server", "  | |_/  (_)  (_) | | | | | || |_/ | (_| | | | | (_)|    Development version", @"  \____/\___/\___/|_| |_| |_|\____/\,__|_|_| |_|\__ |    Build Version 1", "                                                  |_|    www.burbian.net", "" })
            {
                int num = (Console.WindowWidth - str.Length) - 1;
                string str2 = str;
                for (int i = 0; i < num; i++)
                {
                    str2 = str2 + ' ';
                }
                Console.WriteLine(str2);
            }
            Console.WriteLine();
            smethod_1(ConsoleColor.DarkGray);
            smethod_1(ConsoleColor.DarkGray);
            if (outputLevel_0 < OutputLevel.Informational)
            {
                Console.WriteLine();
            }
            smethod_0();
        }

        public static void WriteLine()
        {
            if (outputLevel_0 <= OutputLevel.Notification)
            {
                Console.WriteLine();
                smethod_5(Constants.LineBreakChar.ToString());
            }
        }

        public static void WriteLine(string Line)
        {
            WriteLine(Line, OutputLevel.Informational);
        }

        public static void WriteLine(string Line, OutputLevel Level)
        {
            if (outputLevel_0 <= Level)
            {
                Console.Write(smethod_6());
                smethod_3(Level);
                Console.WriteLine(Line);
                smethod_0();
                smethod_5(Line);
            }
        }
    }
}

