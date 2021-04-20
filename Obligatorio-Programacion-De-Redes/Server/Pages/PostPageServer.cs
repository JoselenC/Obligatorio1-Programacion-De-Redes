using System;
using System.Collections.Generic;
using System.Net.Sockets;
using BusinessLogic;
using Client;
using Domain;
using DataHandler;
using Domain.Services;

namespace Server
{
    public class PostPageServer
    {

        public void Menu(Socket socketClient,SocketHandler socketHandler)
        {
            Console.Clear();
            var exit = false;
            string[] _options = {"Show theme post", "Show post", "Show file post", "Back"};
            while (!exit)
            {
                var option = new MenuServer().ShowMenu(_options,false);
                switch (option)
                {
                    case 1:
                       ShowThemePost(socketClient, socketHandler);
                        break;
                    case 2:
                        socketHandler.SendData(15,socketClient);
                        ShowPost(socketClient,socketHandler);
                        break;
                    case 3:
                        socketHandler.SendData(16,socketClient);
                        ShowFilePost(socketClient,socketHandler);
                        break;
                    case 4:
                        exit = true;
                        new HomePageServer().Menu(socketClient,socketHandler);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

       private void ShowThemePost(Socket socketClient, SocketHandler socketHandler)
        { 
            Console.Clear();
            var exit = false;
            string[] _options = {"By creation date", "By theme", "By both", "Back"};
            while (!exit)
            {
                var option = new MenuServer().ShowMenu(_options,false);
                switch (option)
                {
                    case 1:
                        socketHandler.SendData(14,socketClient);
                        ShowThemePostByCreationDate(socketClient,socketHandler);
                        break;
                    case 2:
                        socketHandler.SendData(20,socketClient);
                        ShowThemePostByTheme(socketClient,socketHandler);
                        break;
                    case 3:
                        socketHandler.SendData(21,socketClient);
                        ShowThemePostByDateAndTheme(socketClient,socketHandler);
                        break;
                    case 4:
                        exit = true;
                        new HomePageServer().Menu(socketClient, socketHandler);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
       
       private static string ReceiveListPost(SocketHandler socketHandler,string message)
       {
           Console.ForegroundColor = ConsoleColor.DarkCyan;
           Console.WriteLine(message+"\n");
           Console.ForegroundColor = ConsoleColor.Black;
           string[] postsNAmes = socketHandler.ReceiveMessage();
           int index = new MenuServer().ShowMenu(postsNAmes,false);
           string optionSelect = postsNAmes[index - 1];
           return optionSelect;
       }
       
       private static string ReceiveThemes(SocketHandler socketHandler,string message)
       {
           Console.ForegroundColor = ConsoleColor.DarkCyan;
           Console.WriteLine(message+"\n");
           Console.ForegroundColor = ConsoleColor.Black;
           string[] themesNames = socketHandler.ReceiveMessage();
           int indexThemes = new MenuServer().ShowMenu(themesNames,false);
           string optionSelectThemes = themesNames[indexThemes - 1];
           return optionSelectThemes;
       }

       private void ShowPost(Socket socketClient,SocketHandler socketHandler)
       {
           var optionSelect = ReceiveListPost(socketHandler,"Posts");
           if (optionSelect == "Back")
           {
               socketHandler.SendMessage(optionSelect);
               Menu(socketClient, socketHandler);
           }
       }

        public void ShowFilePost(Socket socketClient,SocketHandler socketHandler)
        {
            string title = "Select post";
            var optionSelect = ReceiveListPost(socketHandler,"File posts");
            if (optionSelect == "Back")
            {
                socketHandler.SendMessage(optionSelect);
                Menu(socketClient, socketHandler);
            }
            else
            {
                string message = optionSelect;
                socketHandler.SendMessage(message);
                string[] messageArray = socketHandler.ReceiveMessage();
                string name = messageArray[0];
                Console.WriteLine("name:" + name);
                string creationDate = messageArray[1];
                Console.WriteLine("Creation date:" + creationDate);
            }

        }
        
        private void ShowThemePostByCreationDate(Socket socketClient, SocketHandler socketHandler)
        {
            var optionSelect = ReceiveListPost(socketHandler,"Posts by creation date");
            if (optionSelect == "Back")
            {
                socketHandler.SendMessage(optionSelect);
                Menu(socketClient, socketHandler);
            }
        }

        public void ShowThemePostByTheme(Socket socketClient,SocketHandler socketHandler)
        {
            Console.WriteLine("Theme name to filter");
            string themeName = Console.ReadLine();
            socketHandler.SendMessage(themeName);
            var optionSelect = ReceiveListPost(socketHandler,"Posts by theme name");
            if (optionSelect == "Back")
            {
                socketHandler.SendMessage(optionSelect);
                Menu(socketClient, socketHandler);
            }
        }
        
        public void ShowThemePostByDateAndTheme(Socket socketClient,SocketHandler socketHandler)
        {
            Console.WriteLine("Theme name to filter");
            string themeName = Console.ReadLine();
            socketHandler.SendMessage(themeName);
            var optionSelect = ReceiveListPost(socketHandler,"Posts by theme name");
            if (optionSelect == "Back")
            {
                socketHandler.SendMessage(optionSelect);
                Menu(socketClient, socketHandler);
            }
        }

    }
}