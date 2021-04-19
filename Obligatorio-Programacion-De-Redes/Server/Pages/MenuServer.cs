using System;

namespace Client
{
    public class MenuServer
    {
        public int ShowMenu( string[] _options)
        {
            bool salir = false;
            int indexA = 0;
            while (!salir)
            {
                for (var i = 0; i < _options.Length; i++)
                {
                    var prefix = "  ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (i == indexA)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        prefix = "> ";
                    }
                
                    Console.WriteLine($"{prefix}{_options[i]}");
                }
                
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return indexA+1;
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        if (indexA > 0)
                            indexA = indexA - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        if (indexA < _options.Length-1)
                            indexA = indexA + 1;
                        else
                            indexA = 0;
                        break;
                    case ConsoleKey.Escape:
                        indexA = _options.Length-1;
                        break;
                }
            }

            return 1;
        }
    }
}