using System;
using System.Collections.Generic;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;
using Domain;

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
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("----Menu----");
                Console.ForegroundColor = ConsoleColor.Black;
                var option = new MenuClient().ShowMenu(_options);
                switch (option)
                {
                    case 1:
                        socketHandler.SendData(5,SocketClient);
                        AddTheme(socketHandler);
                        break;
                    case 2:
                        socketHandler.SendData(6,SocketClient);
                        ModifyTheme(socketHandler,SocketClient);
                        break;
                    case 3:
                        socketHandler.SendData(7,SocketClient);
                        DeleteTheme(socketHandler,SocketClient);
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

        private void DeleteTheme(SocketHandler socketHandler, Socket socketClient)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Delete theme");
            Console.ForegroundColor = ConsoleColor.Black;
            string[] postsNAmes = socketHandler.ReceiveMessage();
            int index = new MenuClient().ShowMenu(postsNAmes);
            string optionSelect = postsNAmes[index - 1];
            if (optionSelect == "Back")
            {
                socketHandler.SendMessage(optionSelect);
                Menu(socketClient, socketHandler);
            }
            else
            {
                socketHandler.SendMessage(optionSelect);
                string[] messageArray = socketHandler.ReceiveMessage();
                Console.WriteLine(messageArray[0]);
            }
        }

        private void ModifyTheme(SocketHandler socketHandler,Socket socketClient)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Modify theme\n");
            string[] postsNames = socketHandler.ReceiveMessage();
            int index=new MenuClient().ShowMenu(postsNames);
            string optionSelected = postsNames[index-1];
            if (optionSelected == "Back")
            {
                socketHandler.SendMessage(optionSelected);
                Menu(socketClient, socketHandler);
            }
            else
            {
                Console.WriteLine("New data theme\n");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Name: ");
                Console.ForegroundColor = ConsoleColor.Black;
                string name = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Description: ");
                Console.ForegroundColor = ConsoleColor.Black;
                string description = Console.ReadLine();
                string message = optionSelected + "#" + name + "#" + description;
                socketHandler.SendMessage(message);
                string[] messageArray = socketHandler.ReceiveMessage();
                Console.WriteLine(messageArray[0]);
            }
        }

        public void AddTheme(SocketHandler socketHandler)
        {
            Console.Write("New theme \n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Name: ");
            Console.ForegroundColor = ConsoleColor.Black;
            string name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Description: ");
            Console.ForegroundColor = ConsoleColor.Black;
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