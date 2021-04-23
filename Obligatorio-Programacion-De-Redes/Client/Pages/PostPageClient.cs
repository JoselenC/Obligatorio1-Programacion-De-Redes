﻿using System;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Domain;
using DataHandler;
using Protocol;

namespace Client
{
    public class PostPageClient
    {

        public void Menu(Socket socketClient,SocketHandler socketHandler,bool exit)
        {
            string[] _options = {"Add post", "Modify post", "Delete post", "Associate theme", "Disassociate theme","Back"};
            int option = new MenuClient().ShowMenu(_options, "Post menu");
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Packet packg1 = new Packet("REQ", "1", "Add post");
                        socketHandler.SendPackg(packg1);
                        AddPost(socketHandler, socketClient);
                        break;
                    case 2:
                        Console.Clear();
                        Packet packg2 = new Packet("REQ", "2", "Modify post");
                        socketHandler.SendPackg(packg2);
                        ModifyPost(socketHandler, socketClient);
                        break;
                    case 3:
                        Console.Clear();
                        Packet packg3 = new Packet("REQ", "3", "Delete post");
                        socketHandler.SendPackg(packg3);
                        DeletePost(socketHandler, socketClient);
                        break;
                    case 4:
                        Console.Clear();
                        Packet packg4 = new Packet("REQ", "4", "Associate theme");
                        socketHandler.SendPackg(packg4);
                        AsociateTheme(socketHandler, socketClient);
                        break;
                    case 5:
                        Console.Clear();
                        Packet packg11 = new Packet("REQ", "11", "Disassociate theme");
                        socketHandler.SendPackg(packg11);
                        DisassociateTheme(socketHandler, socketClient);
                        break;
                    case 6:
                        Console.Clear();
                        new HomePageClient().Menu(socketClient, socketHandler);
                        break;
                    default:
                        break;
                }
        }

       private void DeletePost(SocketHandler socketHandler,Socket socketClient)
       {
            string message = "Posts to delete";
            string optionSelect = ReceiveListPost(socketHandler,message);
            if (optionSelect == "Back")
            {
                Packet packg = new Packet("REQ", "3", optionSelect);
                socketHandler.SendPackg(packg);
                Menu(socketClient, socketHandler,false);
            }
            else
            {
                Packet packg = new Packet("REQ", "3", optionSelect);
                socketHandler.SendPackg(packg);
                var packet = socketHandler.ReceivePackg();
                string messageReceive = packet.Data;
                Console.WriteLine(messageReceive);
                Menu(socketClient, socketHandler,false);
            }
        }

        private void ModifyPost(SocketHandler socketHandler,Socket socketClient)
        {
            string title ="Posts to modify";
            string optionSelect = ReceiveListPost(socketHandler,title);
            if (optionSelect == "Back")
            {
                Packet packg = new Packet("REQ", "2", optionSelect);
                socketHandler.SendPackg(packg);
                Menu(socketClient, socketHandler,false);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("-----New data----- \n");
                Console.Write("Name: ");
                Console.ForegroundColor = ConsoleColor.Black;
                string name = Console.ReadLine();
                while (name == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("The name cannot be empty: \n");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Name: ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    name = Console.ReadLine();
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Creation date: ");
                Console.ForegroundColor = ConsoleColor.Black;
                string creationDate = Console.ReadLine();
                while (!GoodFormat(creationDate))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("The date format must be: \n" + "dd/mm/yyyy \n");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Creation date: ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    creationDate = Console.ReadLine();
                }
                string message = optionSelect + "#" + name + "#" + creationDate;
                Packet packg = new Packet("REQ", "2", message);
                socketHandler.SendPackg(packg);
                var packet = socketHandler.ReceivePackg();
                string messageReceive = packet.Data;
                Console.WriteLine(messageReceive);
                Menu(socketClient, socketHandler,false);
            }

        }

        private bool GoodFormat(string creationDate)
        {
            Regex regex = new Regex(@"\b\d{1,2}(/|-|.|\s)\d{1,2}(/|-|.|\s)(\d{4}|\d{2})");
            var match = regex.Match(creationDate);
            if (match.Success)
                return true;
            return false;
        } 

        public void AddPost(SocketHandler socketHandler,Socket socketClient)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("------New post------ \n");
            Console.Write("Name: ");
            Console.ForegroundColor = ConsoleColor.Black;
            string name = Console.ReadLine();
            while (name == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("The name cannot be empty: \n");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Name:  \n");
                Console.ForegroundColor = ConsoleColor.Black;
                name = Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Creation date: ");
            Console.ForegroundColor = ConsoleColor.Black;
            string creationDate = Console.ReadLine();
            while(!GoodFormat(creationDate))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("The date format must be: \n" +  "MM:dd:yyyy or dd/mm/yyyy \n");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Creation date: ");
                Console.ForegroundColor = ConsoleColor.Black;
                creationDate = Console.ReadLine();
            }
            string message = name + "#" + creationDate;
            Packet packg = new Packet("REQ", "1", message);
            socketHandler.SendPackg(packg);
            var packet = socketHandler.ReceivePackg();
            string messageReceive = packet.Data;
            Console.WriteLine(messageReceive);
            AssociateThemePost(socketHandler, socketClient, name);
            Menu(socketClient, socketHandler,false);
           
        }

        private void AssociateThemePost(SocketHandler socketHandler, Socket socketClient, string name)
        {
                  string[] _options = {"Add", "Back"};
                 int option = new MenuClient().ShowMenu(_options,"Add theme to post");
                switch (option)
                {
                    case 1:
                        Packet packg12 = new Packet("REQ", "12", "Associate theme post");
                        socketHandler.SendPackg(packg12);
                        AddThemeToPost(socketHandler, name,socketClient);
                        break;
                    case 2:
                        Menu(socketClient, socketHandler,false);
                        break;
                    default:
                        break;
                }
        }

        private void AddThemeToPost(SocketHandler socketHandler,string postName,Socket socketClient)
        {
            string title="Themes to add to the post";
            string optionSelect = ReceiveThemes(socketHandler,title);
            if (optionSelect == "Back")
            {
                Packet packg = new Packet("REQ", "2", optionSelect);
                socketHandler.SendPackg(packg);
            }
            else
            {
                string message = postName + "#" + optionSelect;
                Packet packg = new Packet("REQ", "2", message);
                socketHandler.SendPackg(packg);
               
            }
            var packet = socketHandler.ReceivePackg();
            string messageReceive = packet.Data;
            Console.WriteLine(messageReceive);
            AssociateThemePost(socketHandler,socketClient,postName);

        }
        
        private void DisassociateTheme(SocketHandler socketHandler, Socket socketClient)
        {
            string title="Select post to disassociate theme";
           var optionSelect = ReceiveListPost(socketHandler,title);
           Packet packg = new Packet("REQ", "11", optionSelect);
           socketHandler.SendPackg(packg);
            if (optionSelect == "Back")
            {
                Packet packg2 = new Packet("REQ", "11", optionSelect);
                socketHandler.SendPackg(packg2);
                Menu(socketClient, socketHandler,false);
            }
            else
            {
                string title2="Select theme to disassociate to the post";
                var optionSelectThemes = ReceiveThemes(socketHandler,title2);
                if (optionSelectThemes == "Back")
                {
                    Packet packg3 = new Packet("REQ", "11", optionSelectThemes);
                    socketHandler.SendPackg(packg3);
                    Menu(socketClient, socketHandler,false);
                }
                else
                {
                    string title3 = "select new theme to associate to the post";
                    var optionSelectThemes2 = ReceiveThemes(socketHandler, title3);
                    string message = optionSelect + "#" + optionSelectThemes + "#" + optionSelectThemes2;
                    Packet packg4 = new Packet("REQ", "11", message);
                    socketHandler.SendPackg(packg4);
                    var packet = socketHandler.ReceivePackg();
                    string messageReceive = packet.Data;
                    Console.WriteLine(messageReceive);
                    Menu(socketClient, socketHandler,false);
                }
                
            }
        }

        private string ReceiveThemes(SocketHandler socketHandler,string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("----"+message+"----\n");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            var packet = socketHandler.ReceivePackg();
            String[] themesNames= packet.Data.Split('#');
            int indexThemes = new MenuClient().ShowMenu(themesNames,"Themes");
           string optionSelectThemes = themesNames[indexThemes-1];
            return optionSelectThemes;
            
        }

        private string ReceiveListPost(SocketHandler socketHandler,string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("----"+message+"----\n");
            Console.ForegroundColor = ConsoleColor.Black;
            var packet = socketHandler.ReceivePackg();
            String[] postsNAmes = packet.Data.Split('#');
            int index = new MenuClient().ShowMenu(postsNAmes,"Posts");
            string optionSelect = postsNAmes[index-1];
            return optionSelect;
        }

        public void AsociateTheme(SocketHandler socketHandler,Socket SocketClient)
        {
            string title="Select post to associate theme";
            string optionSelect1 = ReceiveListPost(socketHandler,title);
            if (optionSelect1 == "Back")
            {
                Packet packg = new Packet("REQ", "4", optionSelect1);
                socketHandler.SendPackg(packg);
                new HomePageClient().Menu(SocketClient, socketHandler);
            }
            else
            {
                string title2="Select theme to associate the post";
                string optionSelect = ReceiveThemes(socketHandler,title2);
                if (optionSelect == "Back")
                {
                    Packet packg = new Packet("REQ", "4", optionSelect);
                    socketHandler.SendPackg(packg);
                    new HomePageClient().Menu(SocketClient, socketHandler);
                }
                else
                {
                    string message = optionSelect1 + "#" + optionSelect;
                    Packet packg = new Packet("REQ", "4", message);
                    socketHandler.SendPackg(packg);
                    var packet = socketHandler.ReceivePackg();
                    string messageReceive = packet.Data;
                    Console.WriteLine(messageReceive);
                    Menu(SocketClient, socketHandler,false);
                }
            }
        }

        public void SearchPost(SocketHandler socketHandler,Socket SocketClient)
        {
            string title="Select post";
            var optionSelect = ReceiveListPost(socketHandler,title);
            if (optionSelect == "Back")
            {
                Packet packg = new Packet("REQ", "2", optionSelect);
                socketHandler.SendPackg(packg);
                Menu(SocketClient, socketHandler,false);
            }
            else
            {
                Packet packg = new Packet("REQ", "2", optionSelect);
                socketHandler.SendPackg(packg);
                var packet = socketHandler.ReceivePackg();
                String[] messageArray = packet.Data.Split('#');
                string name = messageArray[0];
                Console.WriteLine("Name:" + name);
                string creationDate = messageArray[1];
                Console.WriteLine("Creation date:" + creationDate);
                Menu(SocketClient, socketHandler,false);
            }
        }
    }
}