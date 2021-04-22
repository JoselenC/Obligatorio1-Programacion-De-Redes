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

        public void Menu(MemoryRepository repository,Socket socketClient,SocketHandler socketHandler)
        {
           
            var exit = false;
            string[] _options = {"Show theme post", "Show post", "Show file post", "Back"};
            int option = new MenuServer().ShowMenu(_options,"Post menu");
                switch (option)
                {
                    case 1:
                        Console.Clear();
                       ShowThemePost(repository,socketClient, socketHandler);
                        break;
                    case 2:
                        Console.Clear();
                        ShowPost(repository,socketClient,socketHandler);
                        break;
                    case 3:
                        Console.Clear();
                        ShowFilePost(repository,socketClient,socketHandler);
                        break;
                    case 4:
                        Console.Clear();
                        new HomePageServer().Menu(repository,socketClient,socketHandler);
                        break;
                    default:
                        break;
                
            }
        }

       private void ShowThemePost(MemoryRepository repository,Socket socketClient, SocketHandler socketHandler)
        { 
            Console.Clear();
            var exit = false;
            string[] _options = {"By creation date", "By theme", "By both", "Back"};
            int option = new MenuServer().ShowMenu(_options,"Filter");
                switch (option)
                {
                    case 1:
                        ShowThemePostByCreationDate(repository,socketClient, socketHandler);
                        break;
                    case 2:
                        ShowPostByTheme(repository,socketClient, socketHandler);
                        break;
                    case 3:
                        ShowThemePostByDateAndTheme(repository,socketClient, socketHandler);
                        break;
                    case 4:
                        exit = true;
                        new HomePageServer().Menu(repository,socketClient, socketHandler);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
        }
       
       private static string ListPost(MemoryRepository repository,string title)
       {
           Console.Clear();
           Console.ForegroundColor = ConsoleColor.DarkCyan;
           Console.WriteLine("----"+ title +"----");
           for (var i = 0; i < repository.Posts.Count; i++)
           {
               Console.ForegroundColor = ConsoleColor.DarkCyan;
               var prefix = "Post" +i +1 + ":  ";
               Console.ForegroundColor = ConsoleColor.Black;
               Console.WriteLine(prefix + "\n" + "Name:" + repository.Posts[i].Name + "  Creation date:" + repository.Posts[i].CreationDate);
           }
           Console.WriteLine(repository.Posts.Count+1 +"\n" + ".  Back");
           var var=Console.ReadLine();
           int indexPost= Int32.Parse(var);
           if (indexPost > repository.Posts.Count)
           {
               return "Back";
                
           }
           else
           {
               string optionSelectedPosts = repository.Posts[indexPost - 1].Name;
               return optionSelectedPosts;
           }
       }
       
       private static string ListPostOrder(MemoryRepository repository,string title)
       {
           Console.Clear();
           IOrderedEnumerable<Post> orderedEnumerable= repository.Posts.OrderBy((x => x.CreationDate));
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
           Console.Clear();
           Console.ForegroundColor = ConsoleColor.DarkCyan;
           Console.WriteLine("----"+ title +"----");
           Theme theme = repository.Themes.Find(x => x.Name == themeName);
           for (var i = 0; i < repository.Posts.Count; i++)
           {

               if (repository.Posts[i].Theme.Contains(theme))
               {
                   Console.ForegroundColor = ConsoleColor.DarkCyan;
                   var prefix = "Post" + i + 1 + ":  ";
                   Console.BackgroundColor = ConsoleColor.Black;
                   Console.WriteLine(prefix + "Name:" + repository.Posts[i].Name + "Creation date:" +
                                     repository.Posts[i].CreationDate);
               }
           }
           Console.WriteLine(repository.Posts.Count+1 +".  Back");
           var var=Console.ReadLine();
           int indexPost= Int32.Parse(var);
           if (indexPost > repository.Posts.Count)
           {
               return "Back";
                
           }
           else
           {
               string optionSelectedPosts = repository.Posts[indexPost - 1].Name;
               return optionSelectedPosts;
           }
       }
    
       
       private static string ListPostByThemeAndCreationDate(MemoryRepository repository,string title,string themeName)
       {
           Console.Clear();
           IOrderedEnumerable<Post> orderedEnumerable= repository.Posts.OrderBy((x => x.CreationDate));
           List<Post>orderList=orderedEnumerable.ToList();
           Console.ForegroundColor = ConsoleColor.DarkCyan;
           Console.WriteLine("----"+ title +"----");
           Theme theme = repository.Themes.Find(x => x.Name == themeName);
           for (var i = 0; i < orderList.Count; i++)
           {

               if (orderList[i].Theme.Contains(theme))
               {
                   Console.ForegroundColor = ConsoleColor.DarkCyan;
                   var prefix = "Post" + i + 1 + ":  ";
                   Console.BackgroundColor = ConsoleColor.Black;
                   Console.WriteLine(prefix + "Name:" + orderList[i].Name + "Creation date:" +
                                     orderList[i].CreationDate);
               }
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
       private void ShowPost(MemoryRepository repository,Socket socketClient,SocketHandler socketHandler)
       {
           Console.Clear();
           var optionSelect = ListPostOrder(repository,"Posts");
           if (optionSelect == "Back")
           {
               Menu(repository,socketClient, socketHandler);
           }
           else
           {
               Post post = repository.Posts.Find(a => a.Name==optionSelect);
               Console.WriteLine("Post\n" + "Name:" + post.Name + "Creation date:" + post.CreationDate);
           }
           Menu(repository,socketClient, socketHandler);
       }

        public void ShowFilePost(MemoryRepository repository,Socket socketClient,SocketHandler socketHandler)
        {
            Console.Clear();
            string title = "Select post";
            var optionSelect = ListPost(repository,"File posts");
            if (optionSelect == "Back")
            {
                Menu(repository,socketClient, socketHandler);
            }
            else
            {
                Post post = repository.Posts.Find(x => x.Name == optionSelect);
                File file = post.File;
                Console.WriteLine("File\n" + "Name:" + file.Name + "Size:" + file.Size 
                                  + "Upload date" +file.UploadDate);
            }

        }
        
        private void ShowThemePostByCreationDate(MemoryRepository repository,Socket socketClient, SocketHandler socketHandler)
        {
            Console.Clear();
            var optionSelect = ListPost(repository,"Posts by creation date");
            if (optionSelect == "Back")
            {
                Menu(repository,socketClient, socketHandler);
            }
            Menu(repository,socketClient, socketHandler);
        }

        public void ShowPostByTheme(MemoryRepository repository,Socket socketClient,SocketHandler socketHandler)
        {
            Console.Clear();
            Console.WriteLine("Theme name to filter");
            string themeName = Console.ReadLine();
            var optionSelect = ListPostByTheme(repository,"Posts by theme name",themeName);
            if (optionSelect == "Back")
            {
                Menu(repository,socketClient, socketHandler);
            }
            Menu(repository,socketClient, socketHandler);
        }
        
        public void ShowThemePostByDateAndTheme(MemoryRepository repository,Socket socketClient,SocketHandler socketHandler)
        {
            Console.Clear();
            Console.WriteLine("Theme name to filter");
            string themeName = Console.ReadLine();
            var optionSelect = ListPostByThemeAndCreationDate(repository,"Posts by theme name",themeName);
            if (optionSelect == "Back")
            {
                Menu(repository,socketClient, socketHandler);
            }
            Menu(repository,socketClient, socketHandler);
        }

    }
}