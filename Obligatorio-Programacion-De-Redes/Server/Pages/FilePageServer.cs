using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;
using Domain;

namespace ClientHandler
{
    public class FilePageServer
    {
        
        public void ShowFileList(MemoryRepository repository,SocketHandler socketHandler,Socket socketClient)
        {
           
            Console.Clear();
            var exit = false;
            string[] _options = {"All files", "By theme", "Order by creation date", "Order by name", "Order by size", "Back"};
            Console.WriteLine("----Select filter----");
            for (var i = 0; i < _options.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                var prefix =i + 1 + ".  ";
                Console.WriteLine($"{prefix}{_options[i]}");
            }
            Console.ForegroundColor = ConsoleColor.Black;
            var var = Console.ReadLine();
            int option= Int32.Parse(var);
            switch (option)
            {
                case 1:
                    ShowAllFiles(repository,socketClient, socketHandler);
                    break;
                case 2:
                    ShowFileByTheme(repository,socketClient, socketHandler);
                    break;
                case 3:
                    ShowFileByDate(repository,socketClient, socketHandler);
                    break;
                case 4:
                    ShowFileByName(repository,socketClient, socketHandler);
                    break;
                case 5:
                    ShowFileBySize(repository,socketClient, socketHandler);
                    break;
                case 6:
                    exit = true;
                    new HomePageServer().Menu(repository,socketClient, socketHandler);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        private static string ListFileBySize(MemoryRepository repository,string title)
        {
            IOrderedEnumerable<File> orderedEnumerable= repository.Files.OrderBy(x=>x.Size);
            List<File> orderList =orderedEnumerable.ToList();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----"+ title +"----");
            for (var i = 0; i < orderList.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                var prefix = "File" + i + 1 + ":  ";
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(prefix + "Name: " + orderList[i].Name + "Size: " +
                                  orderList[i].Size + "Upload date: " + orderList[i].UploadDate);
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
        private void ShowFileBySize(MemoryRepository repository,Socket socketClient, SocketHandler socketHandler)
        {
            var optionSelect = ListFileBySize(repository,"Files by size");
            if (optionSelect == "Back")
            {
                new HomePageServer().Menu(repository,socketClient, socketHandler);
            }
            new HomePageServer().Menu(repository,socketClient, socketHandler);
        }

        private static string ListFileByName(MemoryRepository repository,string title)
        {
            IOrderedEnumerable<File> orderedEnumerable= repository.Files.OrderBy(x=>x.Name);
            List<File> orderList =orderedEnumerable.ToList();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----"+ title +"----");
            for (var i = 0; i < orderList.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                var prefix = "File" + i + 1 + ":  ";
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(prefix + "Name: " + orderList[i].Name + "Size: " +
                                  orderList[i].Size + "Upload date: " + orderList[i].UploadDate);
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
        private void ShowFileByName(MemoryRepository repository,Socket socketClient, SocketHandler socketHandler)
        {
            var optionSelect = ListFileByName(repository,"Files by name");
            if (optionSelect == "Back")
            {
                new HomePageServer().Menu(repository,socketClient, socketHandler);
            }
            new HomePageServer().Menu(repository,socketClient, socketHandler);
        }
        
        private static string ListFileByDate(MemoryRepository repository,string title)
        {
            IOrderedEnumerable<File> orderedEnumerable= repository.Files.OrderBy(x=>x.UploadDate);
            List<File> orderList =orderedEnumerable.ToList();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----"+ title +"----");
            for (var i = 0; i < orderList.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                var prefix = "File" + i + 1 + ":  ";
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(prefix + "Name: " + orderList[i].Name + "Size: " +
                                  orderList[i].Size + "Upload date: " + orderList[i].UploadDate);
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

        private void ShowFileByDate(MemoryRepository repository,Socket socketClient, SocketHandler socketHandler)
        {
            var optionSelect = ListFileByDate(repository,"Files by date");
            if (optionSelect == "Back")
            {
                new HomePageServer().Menu(repository,socketClient, socketHandler);
            }
            new HomePageServer().Menu(repository,socketClient, socketHandler);
        }

        private void ShowFileByTheme(MemoryRepository repository,Socket socketClient, SocketHandler socketHandler)
        {
            Console.WriteLine("Theme name to filter");
            string themeName = Console.ReadLine();
            var optionSelect = ListFileByTheme(repository,"Files by theme name",themeName);
            if (optionSelect == "Back")
            {
                new HomePageServer().Menu(repository,socketClient, socketHandler);
            }
            new HomePageServer().Menu(repository,socketClient, socketHandler);
        }
        
        private static string ListFileByTheme(MemoryRepository repository,string title,string themeName)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----"+ title +"----");
            Theme theme = repository.Themes.Find(x => x.Name == themeName);
            for (var i = 0; i < repository.Files.Count; i++)
            {

                if (repository.Files[i].Themes.Contains(theme))
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    var prefix = "File" + i + 1 + ":  ";
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(prefix + "Name: " + repository.Files[i].Name + "Size: " +
                                      repository.Files[i].Size + "Upload date: "+ repository.Files[i].UploadDate);
                }
            }
            Console.WriteLine(repository.Files.Count+1 +".  Back");
            var var=Console.ReadLine();
            int indexPost= Int32.Parse(var);
            if (indexPost > repository.Files.Count)
            {
                return "Back";
                
            }
            else
            {
                string optionSelectedPosts = repository.Files[indexPost - 1].Name;
                return optionSelectedPosts;
            }
        }
        
        private static string ListFiles(MemoryRepository repository,string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----"+ title +"----");
            for (var i = 0; i < repository.Files.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                var prefix = "File" + i + 1 + ":  ";
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(prefix + "Name: " + repository.Files[i].Name + "Size: " +
                                  repository.Files[i].Size + "Upload date: " + repository.Files[i].UploadDate);
            }
            Console.WriteLine(repository.Files.Count+1 +".  Back");
            var var=Console.ReadLine();
            int indexPost= Int32.Parse(var);
            if (indexPost > repository.Files.Count)
            {
                return "Back";
                
            }
            else
            {
                string optionSelectedPosts = repository.Files[indexPost - 1].Name;
                return optionSelectedPosts;
            }
        }

        public void ShowAllFiles(MemoryRepository repository, Socket socketClient, SocketHandler socketHandler)
        {
            var optionSelect = ListFiles(repository, "File posts");
            if (optionSelect == "Back")
            {
                new HomePageServer().Menu(repository, socketClient, socketHandler);
            }
            else
            {
                Post post = repository.Posts.Find(x => x.Name == optionSelect);
                File file = post.File;
                Console.WriteLine("File\n" + "Name:" + file.Name + "Size:" + file.Size
                                  + "Upload date" + file.UploadDate);
            }

            new HomePageServer().Menu(repository, socketClient, socketHandler);
        }
    }
}