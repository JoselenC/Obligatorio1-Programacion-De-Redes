﻿using System;
using System.Threading.Tasks;

namespace Client
{
    public class MenuClient
    {
        public async Task<int> ShowMenuAsync( string[] _options, string title)
        {
            bool salir = false;
            int indexMenu = 0;
            while (!salir)
            {
                PrintOptions(_options, title, indexMenu);

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
                        if (indexMenu < _options.Length - 1)
                            indexMenu = indexMenu + 1;
                        else
                            indexMenu = 0;
                        break;
                    case ConsoleKey.Escape:
                        indexMenu = _options.Length - 1;
                        break;
                    default:
                        return 0;
                        ;
                }
            }
            return 0;
        }

        private void PrintOptions(string[] _options, string title, int indexMenu)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("----" + title + "----");
            Console.ForegroundColor = ConsoleColor.White;
            for (var i = 0; i < _options.Length; i++)
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

                Console.WriteLine($"{prefix}{_options[i]}");
            }
        }
    }
}