using System;

namespace Boombang
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "BoomBang RetroServer";
            Console.CursorVisible = false;
            Console.Beep();

            Environment.Initialize();
        }
    }
}
