using System;
using System.Collections.Generic;
using System.Net.Sockets;
using BusinessLogic;
using Domain;
using DataHandler;
using Domain.Services;

namespace Server
{
    public class PostPageServer
    {

        public void Menu(Socket socketClient,SocketHandler socketHandler)
        {
           
            var exit = false;
            string[] _options = {"Show theme post", "Show post", "Show file post", "Back"};
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
                        Console.Clear();
                       ShowThemePost(socketClient, socketHandler);
                        break;
                    case 2:
                        Console.Clear();
                        socketHandler.SendData(15,socketClient);
                        ShowPost(socketClient,socketHandler);
                        break;
                    case 3:
                        Console.Clear();
                        socketHandler.SendData(16,socketClient);
                        ShowFilePost(socketClient,socketHandler);
                        break;
                    case 4:
                        Console.Clear();
                        exit = true;
                        new HomePageServer().Menu(socketClient,socketHandler);
                        break;
                    default:
                        break;
                
            }
        }

       private void ShowThemePost(Socket socketClient, SocketHandler socketHandler)
        { 
            Console.Clear();
            var exit = false;
            string[] _options = {"By creation date", "By theme", "By both", "Back"};
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
                        socketHandler.SendData(14, socketClient);
                        ShowThemePostByCreationDate(socketClient, socketHandler);
                        break;
                    case 2:
                        socketHandler.SendData(20, socketClient);
                        ShowThemePostByTheme(socketClient, socketHandler);
                        break;
                    case 3:
                        socketHandler.SendData(21, socketClient);
                        ShowThemePostByDateAndTheme(socketClient, socketHandler);
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
       
       private static string ReceiveListPost(SocketHandler socketHandler,string message)
       {
           Console.ForegroundColor = ConsoleColor.DarkCyan;
           Console.WriteLine(message+"\n");
           Console.ForegroundColor = ConsoleColor.Black;
           string[] postsNAmes = socketHandler.ReceiveMessage();
           Console.WriteLine("----Menu----");
           for (var i = 0; i < postsNAmes.Length; i++)
           {
               var prefix =i;
               Console.ForegroundColor = ConsoleColor.White;
               Console.BackgroundColor = ConsoleColor.Black;
               Console.WriteLine($"{prefix}{postsNAmes[i]}");
           }
           var var = Console.ReadLine();
           int index= Int32.Parse(var);
           string optionSelect = postsNAmes[index - 1];
           return optionSelect;
       }
    
       private void ShowPost(Socket socketClient,SocketHandler socketHandler)
       {
           var optionSelect = ReceiveListPost(socketHandler,"Posts");
           if (optionSelect == "Back")
           {
               socketHandler.SendMessage(optionSelect);
               Menu(socketClient, socketHandler);
           }
           Menu(socketClient, socketHandler);
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
                Menu(socketClient, socketHandler);
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
            Menu(socketClient, socketHandler);
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
            Menu(socketClient, socketHandler);
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
            Menu(socketClient, socketHandler);
        }

    }
}