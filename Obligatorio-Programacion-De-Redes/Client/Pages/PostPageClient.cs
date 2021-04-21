using System;
using System.Net.Sockets;
using Domain;
using DataHandler;

namespace Client
{
    public class PostPageClient
    {

        public void Menu(Socket socketClient,SocketHandler socketHandler,bool exit)
        {
            string[] _options = {"Add post", "Modify post", "Delete post", "Associate theme", "Disassociate theme","Back"};
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
                        socketHandler.SendData(1, socketClient);
                        AddPost(socketHandler, socketClient);
                        break;
                    case 2:
                        Console.Clear();
                        socketHandler.SendData(2, socketClient);
                        ModifyPost(socketHandler, socketClient);
                        break;
                    case 3:
                        Console.Clear();
                        socketHandler.SendData(3, socketClient);
                        DeletePost(socketHandler, socketClient);
                        break;
                    case 4:
                        Console.Clear();
                        socketHandler.SendData(4, socketClient);
                        AsociateTheme(socketHandler, socketClient);
                        break;
                    case 5:
                        Console.Clear();
                        socketHandler.SendData(11, socketClient);
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
                socketHandler.SendMessage(optionSelect);
                Menu(socketClient, socketHandler,false);
            }
            else
            {
                socketHandler.SendMessage(optionSelect);
                string[] messageArray = socketHandler.ReceiveMessage();
                Console.WriteLine(messageArray[0]);
                Menu(socketClient, socketHandler,false);
            }
        }

        private void ModifyPost(SocketHandler socketHandler,Socket socketClient)
        {
            string title ="Posts to modify";
            string optionSelect = ReceiveListPost(socketHandler,title);
            if (optionSelect == "Back")
            {
                socketHandler.SendMessage(optionSelect);
                Menu(socketClient, socketHandler,false);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Name: ");
                Console.ForegroundColor = ConsoleColor.Black;
                string name = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Creation date: ");
                Console.ForegroundColor = ConsoleColor.Black;
                string creationDate = Console.ReadLine();
                string message = optionSelect + "#" + name + "#" + creationDate;
                socketHandler.SendMessage(message);
                string[] messageArray = socketHandler.ReceiveMessage();
                Console.WriteLine(messageArray[0]);
                Menu(socketClient, socketHandler,false);
            }

        }

        public void AddPost(SocketHandler socketHandler,Socket socketClient)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("New post \n");
            Console.Write("Name: ");
            Console.ForegroundColor = ConsoleColor.Black;
            string name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Creation date: ");
            Console.ForegroundColor = ConsoleColor.Black;
            string creationDate = Console.ReadLine();
            string message = name + "#" + creationDate;
            socketHandler.SendMessage(message);
            var exit = false;
            string[] _options = {"Add theme to post", "Back"};
            string[] messageArray = socketHandler.ReceiveMessage();
            Console.WriteLine(messageArray[0]);
            AssociateThemePost(socketHandler, socketClient, exit, _options, name);
            Menu(socketClient, socketHandler,false);
           
        }

        private void AssociateThemePost(SocketHandler socketHandler, Socket socketClient, bool exit, string[] _options,
            string name)
        {
                Console.WriteLine("----Menu----");
                for (var i = 0; i < _options.Length; i++)
                {
                    var prefix =i+1;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"{prefix}{_options[i]}");
                }
                var var = Console.ReadLine();
                int option= Int32.Parse(var);
                switch (option)
                {
                    case 1:
                        socketHandler.SendData(12, socketClient);
                        AddThemeToPost(socketHandler, name,socketClient);
                        break;
                    case 2:
                        exit = true;
                        Menu(socketClient, socketHandler,false);
                        break;
                
            }
        }

        private void AddThemeToPost(SocketHandler socketHandler,string postName,Socket socketClient)
        {
            string title="Themes to add to the post";
            string optionSelect = ReceiveThemes(socketHandler,title);
            if (optionSelect == "Back")
            {
                socketHandler.SendMessage(optionSelect);
                Menu(socketClient, socketHandler,false);
            }
            else
            {
                string message = postName + "#" + optionSelect;
                socketHandler.SendMessage(message);
                string[] messageArray = socketHandler.ReceiveMessage();
                Console.WriteLine(messageArray[0]);
                Menu(socketClient, socketHandler,false);
            }

        }
        
        private void DisassociateTheme(SocketHandler socketHandler, Socket socketClient)
        {
            string title="Select post to disassociate the theme";
           var optionSelect = ReceiveListPost(socketHandler,title);
            socketHandler.SendMessage(optionSelect);
            if (optionSelect == "Back")
            {
                socketHandler.SendMessage(optionSelect);
                Menu(socketClient, socketHandler,false);
            }
            else
            {
                string title2="Select theme to disassociate to the post";
                var optionSelectThemes = ReceiveThemes(socketHandler,title2);
                if (optionSelectThemes == "Back")
                {
                    socketHandler.SendMessage(optionSelectThemes);
                    Menu(socketClient, socketHandler,false);
                }
                else
                {
                    string title3 = "select theme to associate to the post";
                    var optionSelectThemes2 = ReceiveThemes(socketHandler, title3);
                    string message = optionSelect + "#" + optionSelectThemes + "#" + optionSelectThemes2;
                    socketHandler.SendMessage(message);
                    string[] messageArray = socketHandler.ReceiveMessage();
                    Console.WriteLine(messageArray[0]);
                    Menu(socketClient, socketHandler,false);
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

        private static string ReceiveListPost(SocketHandler socketHandler,string message)
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

        public void AsociateTheme(SocketHandler socketHandler,Socket SocketClient)
        {
            string title="Select post to associate theme";
            string optionSelect1 = ReceiveListPost(socketHandler,title);
            if (optionSelect1 == "Back")
            {
                socketHandler.SendMessage(optionSelect1);
                new HomePageClient().Menu(SocketClient, socketHandler);
            }
            else
            {
                string title2="Select theme to associate the post";
                string optionSelect = ReceiveThemes(socketHandler,title2);
                if (optionSelect == "Back")
                {
                    socketHandler.SendMessage(optionSelect);
                    new HomePageClient().Menu(SocketClient, socketHandler);
                }
                else
                {
                    string message = optionSelect1 + "#" + optionSelect;
                    socketHandler.SendMessage(message);
                    string[] messageArray = socketHandler.ReceiveMessage();
                    Console.WriteLine(messageArray[0]);
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
                socketHandler.SendMessage(optionSelect);
                Menu(SocketClient, socketHandler,false);
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
                Menu(SocketClient, socketHandler,false);
            }
        }
    }
}