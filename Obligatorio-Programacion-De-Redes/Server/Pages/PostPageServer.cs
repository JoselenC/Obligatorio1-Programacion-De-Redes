using System;
using System.Collections.Generic;
using System.Net.Sockets;
using BusinessLogic;
using Client;
using Domain;
using DataHandler;
using Domain.Services;

namespace Server
{
    public class PostPageServer
    {

        public void Menu(Socket socketClient,SocketHandler socketHandler, MemoryRepository repository)
        {
            Console.Clear();
            var exit = false;
            string[] _options = {"Show theme post", "Show post", "Show file post", "Back"};
            while (!exit)
            {
                var option = new MenuServer().ShowMenu(_options);
                switch (option)
                {
                    case 1:
                        ShowThemePost(socketClient, socketHandler, repository);
                        break;
                    case 2:
                        ShowPost(socketHandler, repository);
                        break;
                    case 3:
                        ShowFilePost(socketHandler, repository);
                        break;
                    case 4:
                        exit = true;
                        new HomePageServer().Menu(socketClient,socketHandler, repository);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

       private void ShowThemePost(Socket socketClient, SocketHandler socketHandler, MemoryRepository repository)
        { 
            Console.Clear();
            var exit = false;
            while (!exit)
            {
                var option = ShowFilter();
                switch (option)
                {
                    case 1:
                        ShowThemePostByCreationDate(socketHandler, repository);
                        break;
                    case 2:
                        ShowThemePostByTheme(socketHandler, repository);
                        break;
                    case 3:
                        ShowThemePostByDateAndTheme(socketHandler, repository);
                        break;
                    case 4:
                        exit = true;
                        new HomePageServer().Menu(socketClient, socketHandler, repository);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
        
        private int ShowFilter()
        {
            string[] _options = {"By creation date", "By theme", "By both", "Back"};
            bool salir = false;
            int index = 0;
            while (!salir)
            {
                Console.Write("Select filter ");
                for (var i = 0; i < 4; i++)
                {
                    var prefix = "  ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        prefix = "> ";
                    }
                
                    Console.WriteLine($"{prefix}{_options[i]}");
                }
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        salir = true;
                        return index+1;
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        if (index > 0)
                            index = index - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        if (index < 3)
                            index = index + 1;
                        else
                            index = 0;
                        break;
                    case ConsoleKey.Escape:
                        index = 3;
                        break;
                }
            }

            return 1;
        }


        private void ShowPost(SocketHandler socketHandler, MemoryRepository repository)
        {
            List<Post> posts = ShowThemePostByCreationDate(socketHandler, repository);
            Console.WriteLine("Select post number");
            int option = Int32.Parse(Console.ReadLine());
            String name = posts[option].Name;
            List<Theme> postThemes = posts[option].Theme;
            String creationDate = posts[option].CreationDate;
            File file = posts[option].File;
            
            Console.WriteLine("Name: " + name);

            if (postThemes == null || postThemes.Count == 0)
            {
                Console.WriteLine("Themes: no themes");
            }
            else
            {
                Console.WriteLine("Themes: ");
                for (int i = 0; i < postThemes.Count; i++)
                {
                    Console.WriteLine(i + ". " + postThemes[i].Name);   
                }    
            }
            Console.WriteLine("Creation date: " + creationDate);
            if (file == null)
            {
                Console.WriteLine("File: no file");
            }
            else
            {
                Console.WriteLine("File: " + file.Name);
            }
            
        }

        public void ShowFilePost(SocketHandler socketHandler, MemoryRepository repository)
        {
            List<Post> posts = ShowThemePostByCreationDate(socketHandler, repository);
            Console.WriteLine("Select post number");
            int option = Int32.Parse(Console.ReadLine());
            File file = posts[option].File;
            if (file == null)
            {
                Console.WriteLine("File: no file");
            }
            else
            {
                Console.WriteLine("File: " + file.Name);
            }

        }
        
        private List<Post> ShowThemePostByCreationDate(SocketHandler socketHandler, MemoryRepository repository)
        {
            PostService postService = new PostService(repository);
            List<Post> orderedPosts = postService.OrderPostByCreationDate();
            Console.WriteLine("Posts by creation date");
            for (int i = 0; i < orderedPosts.Count; i++)
            {
                Console.WriteLine(i + ". " + orderedPosts[i].Name);   
            }

            return orderedPosts;
        }

        public void ShowThemePostByTheme(SocketHandler socketHandler, MemoryRepository repository)
        {
            PostService postService = new PostService(repository);
            List<Post> orderedPosts = postService.OrderPostTheme();
            Console.WriteLine("Posts by theme");
            for (int i = 0; i < orderedPosts.Count; i++)
            {
                Console.WriteLine(i + ". " + orderedPosts[i].Name);   
            }
        }
        
        public void ShowThemePostByDateAndTheme(SocketHandler socketHandler, MemoryRepository repository)
        {
            PostService postService = new PostService(repository);
            List<Post> orderedPosts = postService.OrderPostTheme();
            Console.WriteLine("Posts by theme");
            for (int i = 0; i < orderedPosts.Count; i++)
            {
                Console.WriteLine(i + ". " + orderedPosts[i].Name);   
            }
        }

    }
}