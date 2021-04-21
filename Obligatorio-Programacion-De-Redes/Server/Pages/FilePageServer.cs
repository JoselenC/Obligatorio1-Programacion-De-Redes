using System;
using System.Net.Sockets;
using DataHandler;

namespace Server
{
    public class FilePageServer
    {
        
        public void ShowFileList(SocketHandler socketHandler,Socket socketClient)
        {
           
            Console.Clear();
            var exit = false;
            string[] _options = {"All files", "By theme", "Order by creation date", "Order by name", "Order by size", "Back"};
            Console.WriteLine("----Select filter----");
            for (var i = 0; i < _options.Length; i++)
            {
                var prefix =i;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"{prefix}{_options[i]}");
            }
            Console.ForegroundColor = ConsoleColor.Black;
            var var = Console.ReadLine();
            int option= Int32.Parse(var);
            switch (option)
            {
                case 1:
                    socketHandler.SendData(14, socketClient);
                    ShowAllFiles(socketClient, socketHandler);
                    break;
                case 2:
                    socketHandler.SendData(20, socketClient);
                    ShowTFileByTheme(socketClient, socketHandler);
                    break;
                case 3:
                    socketHandler.SendData(21, socketClient);
                    ShowFileByDate(socketClient, socketHandler);
                    break;
                case 4:
                    socketHandler.SendData(20, socketClient);
                    ShowFileByName(socketClient, socketHandler);
                    break;
                case 5:
                    socketHandler.SendData(21, socketClient);
                    ShowFileBySize(socketClient, socketHandler);
                    break;
                case 6:
                    exit = true;
                    new HomePageServer().Menu(socketClient, socketHandler);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        private void ShowFileBySize(Socket socketClient, SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }

        private void ShowFileByName(Socket socketClient, SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }

        private void ShowFileByDate(Socket socketClient, SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }

        private void ShowTFileByTheme(Socket socketClient, SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }

        private void ShowAllFiles(Socket socketClient, SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }
    }
}