using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;
using Domain;
using Server;

namespace ClientHandler
{
    public class FilePageServer
    {
        
        public void ShowFileList(MemoryRepository repository,SocketHandler socketHandler,Socket socketClient)
        {
            string[] _options = {"All files", "By theme", "Order by creation date", "Order by name", "Order by size", "Back"};
            int option = new MenuServer().ShowMenu(_options, "File menu");
            switch (option)
            {
                case 1:
                    Console.Clear();
                    ShowAllFiles(repository,socketClient, socketHandler);
                    break;
                case 2:
                    Console.Clear();
                    ShowFileByTheme(repository,socketClient, socketHandler);
                    break;
                case 3:
                    Console.Clear();
                    ShowFileByDate(repository,socketClient, socketHandler);
                    break;
                case 4:
                    Console.Clear();
                    ShowFileByName(repository,socketClient, socketHandler);
                    break;
                case 5:
                    Console.Clear();
                    ShowFileBySize(repository,socketClient, socketHandler);
                    break;
                case 6:
                    Console.Clear();  
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
                ShowFileList(repository, socketHandler, socketClient);
            }
            ShowFileList(repository, socketHandler, socketClient);
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
                                  orderList[i].Size.ToString() + "Upload date: " + orderList[i].UploadDate.ToString());
                if (repository.Files[i].Themes != null)
                {
                    Console.WriteLine("Themes");
                    foreach (var theme in repository.Files[i].Themes)
                    {
                        Console.WriteLine("Name: " + theme.Name);
                    }
                }
                if (repository.Files[i].Post != null)
                {
                    Console.WriteLine("File: ");
                    Console.WriteLine("Name: " + repository.Files[i].Post.Name);
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
        private void ShowFileByName(MemoryRepository repository,Socket socketClient, SocketHandler socketHandler)
        {
            var optionSelect = ListFileByName(repository,"Files by name");
            if (optionSelect == "Back")
            {
                ShowFileList(repository, socketHandler, socketClient);
            }
            ShowFileList(repository, socketHandler, socketClient);
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
                if (repository.Files[i].Themes != null)
                {
                    Console.WriteLine("Themes");
                    foreach (var theme in repository.Files[i].Themes)
                    {
                        Console.WriteLine("Name: " + theme.Name);
                    }
                }
                if (repository.Files[i].Post != null)
                {
                    Console.WriteLine("File: ");
                    Console.WriteLine("Name: " + repository.Files[i].Post.Name);
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

        private void ShowFileByDate(MemoryRepository repository,Socket socketClient, SocketHandler socketHandler)
        {
            var optionSelect = ListFileByDate(repository,"Files by date");
            if (optionSelect == "Back")
            {
                ShowFileList(repository, socketHandler, socketClient);
            }
            ShowFileList(repository, socketHandler, socketClient);
        }

        private string ListThemes(MemoryRepository repository,SocketHandler socketHandler)
        {
            string [] posts= new string[repository.Themes.Count];
            for (int i = 0; i < repository.Themes.Count; i++)
            {
                posts[i] = repository.Themes[i].Name;
            }
            int index = new MenuServer().ShowMenu(posts, "Themes");
            string optionSelect = posts[index - 1];
            return optionSelect.Split("\0")[0];
        }


        private void ShowFileByTheme(MemoryRepository repository,Socket socketClient, SocketHandler socketHandler)
        {
            if (repository.Themes.Count > 0)
            {
                var themeName = ListThemes(repository, socketHandler);
                var optionSelect = ListFileByTheme(repository, "Files by theme name", themeName);
                if (optionSelect == "Back")
                {
                    ShowFileList(repository, socketHandler, socketClient);
                }
                ShowFileList(repository, socketHandler, socketClient);
            }
            else
            {
                Console.WriteLine("There aren't themes to filter");
                ShowFileList(repository, socketHandler, socketClient);
            }
        }
        
        private static string ListFileByTheme(MemoryRepository repository,string title,string themeName)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----"+ title +"----");
            Theme theme = repository.Themes.Find(x => x.Name == themeName);
            for (var i = 0; i < repository.Files.Count; i++)
            {
                if (repository.Files[i].Themes != null)
                {
                    if (repository.Files[i].Themes.Contains(theme))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        var prefix = "File" + i + 1 + ":  ";
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(prefix + "Name: " + repository.Files[i].Name + "Size: " +
                                          repository.Files[i].Size + "Upload date: " + repository.Files[i].UploadDate);


                        Console.WriteLine("Themes");
                        foreach (var themef in repository.Files[i].Themes)
                        {
                            Console.WriteLine("Name: " + themef.Name);
                        }

                        if (repository.Files[i].Post != null)
                        {
                            Console.WriteLine("File: ");
                            Console.WriteLine("Name: " + repository.Files[i].Post.Name);
                        }
                    }
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
                if (repository.Files[i].Themes != null)
                {
                    Console.WriteLine("Themes");
                    foreach (var theme in repository.Files[i].Themes)
                    {
                        Console.WriteLine("Name: " + theme.Name);
                    }
                }
                if (repository.Files[i].Post != null)
                {
                    Console.WriteLine("File: ");
                    Console.WriteLine("Name: " + repository.Files[i].Post.Name);
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

        public void ShowAllFiles(MemoryRepository repository, Socket socketClient, SocketHandler socketHandler)
        {
            var optionSelect = ListFiles(repository, "File posts");
            if (optionSelect == "Back")
            {
                ShowFileList(repository, socketHandler, socketClient);
            }
            else
            {
                Post post = repository.Posts.Find(x => x.Name == optionSelect);
                File file = post.File;
                Console.WriteLine("File\n" + "Name:" + file.Name + "Size:" + file.Size
                                  + "Upload date" + file.UploadDate);
                if (file.Themes != null)
                {
                    Console.WriteLine("Themes");
                    foreach (var theme in file.Themes)
                    {
                        Console.WriteLine("Name: " + theme.Name);
                    }
                }
                if (file.Post != null)
                {
                    Console.WriteLine("File: ");
                    Console.WriteLine("Name: " + file.Post.Name);
                }
            }

            ShowFileList(repository, socketHandler, socketClient);
        }
    }
}