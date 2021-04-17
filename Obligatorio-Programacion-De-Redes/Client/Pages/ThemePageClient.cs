using System;
using System.Net.Sockets;
using DataHandler;

namespace Client
{
    public class ThemePageClient
    {
        public void Menu(Socket SocketClient,SocketHandler socketHandler)
        {
            Console.Clear();
            var exit = false;
            string[] _options = {"Add theme", "Modify theme", "Delete theme","Back"};
            while (!exit)
            {
                var option = new MenuClient().ShowMenu(_options);
                switch (option)
                {
                    case 1:
                        socketHandler.SendData(5,SocketClient);
                        AddTheme(socketHandler);
                        break;
                    case 2:
                        socketHandler.SendData(6,SocketClient);
                        ModifyTheme(socketHandler);
                        break;
                    case 3:
                        socketHandler.SendData(7,SocketClient);
                        DeleteTheme(socketHandler);
                        break;
                    case 4:
                        exit = true;
                        new HomePageClient().Menu(SocketClient,socketHandler);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
        
        private void DeleteTheme(SocketHandler socketHandler)
        {
            Console.WriteLine("Name of the theme to delete"); 
            string name = Console.ReadLine();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(name);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
            string[] messageArray = socketHandler.ReceiveMessage();
            Console.WriteLine(messageArray[0]);
        }

        private void ModifyTheme(SocketHandler socketHandler)
        {
            Console.WriteLine("Name of the theme to modify");
            string oldName = Console.ReadLine();
            Console.WriteLine("New data theme:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            string message = oldName + "#" + name + "#" +description;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
            string[] messageArray = socketHandler.ReceiveMessage();
            Console.WriteLine(messageArray[0]);
            
        }

        public void AddTheme(SocketHandler socketHandler)
        {
            Console.Write("New theme ");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            string message = name + "#" + description;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
            string[] messageArray = socketHandler.ReceiveMessage();
            Console.WriteLine(messageArray[0]);
        }

        
    }
}