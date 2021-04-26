using System;
using System.Collections.Generic;
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
            string[] _options = {"Themes list", "Theme with more post", "Back"};
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

        private static string ListThemes(MemoryRepository repository, string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----" + title + "----");
            for (var i = 0; i < repository.Themes.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine((i+1) + ".  Theme" + (i + 1) + ":  ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Name: " + repository.Themes[i].Name + " Description: " +
                                  repository.Themes[i].Description);
                Console.WriteLine("Posts: ");
                if (repository.Themes[i].Posts != null)
                {
                    List<Post> posts = repository.Themes[i].Posts;
                    foreach (var post in posts)
                    {
                        Console.WriteLine("Name: " + post.Name);
                    }

                }
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(repository.Themes.Count + 1 + ".  Back");
            Console.ForegroundColor = ConsoleColor.White;
            var var = Console.ReadLine();
            int indexThemes = Int32.Parse(var);
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

        private void ShowThemeWithMorePosts(MemoryRepository repository, SocketHandler socketHandler,Socket socketClient)
        {
            int max = 0;
            List<string> themeNames = new List<string>();
            int cant = 0;
            if (repository.Themes != null && repository.Themes.Count!=0)
            {
                foreach (var theme in repository.Themes)
                {
                    if (theme.Posts == null)
                    {
                        cant = 0;
                    }
                    else
                    {
                        cant = theme.Posts.Count;
                    }

                    if (cant == max)
                    {
                        themeNames.Add(theme.Name);
                    }
                    else if (cant > max)
                    {
                        max = cant;
                        themeNames = new List<string>();
                        themeNames.Add(theme.Name);
                    }
                }

                Console.WriteLine("Cantidad de post: " + max);
                foreach (var themeName in themeNames)
                {
                    Theme themeMax = repository.Themes.Find(x => x.Name == themeName);
                    Console.WriteLine("Name:" + themeMax.Name + "Description:" +
                                      themeMax.Description);
                }
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine( "2.  Back");
                Console.ForegroundColor = ConsoleColor.White;
                var var = Console.ReadLine();
                int indexThemes = Int32.Parse(var);
                if (indexThemes > repository.Themes.Count)
                {
                    Menu(repository,socketClient, socketHandler);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There aren't themes in the system");
                Console.ForegroundColor = ConsoleColor.White;
                Menu(repository, socketClient, socketHandler);
            }

        }
    }
}