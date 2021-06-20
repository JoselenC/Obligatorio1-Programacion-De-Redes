using System;

namespace Server.Server
{
    public class MenuServer
    {
        public int ShowMenu(string[] options, string title)
        {
            bool exit = false;
            int indexMenu = 0;
            while (!exit)
            {
                PrintOptions(options, title, indexMenu);
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        return indexMenu + 1;
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        if (indexMenu > 0)
                            indexMenu = indexMenu - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        if (indexMenu < options.Length - 1)
                            indexMenu = indexMenu + 1;
                        else
                            indexMenu = 0;
                        break;
                    case ConsoleKey.Escape:
                        indexMenu = options.Length - 1;
                        break;
                    default:
                        return 0;
                        ;
                }
            }

            return 0;
        }

        private static void PrintOptions(string[] options, string title, int indexMenu)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----" + title + "----");
            Console.ForegroundColor = ConsoleColor.White;
            for (var i = 0; i < options.Length; i++)
            {
                var prefix = "  ";
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                if (i == indexMenu)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    prefix = "> ";
                }

                Console.WriteLine($"{prefix}{options[i]}");
            }
        }
    }
}