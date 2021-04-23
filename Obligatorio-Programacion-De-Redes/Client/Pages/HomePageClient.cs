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
                string[] _options = {"Posts", "Themes", "Associate file", "Search post", "Exit"};

                int option=new MenuClient().ShowMenu(_options,"Menu");
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
                        SendData(9, SocketClient);
                        new PostPageClient().SearchPost(socketHandler, SocketClient);
                        break;
                    case 5:
                        SendData(6, SocketClient);
                        exit = true;
                        SocketClient.Shutdown(SocketShutdown.Both);
                        SocketClient.Close();
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
        }

        private byte[] ConvertDataToHeader(short command, int data)
        {
            return HeaderHandler.EncodeHeader(command, data);
        }

        private void SendData(short command,Socket SocketClient)
        {
            if (SocketClient.Send(ConvertDataToHeader(command, new Random().Next())) == 0)
            {
                throw new SocketException();
            }
        }
    }
}