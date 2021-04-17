using System;
using System.Net.Sockets;
using BusinessLogic;
using Client;
using DataHandler;

namespace Server
{
    public class ThemePageServer
    {
        public void Menu(Socket SocketClient,SocketHandler socketHandler, MemoryRepository repository)
        {
            var exit = false;
            string[] _options = {"Add theme", "Modify theme", "Delete theme", "Back"};
            while (!exit)
            {
                var option = new MenuServer().ShowMenu(_options);
                switch (option)
                {
                    case 1:
                        ShowThemeList(socketHandler);
                        break;
                    case 2:
                        ShowThemeWithMorePosts(socketHandler);
                        break;
                    case 3:
                        exit = true;
                        new HomePageServer().Menu(SocketClient, socketHandler, repository);
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