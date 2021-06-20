using System;
using System.Collections.Generic;
using System.Net.Sockets;
using BusinessLogic;
using BusinessLogic.Managers;
using DomainObjects;
using Server;
using Server.Server;
using Server.Server.Pages;

namespace Server
{
    public class ThemePageServer
    {
        public void Menu(ManagerRepository repository,ManagerThemeRepository managerThemeRepository,ManagerPostRepository managerPostRepository)
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
                        ShowThemeList(repository,managerThemeRepository,managerPostRepository);
                        break;
                    case 2:
                        Console.Clear();
                        ShowThemeWithMorePosts(repository,managerThemeRepository,managerPostRepository);
                        break;
                    case 3:
                        Console.Clear();
                        exit = true;
                        new HomePageServer().MenuAsync(repository,false,managerPostRepository,managerThemeRepository);
                        break;
                    default:
                       break;
                }
            }
        }

        private static string ListThemes(ManagerThemeRepository managerThemeRepository, string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----" + title + "----");
            for (var i = 0; i < managerThemeRepository.Themes.Get().Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine((i+1) + ".  Theme" + (i + 1) + ":  ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Name: " + managerThemeRepository.Themes.Get()[i].Name + " Description: " +
                                  managerThemeRepository.Themes.Get()[i].Description);
                Console.WriteLine("Posts: ");
                if (managerThemeRepository.Themes.Get()[i].Posts != null)
                {
                    List<Post> posts = managerThemeRepository.Themes.Get()[i].Posts;
                    foreach (var post in posts)
                    {
                        Console.WriteLine("Name: " + post.Name);
                    }

                }
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(managerThemeRepository.Themes.Get().Count + 1 + ".  Back");
            Console.ForegroundColor = ConsoleColor.White;
            var var = Console.ReadLine();
            int indexThemes = Int32.Parse(var);
            if (indexThemes > managerThemeRepository.Themes.Get().Count)
            {
                return "Back";
            }
            else
            {
                string optionSelectThemes = managerThemeRepository.Themes.Get()[indexThemes - 1].Name;
                return optionSelectThemes;
            }
        }

        private void ShowThemeList(ManagerRepository repository,ManagerThemeRepository managerThemeRepository,ManagerPostRepository managerPostRepository)
        {
            string optionSelected = ListThemes(managerThemeRepository,"Themes");
            if (optionSelected == "Back")
            {
                Menu(repository,managerThemeRepository,managerPostRepository);
            }
            Menu(repository,managerThemeRepository,managerPostRepository);
        }

        private void ShowThemeWithMorePosts(ManagerRepository repository,ManagerThemeRepository managerThemeRepository,ManagerPostRepository managerPostRepository)
        {
            int max = 0;
            List<string> themeNames = new List<string>();
            int cant = 0;
            if (managerThemeRepository.Themes.Get() != null && managerThemeRepository.Themes.Get().Count!=0)
            {
                foreach (var theme in managerThemeRepository.Themes.Get())
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
                    Theme themeMax = managerThemeRepository.Themes.Get().Find(x => x.Name == themeName);
                    Console.WriteLine("Name:" + themeMax.Name + " Description: " +
                                      themeMax.Description);
                }
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine( "2.  Back");
                Console.ForegroundColor = ConsoleColor.White;
                var var = Console.ReadLine();
                int indexThemes = Int32.Parse(var);
                if (indexThemes > managerThemeRepository.Themes.Get().Count)
                {
                    Menu(repository,managerThemeRepository,managerPostRepository);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There aren't themes in the system");
                Console.ForegroundColor = ConsoleColor.White;
                Menu(repository,managerThemeRepository,managerPostRepository);
            }

        }
    }
}