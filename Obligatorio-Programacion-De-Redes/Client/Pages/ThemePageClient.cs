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
            Console.WriteLine("----Menu----");
            for (var i = 0; i < _options.Length; i++)
            {
                var prefix =i;
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine($"{prefix}{_options[i]}");
            }
            var var = Console.ReadLine();
            int option= Int32.Parse(var);
            switch (option)
            {
                case 1:
                    socketHandler.SendData(5, SocketClient);
                    AddTheme(socketHandler,SocketClient);
                    break;
                case 2:
                    socketHandler.SendData(6, SocketClient);
                    ModifyTheme(socketHandler, SocketClient);
                    break;
                case 3:
                    socketHandler.SendData(7, SocketClient);
                    DeleteTheme(socketHandler, SocketClient);
                    break;
                case 4:
                    exit = true;
                    new HomePageClient().Menu(SocketClient, socketHandler);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;

            }
        }

        private void DeleteTheme(SocketHandler socketHandler, Socket socketClient)
        {
            string optionSelect= ReceiveListThemes(socketHandler,"Themes");
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
                Menu(socketClient, socketHandler);
            }
        }
        
        private static string ReceiveListThemes(SocketHandler socketHandler,string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(message+"\n");
            Console.ForegroundColor = ConsoleColor.Black;
            string[] postsNAmes = socketHandler.ReceiveMessage();
            Console.WriteLine("----Themes----");
            for (var i = 0; i < postsNAmes.Length; i++)
            {
                var prefix =i;
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine($"{prefix}{postsNAmes[i]}");
            }
            var var=Console.ReadLine();
            int index= Int32.Parse(var);
            string optionSelect = postsNAmes[index-1];
            return optionSelect;
        }

        private void ModifyTheme(SocketHandler socketHandler,Socket socketClient)
        {
            string optionSelected= ReceiveListThemes(socketHandler,"Themes");
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
                Menu(socketClient, socketHandler);
            }
        }

        public void AddTheme(SocketHandler socketHandler,Socket socketClient)
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
            Menu(socketClient, socketHandler);
        }

        
    }
}