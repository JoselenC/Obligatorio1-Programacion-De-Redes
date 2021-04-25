using System;
using System.Collections.Generic;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;
using Domain;
using Protocol;

namespace Client
{
    public class ThemePageClient
    {
        public void Menu(Socket SocketClient,SocketHandler socketHandler)
        {
            string[] _options = {"Add theme", "Modify theme", "Delete theme","Back"};
            int option = new MenuClient().ShowMenu(_options,"Theme menu");
            switch (option)
            {
                case 1:
                    Packet packg1 = new Packet("REQ", "5", "Add post");
                    socketHandler.SendPackg(packg1);
                    AddTheme(socketHandler,SocketClient);
                    break;
                case 2:
                    Packet packg6 = new Packet("REQ", "6", "Modify theme");
                    socketHandler.SendPackg(packg6);
                    ModifyTheme(socketHandler, SocketClient);
                    break;
                case 3:
                    Packet packg7 = new Packet("REQ", "7", "Delete theme");
                    socketHandler.SendPackg(packg7);
                    DeleteTheme(socketHandler, SocketClient);
                    break;
                case 4:
                    new HomePageClient().Menu(SocketClient, socketHandler);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;

            }
        }

        private void DeleteTheme(SocketHandler socketHandler, Socket socketClient)
        {
            string optionSelect= ReceiveListThemes(socketHandler,"Themes").Split('\0')[0]; ;
            if (optionSelect == "Back")
            {
                Packet packg = new Packet("REQ", "4", optionSelect);
                socketHandler.SendPackg(packg);
                Menu(socketClient, socketHandler);
            }
            else
            {
                Packet packg = new Packet("REQ", "4", optionSelect);
                socketHandler.SendPackg(packg);
                var packet = socketHandler.ReceivePackg();
                Console.WriteLine(packet.Data.Split('\0')[0]);
                Menu(socketClient, socketHandler);
            }
        }
        
        private string ReceiveListThemes(SocketHandler socketHandler,string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message+"\n");
            Console.ForegroundColor = ConsoleColor.White;
            var packet = socketHandler.ReceivePackg();
            String[] postsNAmes= packet.Data.Split('#');
            int index = new MenuClient().ShowMenu(postsNAmes,"Themes");
            string optionSelect = postsNAmes[index-1];
            return optionSelect;
        }

        private void ModifyTheme(SocketHandler socketHandler,Socket socketClient)
        {
            string optionSelected= ReceiveListThemes(socketHandler,"Themes").Split('\0')[0]; ;
            if (optionSelected == "Back")
            {
                Packet packg = new Packet("REQ", "4", optionSelected);
                socketHandler.SendPackg(packg);
                Menu(socketClient, socketHandler);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("-----New data theme-----\n");
                Console.Write("Name: ");
                Console.ForegroundColor = ConsoleColor.White;
                string name = Console.ReadLine();
                while (name == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("The name cannot be empty: \n");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Name:  ");
                    Console.ForegroundColor = ConsoleColor.White;
                    name = Console.ReadLine();
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Description: ");
                Console.ForegroundColor = ConsoleColor.White;
                string description = Console.ReadLine();
                string message = optionSelected + "#" + name + "#" + description;
                Packet packg = new Packet("REQ", "4", message);
                socketHandler.SendPackg(packg);
                var packet = socketHandler.ReceivePackg();
                Console.WriteLine(packet.Data.Split('\0')[0]);
                Menu(socketClient, socketHandler);
            }
        }

        public void AddTheme(SocketHandler socketHandler,Socket socketClient)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("-----New theme----- \n");
            Console.Write("Name: ");
            Console.ForegroundColor = ConsoleColor.White;
            string name = Console.ReadLine();
            while (name == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("The name cannot be empty: \n");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Name:  \n");
                Console.ForegroundColor = ConsoleColor.White;
                name = Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Description: ");
            Console.ForegroundColor = ConsoleColor.White;
            string description = Console.ReadLine();
            string message = name + "#" + description;
            Packet packg = new Packet("REQ", "4", message);
            socketHandler.SendPackg(packg);
            var packet = socketHandler.ReceivePackg();
            Console.WriteLine(packet.Data.Split('\0')[0]);
            Menu(socketClient, socketHandler);
        }

        
    }
}