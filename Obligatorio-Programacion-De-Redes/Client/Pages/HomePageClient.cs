using System;
using System.Net.Sockets;
using DataHandler;
using Protocol;
using SocketHandler = DataHandler.SocketHandler;

namespace Client
{
    public class HomePageClient
    {
        public void Menu(Socket SocketClient, SocketHandler socketHandler)
        {
            var exit = false;
            string[] _options = { "Posts", "Themes", "Associate file", "Search post", "Exit" };

            int option = new MenuClient().ShowMenu(_options, "Menu");
            switch (option)
            {
                case 1:
                    new PostPageClient().Menu(SocketClient, socketHandler, exit);
                    break;
                case 2:
                    new ThemePageClient().Menu(SocketClient, socketHandler);
                    break;
                case 3:
                    new FilePageClient().AssociateFile(socketHandler, SocketClient);
                    break;
                case 4:
                    Console.Clear();
                    Packet packg1 = new Packet("REQ", "9", "Search post");
                    socketHandler.SendPackg(packg1);
                    new PostPageClient().SearchPost(socketHandler, SocketClient);
                    break;
                case 5:
                    Console.Clear();
                    Packet packg2 = new Packet("REQ", "6", "Add post");
                    socketHandler.SendPackg(packg2);
                    exit = true;
                    SocketClient.Shutdown(SocketShutdown.Both);
                    SocketClient.Close();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
}