using System;
using System.Net.Sockets;
using BusinessLogic;
using Client;
using DataHandler;

namespace Server
{
    public class ThemePageServer
    {
        public void Menu(Socket SocketClient,SocketHandler socketHandler)
        {
            var exit = false;
            string[] _options = {"Add theme", "Modify theme", "Delete theme", "Back"};
            while (!exit)
            {
                var option = new MenuServer().ShowMenu(_options,exit);
                switch (option)
                {
                    case 1:
                        socketHandler.SendData(17,SocketClient);
                        ShowThemeList(socketHandler);
                        break;
                    case 2:
                        socketHandler.SendData(18,SocketClient);
                        ShowThemeWithMorePosts(socketHandler);
                        break;
                    case 3:
                        exit = true;
                        new HomePageServer().Menu(SocketClient, socketHandler);
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