using System;
using System.Collections.Generic;
using System.Net.Sockets;
using BusinessLogic;
using Domain;
using DataHandler;
using Domain.Services;

namespace Server
{
    public class PostPageServer
    {

        public void ShowMenu(Socket socketClient,SocketHandler socketHandler, MemoryRepository repository)
        {
            Console.Clear();
            Console.Write("Select option");
            Console.WriteLine("1-Show theme post");
            Console.WriteLine("2-Show post");
            Console.WriteLine("3-Show file post");
            Console.WriteLine("4-Back");
            bool exit = false;
            while (!exit)
            {

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ShowThemePost(socketClient, socketHandler, repository);
                        break;
                    case "2":
                        ShowPost(socketHandler, repository);
                        break;
                    case "3":
                        ShowFilePost(socketHandler, repository);
                        break;
                    case "4":
                        exit = true;
                        new HomePageServer().ShowMenu(socketClient,socketHandler, repository);
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
            Console.Write("Select filter ");
            Console.WriteLine("1-By creation date");
            Console.WriteLine("2-By theme");
            Console.WriteLine("3-By both");
            Console.WriteLine("4-Back");
            bool exit = false;
            while (!exit)
            {
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ShowThemePostByCreationDate(socketHandler, repository);
                        break;
                    case "2":
                        ShowThemePostByTheme(socketHandler, repository);
                        break;
                    case "3":
                        ShowThemePostByDateAndTheme(socketHandler, repository);
                        break;
                    case "4":
                        exit = true;
                        new HomePageServer().ShowMenu(socketClient, socketHandler, repository);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
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