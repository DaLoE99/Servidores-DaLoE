namespace BoomBang
{
    using BoomBang.Communication.Outgoing;
    using BoomBang.Game.Misc;
    using BoomBang.Game.Sessions;
    using BoomBang.Utils;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;

    public static class Input
    {
        public static void CleanCollections(int Ticks)
        {
            while (Program.Alive)
            {
                Thread.Sleep(Ticks);
            }
        }

        public static void Listen()
        {
            Process.GetCurrentProcess();
            while (Program.Alive)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    Console.Write("$" + Environment.UserName.ToLower() + "@daloe> ");
                    string str = Console.ReadLine();
                    if (str.Length > 0)
                    {
                        ProcessInput(str.Split(new char[] { ' ' }));
                    }
                }
            }
        }
        public static void ProcessInput(string[] Args)
        {
            string key = Args[0].ToLower();
            if (key != null)
            {
                int num2 = 0;
            {
                    Dictionary<string, int> dictionary1 = new Dictionary<string, int>(9);
                    dictionary1.Add("delay", 0);
                    dictionary1.Add("restart", 1);
                    dictionary1.Add("crash", 2);
                    dictionary1.Add("stop", 3);
                    dictionary1.Add("cls", 4);
                    dictionary1.Add("recache_news", 5);
                    dictionary1.Add("laptop_alert", 6);
                    dictionary1.Add("lock", 7);
                    dictionary1.Add("disconnect", 8);
                    dictionary1.Add("HELP", 9);
                    dictionary1.Add("help", 9);
                  
                   switch(num2)
                    {
                        case 0:
                        {
                            int result = 0x1388;
                            if (Args.Length > 1)
                            {
                                int.TryParse(Args[1], out result);
                            }
                            Thread.Sleep(result);
                            return;
                        }
                        case 1:
                            Process.Start(Environment.CurrentDirectory + @"\BoomBang.exe", "\"delay 1500\"");
                            Program.Stop();
                            return;

                        case 2:
                            Environment.FailFast(string.Empty);
                            return;

                        case 3:
                            Program.Stop();
                            return;

                        case 4:
                            Output.ClearStream();
                            return;

                        case 5:
                            NewsCacheManager.ReCacheNews();
                            return;

                        case 6:
                            foreach (KeyValuePair<uint, Session> pair in SessionManager.Sessions)
                            {
                                pair.Value.SendData(LaptopMessageComposer.Compose(pair.Value.CharacterId, InputFilter.MergeString(Args, 1), 2), false);
                            }
                            return;

                        case 7:
                            SessionManager.RejectIncomingConnections = true;
                            return;

                        case 8:
                            SessionManager.StopSession(Convert.ToUInt32(Args[1]));
                            return;
                       case 9:
                            
                             Console.WriteLine("restart -> Reinicia el server");
                             Console.WriteLine("crash -> Apaga el server");
                             Console.WriteLine("stop -> Cierra el server");
                             Console.WriteLine("laptop_alert -> Enviar mensaje de BBTeam*");
                             Console.WriteLine("cls -> Borrar todas las las líneas del servidor");
                             Console.WriteLine("");
                             Console.WriteLine("");
                             Console.WriteLine("");
                             Console.WriteLine("*Para que haga efecto el laptop_alert debes escribir algo como a continuación:");
                             Console.ForegroundColor = ConsoleColor.White;
                             Console.WriteLine("laptop_alert Mensaje a enviar");
                            return;
                    }
                }
          Output.WriteLine("'" + Args[0].ToLower() + "' is not recognized as a command or internal operation.", OutputLevel.Warning);
            }
        }
    }
}


        