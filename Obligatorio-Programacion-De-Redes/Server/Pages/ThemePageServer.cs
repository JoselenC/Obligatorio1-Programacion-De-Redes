using System;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;

namespace Server
{
    public class ThemePageServer
    {
        public void Menu(Socket SocketClient,SocketHandler socketHandler)
        {
            var exit = false;
            string[] _options = {"Add theme", "Modify theme", "Delete theme", "Back"};
            while (!exit)
            {
                Console.WriteLine("----Menu----");
                for (var i = 0; i < _options.Length; i++)
                {
                    var prefix =i;
                    Console.WriteLine($"{prefix}{_options[i]}");
                }
                var var = Console.ReadLine();
                int option= Int32.Parse(var);
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        socketHandler.SendData(17,SocketClient);
                        ShowThemeList(socketHandler,SocketClient);
                        break;
                    case 2:
                        Console.Clear();
                        socketHandler.SendData(18,SocketClient);
                        ShowThemeWithMorePosts(socketHandler,SocketClient);
                        break;
                    case 3:
                        Console.Clear();
                        exit = true;
                        new HomePageServer().Menu(SocketClient, socketHandler);
                        break;
                    default:
                       break;
                }
            }
        }
        
        private static string ReceiveThemes(SocketHandler socketHandler,string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(message+"\n");
            Console.ForegroundColor = ConsoleColor.Black;
            string[] themesNames = socketHandler.ReceiveMessage();
            Console.WriteLine("----Themes----");
            for (var i = 0; i < themesNames.Length; i++)
            {
                var prefix =i;
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine($"{prefix}{themesNames[i]}");
            }
            var var=Console.ReadLine();
            int indexThemes= Int32.Parse(var);
            string optionSelectThemes = themesNames[indexThemes-1];
            return optionSelectThemes;
            
        }

        private void ShowThemeList(SocketHandler socketHandler,Socket socketClient)
        {
            string optionSelected = ReceiveThemes(socketHandler,"Themes");
            if (optionSelected == "Back")
            {
                Menu(socketClient, socketHandler);
            }
        }

        private void ShowThemeWithMorePosts(SocketHandler socketHandler,Socket socketClient)
        {
            string optionSelected = ReceiveThemes(socketHandler,"Themes");
            if (optionSelected == "Back")
            {
                Menu(socketClient, socketHandler);
            }
        }
    }
}