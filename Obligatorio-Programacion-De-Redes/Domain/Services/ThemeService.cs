using System;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using Protocol;

namespace Domain.Services
{
    public class ThemeService
    {
        private MemoryRepository repository;
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);
        public ThemeService(MemoryRepository repository)
        {
            this.repository = repository;
        }
        public async Task AddThemeAsync(SocketHandler socketHandler)
        {
            var packet = await socketHandler.ReceivePackgAsync();
            String[] messageArray = packet.Data.Split('#');
            string name = messageArray[0];
            if (name != "Back")
            {
                string description = messageArray[1];
                string message;

                if (name != "")
                {
                    if (!AlreadyExistTheme(name))
                    {
                        Theme theme = new Theme() { Name = name, Description = description };
                        repository.Themes.Add(theme);
                        message = "The theme " + name + " was added";
                    }
                    else
                    {
                        message = "Not add, the theme " + name + " already exist";
                    }
                }
                else
                {
                    message = "The theme name cannot be empty";
                }
                Packet packg = new Packet("RES", "4", message);
                await socketHandler.SendPackgAsync(packg);
            }
        }
        
        private bool AlreadyExistTheme(string name)
        {
            Theme theme = repository.Themes.Find(x => x.Name == name);
            if (theme == null)
                return false;
            return true;
        }

        public async Task ModifyThemeAsync(SocketHandler socketHandler)
        {
            string posts = "";
            foreach (var post in repository.Themes)
            {
                posts += post.Name + "#";
            }
            posts += "Back" + "#";
            Packet packg = new Packet("RES", "4", posts);
            await socketHandler.SendPackgAsync(packg);
            string message;
            var packet = await socketHandler.ReceivePackgAsync();
            string[] messageArray = packet.Data.Split('#');
            string option = messageArray[0];
            if (option != "Back")
            {
                string name = messageArray[1];
                if (name != "")
                {
                    string description = messageArray[2];
                    Theme theme = new Theme() { Name = name, Description = description };
                    if (!AlreadyExistTheme(name))
                    {
                        Theme themeName = repository.Themes.Find(x => x.Name == option);
                        semaphoreSlim.Wait();
                        repository.Themes.Find(x => x.Name == option);
                        repository.Themes.Remove(themeName);
                        repository.Themes.Add(theme);
                        message = "The theme " + option + " was modified";
                        semaphoreSlim.Release();
                    }
                    else
                    {
                        message = "Not modify, the theme " + name + " already exist";
                    }
                }
                else
                {
                    message = "The theme name cannot be empty";
                }
                Packet packg2 = new Packet("RES", "4", message);
                await socketHandler.SendPackgAsync(packg2);
            }
        }

        public async Task DeleteThemeAsync(SocketHandler socketHandler)
        {
            string posts = "";
            foreach (var post in repository.Themes)
            {
                posts += post.Name + "#";
            }
            posts += "Back" + "#";
            Packet packg = new Packet("RES", "4", posts);
            await socketHandler.SendPackgAsync(packg);
            string message;
            var packet = await socketHandler.ReceivePackgAsync();
            string oldName = packet.Data;
            if (oldName != "Back")
            {
                if (AlreadyExistTheme(oldName))
                {
                    Theme themeName = repository.Themes.Find(x => x.Name == oldName);
                    repository.Themes.Find(x => x.Name == oldName);
                    if (!IsAssociatedAPost(themeName))
                    {
                        semaphoreSlim.Wait();
                        repository.Themes.Remove(themeName);
                        message = "The theme " + oldName + " was deleted";
                        semaphoreSlim.Wait();
                    }
                    else
                    {
                        message = "Not delete, the theme " + oldName + " is associated with a post";
                    }
                }
                else
                {
                    message = "Not delete, the theme " + oldName + " not exist";
                }
                Packet packg3 = new Packet("RES", "4", message);
                await socketHandler.SendPackgAsync(packg3);
            }
        }

        private bool IsAssociatedAPost(Theme theme)
        {
            foreach (var post in repository.Posts)
            {
                if (post.Themes != null)
                {
                    if (post.Themes.Contains(theme))
                        return true;
                }
            }
            return false;
        }
    }
}