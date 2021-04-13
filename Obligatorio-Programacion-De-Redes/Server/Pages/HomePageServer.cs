using System;
using System.Net.Sockets;
using BusinessLogic;
using Protocol;
using DataHandler;

namespace Server
{
    public class HomePageServer
    {
        
        public void ShowMenu(Socket SocketClient, SocketHandler socketHandler, MemoryRepository repository)
        {
            Console.Clear();
            Console.WriteLine("----Menu----");
            Console.WriteLine("1-Client list");
            Console.WriteLine("2-Posts");
            Console.WriteLine("3-Themes");
            Console.WriteLine("4-File");
            Console.WriteLine("5-Exit");
            var exit = false;
            while (!exit)
            {
                var option = Console.ReadLine();
                switch (option)
                {
                    case "uno":
                        SendData(1,SocketClient);
                        new ClientPageServer().ShowClientList(socketHandler);
                        break;
                    case "dos":
                        SendData(1,SocketClient);
                        new PostPageServer().ShowMenu(SocketClient, socketHandler, repository);
                        break;
                    case "tres":
                        SendData(2,SocketClient);
                        new ThemePageServer().ShowMenu(SocketClient, socketHandler, repository);
                        break;
                    case "cuatro":
                        SendData(3,SocketClient);
                        new FilePageServer().ShowFileList(socketHandler);
                        break;
                    case "Cinco":
                        SendData(6,SocketClient);
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