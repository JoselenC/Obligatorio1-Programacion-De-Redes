﻿using System;

namespace Client
{
    public class ThemePage
    {
        public void ShowMenu()
        {
            Console.WriteLine("1-Dar de alta");
            Console.WriteLine("2-Modificar");
            Console.WriteLine("3-Borrar");
            Console.WriteLine("4-Volver");
            bool exit = false;
            while (!exit)
            {

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opcion invalida...");
                        break;
                }
            }
        }
    }
}