using System;

namespace Client
{
    public class MenuServer
    {
        public int ShowMenu(string[] _options, bool salir)
        {
            int index = 0;
            while (!salir)
            {
                for (var i = 0; i < _options.Length; i++)
                {
                    var prefix = "  ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        prefix = "> ";
                    }

                    Console.WriteLine($"{prefix}{_options[i]}");
                }

                if (!Console.KeyAvailable)
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Enter:
                            Console.Clear();
                            return index + 1;
                        case ConsoleKey.UpArrow:
                            Console.Clear();
                            if (index > 0)
                                index = index - 1;
                            break;
                        case ConsoleKey.DownArrow:
                            Console.Clear();
                            if (index < _options.Length - 1)
                                index = index + 1;
                            else
                                index = 0;
                            break;
                        case ConsoleKey.Escape:
                            index = _options.Length - 1;
                            break;
                    }

                    salir = false;
                }
                else
                {
                    salir = true;
                }

            }

            return 0;
        }
    }
}