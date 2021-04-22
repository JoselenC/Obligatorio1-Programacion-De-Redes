using System;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;

namespace ClientHandler
{
    public class FilePageServer
    {
        
        public void ShowFileList(MemoryRepository repository,SocketHandler socketHandler,Socket socketClient)
        {
           
            Console.Clear();
            var exit = false;
            string[] _options = {"All files", "By theme", "Order by creation date", "Order by name", "Order by size", "Back"};
            Console.WriteLine("----Select filter----");
            for (var i = 0; i < _options.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                var prefix =i + 1 + ".  ";
                Console.WriteLine($"{prefix}{_options[i]}");
            }
            Console.ForegroundColor = ConsoleColor.Black;
            var var = Console.ReadLine();
            int option= Int32.Parse(var);
            switch (option)
            {
                case 1:
                    ShowAllFiles(socketClient, socketHandler);
                    break;
                case 2:
                    ShowTFileByTheme(socketClient, socketHandler);
                    break;
                case 3:
                    ShowFileByDate(socketClient, socketHandler);
                    break;
                case 4:
                    ShowFileByName(socketClient, socketHandler);
                    break;
                case 5:
                    ShowFileBySize(socketClient, socketHandler);
                    break;
                case 6:
                    exit = true;
                    new HomePageServer().Menu(repository,socketClient, socketHandler);
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