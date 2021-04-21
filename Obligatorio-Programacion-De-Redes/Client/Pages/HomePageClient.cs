using System;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic;
using ProtocolString;
using DataHandler;

namespace Client
{
    public class HomePageClient
    {
       public void Menu(Socket SocketClient, SocketHandler socketHandler)
        {
                var exit = false;
                string[] _options = {"Posts", "Themes", "Files", "Search post", "Exit"};

                Console.WriteLine("----Menu----");
                for (var i = 0; i < _options.Length; i++)
                {
                    var prefix = i + 1;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"{prefix}{_options[i]}");
                }

                string var = "0";
                var = Console.ReadLine();
                int option = Int32.Parse(var);
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