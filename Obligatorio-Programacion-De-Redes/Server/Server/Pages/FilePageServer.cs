using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;
using Domain;
using Server;
using ServerGRPC.Server;

namespace Server
{
    public class FilePageServer
    {
        
        public void ShowFileList(ManagerRepository repository)
        {
            string[] _options = {"All files", "By theme", "Order by creation date", "Order by name", "Order by size", "Back"};
            int option = new MenuServer().ShowMenu(_options, "File menu");
            switch (option)
            {
                case 1:
                    Console.Clear();
                    ShowAllFiles(repository);
                    break;
                case 2:
                    Console.Clear();
                    ShowFileByTheme(repository);
                    break;
                case 3:
                    Console.Clear();
                    ShowFileByDate(repository);
                    break;
                case 4:
                    Console.Clear();
                    ShowFileByName(repository);
                    break;
                case 5:
                    Console.Clear();
                    ShowFileBySize(repository);
                    break;
                case 6:
                    Console.Clear();  
                    new HomePageServer().MenuAsync(repository,false);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        private static string ListFileBySize(ManagerRepository repository,string title)
        {
            IOrderedEnumerable<File> orderedEnumerable= repository.Files.Get().OrderBy(x=>x.Size);
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
        private void ShowFileBySize(ManagerRepository repository)
        {
            var optionSelect = ListFileBySize(repository,"Files by size");
            if (optionSelect == "Back")
            {
                ShowFileList(repository);
            }
            ShowFileList(repository);
        }

        private static string ListFileByName(ManagerRepository repository,string title)
        {
            IOrderedEnumerable<File> orderedEnumerable= repository.Files.Get().OrderBy(x=>x.Name);
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
                if (repository.Files.Get()[i].Themes != null)
                {
                    Console.WriteLine("Themes");
                    foreach (var theme in repository.Files.Get()[i].Themes)
                    {
                        Console.WriteLine("Name: " + theme.Name);
                    }
                }
                if (repository.Files.Get()[i].Post != null)
                {
                    Console.WriteLine("Post: ");
                    Console.WriteLine("Name: " + repository.Files.Get()[i].Post.Name);
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
        private void ShowFileByName(ManagerRepository repository)
        {
            var optionSelect = ListFileByName(repository,"Files by name");
            if (optionSelect == "Back")
            {
                ShowFileList(repository);
            }
            ShowFileList(repository);
        }
        
        private static string ListFileByDate(ManagerRepository repository,string title)
        {
            IOrderedEnumerable<File> orderedEnumerable= repository.Files.Get().OrderBy(x=>x.UploadDate);
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
                if (repository.Files.Get()[i].Themes != null)
                {
                    Console.WriteLine("Themes");
                    foreach (var theme in repository.Files.Get()[i].Themes)
                    {
                        Console.WriteLine("Name: " + theme.Name);
                    }
                }
                if (repository.Files.Get()[i].Post != null)
                {
                    Console.WriteLine("Post: ");
                    Console.WriteLine("Name: " + repository.Files.Get()[i].Post.Name);
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

        private void ShowFileByDate(ManagerRepository repository)
        {
            var optionSelect = ListFileByDate(repository,"Files by date");
            if (optionSelect == "Back")
            {
                ShowFileList(repository);
            }
            ShowFileList(repository);
        }

        private string ListThemes(ManagerRepository repository)
        {
            string [] posts= new string[repository.Themes.Get().Count];
            for (int i = 0; i < repository.Themes.Get().Count; i++)
            {
                posts[i] = repository.Themes.Get()[i].Name;
            }
            int index = new MenuServer().ShowMenu(posts, "Themes");
            string optionSelect = posts[index - 1];
            return optionSelect.Split("\0")[0];
        }


        private void ShowFileByTheme(ManagerRepository repository)
        {
            if (repository.Themes.Get().Count > 0)
            {
                var themeName = ListThemes(repository);
                var optionSelect = ListFileByTheme(repository, "Files by theme name", themeName);
                if (optionSelect == "Back")
                {
                    ShowFileList(repository);
                }
                ShowFileList(repository);
            }
            else
            {
                Console.WriteLine("There aren't themes to filter");
                ShowFileList(repository);
            }
        }
        
        private static string ListFileByTheme(ManagerRepository repository,string title,string themeName)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----"+ title +"----");
            Theme theme = repository.Themes.Find(x => x.Name == themeName);
            for (var i = 0; i < repository.Files.Get().Count; i++)
            {
                if (repository.Files.Get()[i].Themes != null)
                {
                    if (repository.Files.Get()[i].Themes.Contains(theme))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        var prefix = "File" + i + 1 + ":  ";
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(prefix + "Name: " + repository.Files.Get()[i].Name + "Size: " +
                                          repository.Files.Get()[i].Size + "Upload date: " + repository.Files.Get()[i].UploadDate);


                        Console.WriteLine("Themes");
                        foreach (var themef in repository.Files.Get()[i].Themes)
                        {
                            Console.WriteLine("Name: " + themef.Name);
                        }

                        if (repository.Files.Get()[i].Post != null)
                        {
                            Console.WriteLine("Post: ");
                            Console.WriteLine("Name: " + repository.Files.Get()[i].Post.Name);
                        }
                    }
                }
            }
            Console.WriteLine(repository.Files.Get().Count+1 +".  Back");
            var var=Console.ReadLine();
            int indexPost= Int32.Parse(var);
            if (indexPost > repository.Files.Get().Count)
            {
                return "Back";
                
            }
            else
            {
                string optionSelectedPosts = repository.Files.Get()[indexPost - 1].Name;
                return optionSelectedPosts;
            }
        }
        
        private static string ListFiles(ManagerRepository repository,string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----"+ title +"----");
            for (var i = 0; i < repository.Files.Get().Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                var prefix = "File" + i + 1 + ":  ";
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(prefix + "Name: " + repository.Files.Get()[i].Name + "Size: " +
                                  repository.Files.Get()[i].Size + "Upload date: " + repository.Files.Get()[i].UploadDate);
                if (repository.Files.Get()[i].Themes != null)
                {
                    Console.WriteLine("Themes");
                    foreach (var theme in repository.Files.Get()[i].Themes)
                    {
                        Console.WriteLine("Name: " + theme.Name);
                    }
                }
                if (repository.Files.Get()[i].Post != null)
                {
                    Console.WriteLine("Post: ");
                    Console.WriteLine("Name: " + repository.Files.Get()[i].Post.Name);
                }
            }
            Console.WriteLine(repository.Files.Get().Count+1 +".  Back");
            var var=Console.ReadLine();
            int indexPost= Int32.Parse(var);
            if (indexPost > repository.Files.Get().Count)
            {
                return "Back";
                
            }
            else
            {
                string optionSelectedPosts = repository.Files.Get()[indexPost - 1].Name;
                return optionSelectedPosts;
            }
        }

        public void ShowAllFiles(ManagerRepository repository)
        {
            var optionSelect = ListFiles(repository, "File posts");
            if (optionSelect == "Back")
            {
                ShowFileList(repository);
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
                    Console.WriteLine("Post: ");
                    Console.WriteLine("Name: " + file.Post.Name);
                }
            }

            ShowFileList(repository);
        }
    }
}