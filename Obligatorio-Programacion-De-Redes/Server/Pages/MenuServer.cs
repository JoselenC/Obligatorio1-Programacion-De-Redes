﻿using System;

namespace Client
{
    public class MenuServer
    {
        public int ShowMenu( string[] _options)
        {
            bool salir = false;
            int indexMenu = 0;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("----Menu----");
                for (var i = 0; i < _options.Length; i++)
                {
                    var prefix = "  ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (i == indexMenu)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        prefix = "> ";
                    }

                    Console.WriteLine($"{prefix}{_options[i]}");
                }

                indexMenu = 0;
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
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
                        return 1;
                        ;
                }
            }
            return 1;
        }
    }
}