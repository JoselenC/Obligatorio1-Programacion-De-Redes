using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using BusinessLogic;
using Domain;
using DataHandler;
using Domain.Services;
using Server;

namespace ClientHandler
{
    public class PostPageServer
    {

        public void Menu(MemoryRepository repository,SocketHandler socketHandler)
        {
            string[] _options = {"Show theme post", "Show post", "Show file post", "Back"};
            int option = new MenuServer().ShowMenu(_options,"Post menu");
                switch (option)
                {
                    case 1:
                        Console.Clear();
                       MenuShowThemePost(repository, socketHandler);
                        break;
                    case 2:
                        Console.Clear();
                        ShowEspecificPost(repository,socketHandler);
                        break;
                    case 3:
                        Console.Clear();
                        ShowFilePost(repository,socketHandler);
                        break;
                    case 4:
                        Console.Clear();
                        new HomePageServer().MenuAsync(repository,socketHandler);
                        break;
                    default:
                        break;
                
            }
        }
        
        private void ShowEspecificPost(MemoryRepository repository,SocketHandler socketHandler)
        {
            var optionSelect = ListPost(repository);
            if (optionSelect == "Back")
            {
                Menu(repository, socketHandler);
            }
            else
            {
                Post post = repository.Posts.Find(a => a.Name==optionSelect);
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
            Menu(repository, socketHandler);
        }

       private void MenuShowThemePost(MemoryRepository repository,SocketHandler socketHandler)
        { 
            string[] _options = {"By creation date", "By theme", "By both", "Back"};
            int option = new MenuServer().ShowMenu(_options,"Filter");
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        ShowThemePostByCreationDate(repository,socketHandler);
                        break;
                    case 2:
                        Console.Clear();
                        ShowPostByTheme(repository,socketHandler);
                        break;
                    case 3:
                        Console.Clear();
                        ShowThemePostByDateAndTheme(repository,socketHandler);
                        break;
                    case 4:
                        new HomePageServer().MenuAsync(repository,socketHandler);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
        }
       
     private static string ListPostOrder(MemoryRepository repository,string title)
       {
           IOrderedEnumerable<Post> orderedEnumerable= repository.Posts.OrderBy(x=> new DateTime(Convert.ToInt32(x.CreationDate.Substring(6, 4)), Convert.ToInt32(x.CreationDate.Substring(3, 2)), // Month
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
       
       private static string ListPostByTheme(MemoryRepository repository,string title,string themeName)
       {
           Console.ForegroundColor = ConsoleColor.DarkCyan;
           Console.WriteLine("----"+ title +"----");
           int cant = 0;
            if (themeName != "Back")
            {
                Theme theme = repository.Themes.Find(x => x.Name == themeName);

                for (var i = 0; i < repository.Posts.Count; i++)
                {

                    if (repository.Posts[i].Themes.Contains(theme))
                    {
                        cant++;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        var prefix = "Post" + i + 1 + ":  ";
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(prefix + "Name:" + repository.Posts[i].Name + "Creation date:" +
                                          repository.Posts[i].CreationDate);
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
                        string optionSelectedPosts = repository.Posts[indexPost - 1].Name;
                        return optionSelectedPosts;
                    }
                }
            }
            else
            {
                return "Back";
            }
       }

        private static string ListThemes(MemoryRepository repository, string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----" + title + "----");
            for (var i = 0; i < repository.Themes.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Theme" + (i + 1) + ":  ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Name: " + repository.Themes[i].Name + " Description: " +
                                  repository.Themes[i].Description);
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

        public void ShowPostByTheme(MemoryRepository repository,SocketHandler socketHandler)
       {
            var optionTheme = ListThemes(repository, "Select theme to filter");
           var optionSelect = ListPostByTheme(repository,"Posts by theme name", optionTheme);
           if (optionSelect == "Back")
           {
               MenuShowThemePost(repository,socketHandler);
           }
           else if (optionSelect == "0")
           {
               Console.WriteLine("There aren't post with this theme ");
               MenuShowThemePost(repository,socketHandler);
           }
           else
           {
               MenuShowThemePost(repository,socketHandler);
           }
           
       }

       private static string ListPostByThemeAndCreationDate(MemoryRepository repository,string title,string themeName)
       {
            IOrderedEnumerable<Post> orderedEnumerable = repository.Posts.OrderBy(x => new DateTime(Convert.ToInt32(x.CreationDate.Substring(6, 4)), Convert.ToInt32(x.CreationDate.Substring(3, 2)), // Month
                                     Convert.ToInt32(x.CreationDate.Substring(0, 2))));
            List<Post>orderList=orderedEnumerable.ToList();
           Console.ForegroundColor = ConsoleColor.DarkCyan;
           Console.WriteLine("----"+ title +"----");
           int cant = 0;
            if (themeName != "Back")
            {
                Theme theme = repository.Themes.Find(x => x.Name == themeName);
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
       private void ShowThemePostByCreationDate(MemoryRepository repository,SocketHandler socketHandler)
       {
           var optionSelect = ListPostOrder(repository,"Posts by creation date");
           if (optionSelect == "Back")
           {
               MenuShowThemePost(repository, socketHandler);
           }
           else if(optionSelect == "0")
           {
               Console.WriteLine("There aren't post with this theme ");
               MenuShowThemePost(repository,socketHandler);
           }
           else
           {
               MenuShowThemePost(repository,socketHandler);
           }
       }
       
       public void ShowThemePostByDateAndTheme(MemoryRepository repository,SocketHandler socketHandler)
       {
            var optionTheme = ListThemes(repository, "Select theme to filter");
            var optionSelect = ListPostByThemeAndCreationDate(repository,"Posts by theme name", optionTheme);
           if (optionSelect == "Back")
           {
               MenuShowThemePost(repository, socketHandler);
           }
           MenuShowThemePost(repository,socketHandler);
       }

       private static string ListPost(MemoryRepository repository)
       {
           string[] postNames = GetPostNames(repository);
           int index = new MenuServer().ShowMenu(postNames,"Posts");
           string optionSelect = postNames[index-1];
           return optionSelect;
       }

       private static string[] GetPostNames(MemoryRepository repository)
       {
           string[] themesName = new string[repository.Posts.Count+1];
           for (int i=0; i<repository.Posts.Count; i++)
           {
               themesName[i] = repository.Posts[i].Name;
           }

           themesName[repository.Posts.Count] = "Back";
           return themesName;
       }
       public void ShowFilePost(MemoryRepository repository,SocketHandler socketHandler)
        {
            var optionSelect = ListPost(repository);
            if (optionSelect == "Back")
            {
                Menu(repository,socketHandler);
            }
            else
            {
                Post post = repository.Posts.Find(x => x.Name == optionSelect);
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
                Menu(repository,socketHandler);
            }

        }
        
    }
}