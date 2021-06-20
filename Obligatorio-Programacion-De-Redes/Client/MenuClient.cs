using System;
using System.Threading.Tasks;

namespace Client
{
    public class MenuClient
    {
        public async Task<int> ShowMenuAsync( string[] options, string title)
        {
            bool exit = false;
            int indexMenu = 0;
            while (!exit)
            {
                PrintOptions(options, title, indexMenu);

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return indexMenu+1;
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
                        exit = true;
                        break;
                    default:
                        return 0;
                        ;
                }
            }
            return 0;
        }

        private void PrintOptions(string[] options, string title, int indexMenu)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
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
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    prefix = "> ";
                }

                Console.WriteLine($"{prefix}{options[i]}");
            }
        }
    }
}