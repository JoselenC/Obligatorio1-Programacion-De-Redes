using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Managers;
using DomainObjects;

namespace Server.Server.Pages
{
    public class PostPageServer
    {

        public void Menu(ManagerRepository repository, ManagerPostRepository managerPostRepository, ManagerThemeRepository managerThemeRepository)
        {
            string[] options = {"Show theme post", "Show post", "Show file post", "Back"};
            int option = new MenuServer().ShowMenu(options,"Post menu");
                switch (option)
                {
                    case 1:
                        Console.Clear();
                       MenuShowThemePost(repository,managerPostRepository,managerThemeRepository);
                        break;
                    case 2:
                        Console.Clear();
                        ShowEspecificPost(repository,managerPostRepository,managerThemeRepository);
                        break;
                    case 3:
                        Console.Clear();
                        ShowFilePost(repository,managerPostRepository,managerThemeRepository);
                        break;
                    case 4:
                        Console.Clear();
                        new HomePageServer().MenuAsync(repository,false,managerPostRepository,managerThemeRepository);
                        break;
                    default:
                        break;
                
            }
        }
        
        private void ShowEspecificPost(ManagerRepository repository, ManagerPostRepository managerPostRepository, ManagerThemeRepository managerThemeRepository)
        {
            var optionSelect = ListPost(managerPostRepository);
            if (optionSelect == "Back")
            {
                Menu(repository,managerPostRepository,managerThemeRepository);
            }
            else
            {
                Post post = managerPostRepository.Posts.Find(a => a.Name==optionSelect);
                Console.WriteLine("Post\n" + "Name: " + post.Name + "Creation date: " + post.CreationDate);
              
                if (post.Themes != null)
                {
                    Console.WriteLine("Themes");
                    foreach (var theme in post.Themes)
                    {
                        Console.WriteLine("Name:  " + theme.Name);
                    }
                }

                if (post.File != null)
                {
                  Console.WriteLine("File\n" + "Name: " + post.File.Name);                    
                }

            }
            Menu(repository,managerPostRepository,managerThemeRepository);
        }

       private void MenuShowThemePost(ManagerRepository repository, ManagerPostRepository managerPostRepository, ManagerThemeRepository managerThemeRepository)
        { 
            string[] _options = {"By creation date", "By theme", "By both", "Back"};
            int option = new MenuServer().ShowMenu(_options,"Filter");
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        ShowThemePostByCreationDate(managerThemeRepository,managerPostRepository,repository);
                        break;
                    case 2:
                        Console.Clear();
                        ShowPostByTheme(repository,managerThemeRepository,managerPostRepository);
                        break;
                    case 3:
                        Console.Clear();
                        ShowThemePostByDateAndTheme(repository,managerThemeRepository,managerPostRepository);
                        break;
                    case 4:
                        new HomePageServer().MenuAsync(repository,false,managerPostRepository,managerThemeRepository);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
        }
       
     private static string ListPostOrder(string title,ManagerPostRepository managerPostRepository)
       {
           IOrderedEnumerable<Post> orderedEnumerable= managerPostRepository.Posts.Get().OrderBy(x=> new DateTime(Convert.ToInt32(x.CreationDate.Substring(6, 4)), Convert.ToInt32(x.CreationDate.Substring(3, 2)), // Month
                                    Convert.ToInt32(x.CreationDate.Substring(0, 2))));
            List<Post>orderList=orderedEnumerable.ToList();
           Console.ForegroundColor = ConsoleColor.DarkCyan;
           Console.WriteLine("----"+ title +"----");
           for (var i = 0; i < orderList.Count; i++)
           {
               Console.ForegroundColor = ConsoleColor.DarkCyan;
               var prefix = "Post" +i +1 + ":  ";
               Console.BackgroundColor = ConsoleColor.Black;
               Console.WriteLine(prefix + "Name:" + orderList[i].Name + "Creation date:" + orderList[i].CreationDate);
           }
           Console.WriteLine(orderList.Count+1 +".  Back");
           var var=Console.ReadLine();
           int indexPost= Int32.Parse(var);
           if (indexPost > orderList.Count)
           {
               return "Back";
                
           }
           else
           {
               string optionSelectedPosts = orderList[indexPost - 1].Name;
               return optionSelectedPosts;
           }
       }
       
       private static string ListPostByTheme(ManagerPostRepository managerPostRepository,ManagerThemeRepository managerThemeRepository,string title,string themeName)
       {
           Console.ForegroundColor = ConsoleColor.DarkCyan;
           Console.WriteLine("----"+ title +"----");
           int cant = 0;
            if (themeName != "Back")
            {
                Theme theme = managerThemeRepository.Themes.Find(x => x.Name == themeName);

                for (var i = 0; i < managerPostRepository.Posts.Get().Count; i++)
                {

                    if (managerPostRepository.Posts.Get()[i].Themes.Contains(theme))
                    {
                        cant++;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        var prefix = "Post" + i + 1 + ":  ";
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(prefix + "Name:" + managerPostRepository.Posts.Get()[i].Name + "Creation date:" +
                                          managerPostRepository.Posts.Get()[i].CreationDate);
                    }
                }

                if (cant == 0)
                {
                    return "0";
                }
                else
                {
                    Console.WriteLine(cant + 1 + ".  Back");
                    var var = Console.ReadLine();
                    int indexPost = Int32.Parse(var);
                    if (indexPost > cant)
                    {
                        return "Back";

                    }
                    else
                    {
                        string optionSelectedPosts = managerPostRepository.Posts.Get()[indexPost - 1].Name;
                        return optionSelectedPosts;
                    }
                }
            }
            else
            {
                return "Back";
            }
       }

        private static string ListThemes(string title,ManagerThemeRepository managerThemeRepository)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----" + title + "----");
            for (var i = 0; i < managerThemeRepository.Themes.Get().Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Theme" + (i + 1) + ":  ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Name: " + managerThemeRepository.Themes.Get()[i].Name + " Description: " +
                                  managerThemeRepository.Themes.Get()[i].Description);
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

        public void ShowPostByTheme(ManagerRepository repository,ManagerThemeRepository managerThemeRepository,ManagerPostRepository managerPostRepository)
       {
            var optionTheme = ListThemes( "Select theme to filter",managerThemeRepository);
           var optionSelect = ListPostByTheme(managerPostRepository,managerThemeRepository,"Posts by theme name", optionTheme);
           if (optionSelect == "Back")
           {
               MenuShowThemePost(repository,managerPostRepository,managerThemeRepository);
           }
           else if (optionSelect == "0")
           {
               Console.WriteLine("There aren't post with this theme ");
               MenuShowThemePost(repository,managerPostRepository,managerThemeRepository);
           }
           else
           {
               MenuShowThemePost(repository,managerPostRepository,managerThemeRepository);
           }
           
       }

       private static string ListPostByThemeAndCreationDate(ManagerThemeRepository managerThemeRepository,ManagerPostRepository managerPostRepository,string title,string themeName)
       {
            IOrderedEnumerable<Post> orderedEnumerable = managerPostRepository.Posts.Get()
                .OrderBy(x => new DateTime(Convert.ToInt32(x.CreationDate.Substring(6, 4)), 
                    Convert.ToInt32(x.CreationDate.Substring(3, 2)), // Month
                    Convert.ToInt32(x.CreationDate.Substring(0, 2))));
            List<Post>orderList=orderedEnumerable.ToList();
           Console.ForegroundColor = ConsoleColor.DarkCyan;
           Console.WriteLine("----"+ title +"----");
           int cant = 0;
            if (themeName != "Back")
            {
                Theme theme = managerThemeRepository.Themes.Find(x => x.Name == themeName);
                for (var i = 0; i < orderList.Count; i++)
                {

                    if (orderList[i].Themes.Contains(theme))
                    {
                        cant++;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        var prefix = "Post" + i + 1 + ":  ";
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(prefix + "Name:" + orderList[i].Name + "Creation date:" +
                                          orderList[i].CreationDate);
                    }
                }

                if (cant == 0)
                {
                    return "0";
                }
                else
                {
                    Console.WriteLine(orderList.Count + 1 + ".  Back");
                    var var = Console.ReadLine();
                    int indexPost = Int32.Parse(var);
                    if (indexPost > orderList.Count)
                    {
                        return "Back";

                    }
                    else
                    {
                        string optionSelectedPosts = orderList[indexPost - 1].Name;
                        return optionSelectedPosts;
                    }
                }
            }
            else
            {
                return "Back";
            }
       }
       private void ShowThemePostByCreationDate(ManagerThemeRepository managerThemeRepository,ManagerPostRepository managerPostRepository,ManagerRepository repository)
       {
           var optionSelect = ListPostOrder("Posts by creation date",managerPostRepository);
           if (optionSelect == "Back")
           {
               MenuShowThemePost(repository,managerPostRepository,managerThemeRepository);
           }
           else if(optionSelect == "0")
           {
               Console.WriteLine("There aren't post with this theme ");
               MenuShowThemePost(repository,managerPostRepository,managerThemeRepository);
           }
           else
           {
               MenuShowThemePost(repository,managerPostRepository,managerThemeRepository);
           }
       }
       
       public void ShowThemePostByDateAndTheme(ManagerRepository repository,ManagerThemeRepository managerThemeRepository,ManagerPostRepository managerPostRepository)
       {
            var optionTheme = ListThemes( "Select theme to filter",managerThemeRepository);
            var optionSelect = ListPostByThemeAndCreationDate(managerThemeRepository,managerPostRepository,"Posts by theme name", optionTheme);
           if (optionSelect == "Back")
           {
               MenuShowThemePost(repository,managerPostRepository,managerThemeRepository);
           }
           MenuShowThemePost(repository,managerPostRepository,managerThemeRepository);
       }

       private static string ListPost(ManagerPostRepository managerPostRepository)
       {
           string[] postNames = GetPostNames(managerPostRepository);
           int index = new MenuServer().ShowMenu(postNames,"Posts");
           string optionSelect = postNames[index-1];
           return optionSelect;
       }

       private static string[] GetPostNames(ManagerPostRepository managerPostRepository)
       {
           string[] themesName = new string[managerPostRepository.Posts.Get().Count+1];
           for (int i=0; i<managerPostRepository.Posts.Get().Count; i++)
           {
               themesName[i] = managerPostRepository.Posts.Get()[i].Name;
           }

           themesName[managerPostRepository.Posts.Get().Count] = "Back";
           return themesName;
       }
       public void ShowFilePost(ManagerRepository repository,ManagerPostRepository managerPostRepository, ManagerThemeRepository managerThemeRepository)
       {
            var optionSelect = ListPost(managerPostRepository);
            if (optionSelect == "Back")
            {
                Menu(repository,managerPostRepository,managerThemeRepository);
            }
            else
            {
                Post post = managerPostRepository.Posts.Find(x => x.Name == optionSelect);
                File file = post.File;
                if (file != null)
                {
                    Console.WriteLine("File\n" + " Name: " + file.Name + "Size: " + file.Size.ToString()
                                      + " Upload date " + file.UploadDate.ToString());
                    if (file.Themes != null)
                    {
                        Console.WriteLine("Themes");
                        foreach (var theme in file.Themes)
                        {
                            Console.WriteLine("Name: " + theme.Name);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There aren't file to this post");
                }
                Menu(repository,managerPostRepository,managerThemeRepository);
            }

        }
        
    }
}