using System;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;
using Domain;
using Server;

namespace ClientHandler
{
    public class ThemePageServer
    {
        public void Menu(MemoryRepository repository,Socket SocketClient,SocketHandler socketHandler)
        {
            var exit = false;
            string[] _options = {"Add theme", "Modify theme", "Delete theme", "Back"};
            while (!exit)
            {
                int option = new MenuServer().ShowMenu(_options,"Menu");
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        ShowThemeList(repository,socketHandler,SocketClient);
                        break;
                    case 2:
                        Console.Clear();
                        ShowThemeWithMorePosts(repository,socketHandler,SocketClient);
                        break;
                    case 3:
                        Console.Clear();
                        exit = true;
                        new HomePageServer().Menu(repository,SocketClient, socketHandler);
                        break;
                    default:
                       break;
                }
            }
        }
        
        private static string ListThemes(MemoryRepository repository,string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----"+ title +"----");
            for (var i = 0; i < repository.Themes.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                var prefix = "Theme" +i +1 + ":  ";
                Console.BackgroundColor = ConsoleColor.Black;
               Console.WriteLine(prefix + "Name:" + repository.Themes[i].Name + "Description:" + repository.Themes[i].Description);
            }
            Console.WriteLine(repository.Themes.Count+1 +".  Back");
            var var=Console.ReadLine();
            int indexThemes= Int32.Parse(var);
            if (indexThemes > repository.Themes.Count)
            {
                return "Back";
                
            }
            else
            {
                string optionSelectThemes = repository.Themes[indexThemes - 1].Name;
                return optionSelectThemes;
            }
        }

        private void ShowThemeList(MemoryRepository repository,SocketHandler socketHandler,Socket socketClient)
        {
            string optionSelected = ListThemes(repository,"Themes");
            if (optionSelected == "Back")
            {
                Menu(repository,socketClient, socketHandler);
            }
            Menu(repository,socketClient, socketHandler);
        }

        private void ShowThemeWithMorePosts(MemoryRepository repository, SocketHandler socketHandler,
            Socket socketClient)
        {
            int max = Int32.MinValue;
            string themeName = "";
            foreach (var theme in repository.Themes)
            {
                if (theme.Posts.Count > max)
                {
                    max = theme.Posts.Count;
                    themeName = theme.Name;
                }
            }

            Theme themeMax = repository.Themes.Find(x => x.Name == themeName);
            Console.WriteLine("Name:" + themeMax.Name + "Description:" +
                              themeMax.Description);
            Menu(repository, socketClient, socketHandler);

        }
    }
}