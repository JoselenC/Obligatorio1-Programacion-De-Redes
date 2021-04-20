using System;
using System.Dynamic;
using System.Net.Sockets;
using BusinessLogic;
using Protocol;
using DataHandler;

namespace Client
{
    public class HomePageClient
    {
        
        public void Menu(Socket SocketClient,SocketHandler socketHandler)
        {
           var exit = false;
           string[] _options = {"Posts", "Themes", "Files", "Search post","Exit"};
            while (!exit)
            {
                Console.WriteLine("----Menu----");
                var option = new MenuClient().ShowMenu(_options,exit);
                if (option == 0)
                    exit = true;
                else
                {
                    switch (option)
                    {
                        case 1:
                            new PostPageClient().Menu(SocketClient, socketHandler, exit);
                            break;
                        case 2:
                            new ThemePageClient().Menu(SocketClient, socketHandler);
                            break;
                        case 3:
                            new FilePageClient().UploadFile(socketHandler, SocketClient);
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
            }
        }
        private static byte[] ConvertDataToHeader(short command, int data)
        {
            return HeaderHandler.EncodeHeader(command, data);
        }

        private static void SendData(short command,Socket SocketClient)
        {
            if (SocketClient.Send(ConvertDataToHeader(command, new Random().Next())) == 0)
            {
                throw new SocketException();
            }
        }
    }
}