using System;
using System.Net.Sockets;
using Protocol;

namespace Client
{
    public class ThemePage
    {
        public void ShowMenu(Socket SocketClient,SocketHandler socketHandler)
        {
            Console.Clear();
            Console.Write("Select option");
            Console.WriteLine("1-Add theme");
            Console.WriteLine("2-Modify theme");
            Console.WriteLine("3-Delete theme");
            Console.WriteLine("4-Back");
            bool exit = false;
            while (!exit)
            {

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        AddTheme(socketHandler);
                        break;
                    case "2":
                        ModifyTheme(socketHandler);
                        break;
                    case "3":
                        DeleteTheme(socketHandler);
                        break;
                    case "4":
                        exit = true;
                        new HomePage().ShowMenu(SocketClient,socketHandler);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private void DeleteTheme(SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }

        private void ModifyTheme(SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }

        private void AddTheme(SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }
    }
}