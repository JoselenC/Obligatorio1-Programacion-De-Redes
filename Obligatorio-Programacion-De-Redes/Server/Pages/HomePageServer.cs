using System;
using System.Net.Sockets;
using ProtocolString;
using DataHandler;

namespace Server
{
    public class HomePageServer
    {
        
        public void Menu(Socket SocketClient, SocketHandler socketHandler)
        {
            var exit = false;
            string[] _options = {"Client list", "Posts", "Themes", "File", "Exit"};
           Console.WriteLine("----Menu----");
           for (var i = 0; i < _options.Length; i++)
           {
               var prefix =i;
               Console.ForegroundColor = ConsoleColor.White;
               Console.BackgroundColor = ConsoleColor.Black;
               Console.WriteLine($"{prefix}{_options[i]}");
           }

           var var=Console.ReadLine();
           int option= Int32.Parse(var);

           switch (option)
           {
               case 1:
                   SendData(13, SocketClient);
                   new ClientPageServer().ShowClientList(socketHandler, SocketClient);
                   break;
               case 2:
                   new PostPageServer().Menu(SocketClient, socketHandler);
                   break;
               case 3:
                   SendData(3, SocketClient);
                   new ThemePageServer().Menu(SocketClient, socketHandler);
                   break;
               case 4:
                   SendData(19, SocketClient);
                   new FilePageServer().ShowFileList(socketHandler,SocketClient);
                   break;
               case 5:
                   exit = true;
                   SocketClient.Shutdown(SocketShutdown.Both);
                   SocketClient.Close();
                   break;
               default:
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