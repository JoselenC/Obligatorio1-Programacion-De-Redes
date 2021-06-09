using System;
using System.Collections.Generic;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;
using Domain;
using Server;
using ServerGRPC.Server;

namespace Server
{
    public class ThemePageServer
    {
        public void Menu(ManagerRepository repository)
        {
            var exit = false;
            string[] _options = {"Themes list", "Theme with more post", "Back"};
            while (!exit)
            {
                int option = new MenuServer().ShowMenu(_options,"MenuAsync");
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        ShowThemeList(repository);
                        break;
                    case 2:
                        Console.Clear();
                        ShowThemeWithMorePosts(repository);
                        break;
                    case 3:
                        Console.Clear();
                        exit = true;
                        new HomePageServer().MenuAsync(repository,false);
                        break;
                    default:
                       break;
                }
            }
        }

        private static string ListThemes(ManagerRepository repository, string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----" + title + "----");
            for (var i = 0; i < repository.Themes.Get().Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine((i+1) + ".  Theme" + (i + 1) + ":  ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Name: " + repository.Themes.Get()[i].Name + " Description: " +
                                  repository.Themes.Get()[i].Description);
                Console.WriteLine("Posts: ");
                if (repository.Themes.Get()[i].Posts != null)
                {
                    List<Post> posts = repository.Themes.Get()[i].Posts;
                    foreach (var post in posts)
                    {
                        Console.WriteLine("Name: " + post.Name);
                    }

                }
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(repository.Themes.Get().Count + 1 + ".  Back");
            Console.ForegroundColor = ConsoleColor.White;
            var var = Console.ReadLine();
            int indexThemes = Int32.Parse(var);
            if (indexThemes > repository.Themes.Get().Count)
            {
                return "Back";
            }
            else
            {
                string optionSelectThemes = repository.Themes.Get()[indexThemes - 1].Name;
                return optionSelectThemes;
            }
        }

        private void ShowThemeList(ManagerRepository repository)
        {
            string optionSelected = ListThemes(repository,"Themes");
            if (optionSelected == "Back")
            {
                Menu(repository);
            }
            Menu(repository);
        }

        private void ShowThemeWithMorePosts(ManagerRepository repository)
        {
            int max = 0;
            List<string> themeNames = new List<string>();
            int cant = 0;
            if (repository.Themes.Get() != null && repository.Themes.Get().Count!=0)
            {
                foreach (var theme in repository.Themes.Get())
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
                    Theme themeMax = repository.Themes.Get().Find(x => x.Name == themeName);
                    Console.WriteLine("Name:" + themeMax.Name + " Description: " +
                                      themeMax.Description);
                }
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine( "2.  Back");
                Console.ForegroundColor = ConsoleColor.White;
                var var = Console.ReadLine();
                int indexThemes = Int32.Parse(var);
                if (indexThemes > repository.Themes.Get().Count)
                {
                    Menu(repository);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There aren't themes in the system");
                Console.ForegroundColor = ConsoleColor.White;
                Menu(repository);
            }

        }
    }
}