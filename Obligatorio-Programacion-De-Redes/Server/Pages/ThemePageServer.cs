using System;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;

namespace Server
{
    public class ThemePageServer
    {
        public void ShowMenu(Socket SocketClient,SocketHandler socketHandler, MemoryRepository repository)
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
                        ShowThemeList(socketHandler);
                        break;
                    case "2":
                        ShowThemeWithMorePosts(socketHandler);
                        break;
                    case "3":
                        exit = true;
                        new HomePageServer().ShowMenu(SocketClient, socketHandler, repository);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private void ShowThemeList(SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }

        private void ShowThemeWithMorePosts(SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }
    }
}