using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Domain;
using DataHandler;

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
               /* while(!GoodFormat(creationDate))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("The date format must be: \n" +  "dd/mm/yyyy \n");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Creation date: ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    name = Console.ReadLine();
                }*/

                string message = optionSelect + "#" + name + "#" + creationDate;
                socketHandler.SendMessage(message);
                string[] messageArray = socketHandler.ReceiveMessage();
                Console.WriteLine(messageArray[0]);
                Menu(socketClient, socketHandler,false);
            }

        }

        private bool GoodFormat(string creationDate)
        {
            Regex regex = new Regex(@"\b\d{1,2}(/|-|.|\s)\d{1,2}(/|-|.|\s)(\d{4}|\d{2})");
             var match = regex.Match(creationDate);
                if(match.Success)
                return true;
                return true;        
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
                name = Console.ReadLine();
            }
            string message = name + "#" + creationDate;
            socketHandler.SendMessage(message);
            var exit = false;
           
            string[] messageArray = socketHandler.ReceiveMessage();
            Console.WriteLine(messageArray[0]);
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
                        socketHandler.SendData(12, socketClient);
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
                socketHandler.SendMessage(optionSelect);
                AssociateThemePost(socketHandler,socketClient,postName);
            }
            else
            {
                string message = postName + "#" + optionSelect;
                socketHandler.SendMessage(message);
                string[] messageArray = socketHandler.ReceiveMessage();
                Console.WriteLine(messageArray[0]);
                AssociateThemePost(socketHandler,socketClient,postName);
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
                    string title3 = "select new theme to associate to the post";
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
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message+"\n");
            Console.ForegroundColor = ConsoleColor.Black;
            string[] themesNames = socketHandler.ReceiveMessage();
            int indexThemes = new MenuClient().ShowMenu(themesNames,"Themes");
           string optionSelectThemes = themesNames[indexThemes-1];
            return optionSelectThemes;
            
        }

        private static string ReceiveListPost(SocketHandler socketHandler,string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message+"\n");
            Console.ForegroundColor = ConsoleColor.Black;
            string[] postsNAmes = socketHandler.ReceiveMessage();
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
                Console.WriteLine("Name:" + name);
                string creationDate = messageArray[1];
                Console.WriteLine("Creation date:" + creationDate);
                Menu(SocketClient, socketHandler,false);
            }
        }
    }
}