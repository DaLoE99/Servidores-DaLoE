using System;

namespace Boombang
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Boombang server: BoomBang game Environment";
            Console.WindowHeight = Console.LargestWindowHeight - 50;
            Console.WindowWidth = Console.LargestWindowWidth - 125;
            Console.CursorVisible = false;
            Console.Beep();

            Environment.Initialize();
        }
    }
}
