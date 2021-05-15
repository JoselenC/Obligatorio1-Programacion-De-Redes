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
        private SemaphoreSlim semaphoreSlim;
        public ThemeService(MemoryRepository repository)
        {
            this.repository = repository;
        }

        public ThemeService(MemoryRepository repository,SemaphoreSlim semaphore)
        {
            this.repository = repository;
            semaphoreSlim = semaphore;
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
            await SendThemesAsync(socketHandler);
            string message;
            var packet = await socketHandler.ReceivePackgAsync();
            string[] messageArray = packet.Data.Split('#');
            string option = messageArray[0];
            if (option != "Back")
            {
                message = await ModifyTheme(socketHandler, option);
                Packet packg2 = new Packet("RES", "4", message);
                await socketHandler.SendPackgAsync(packg2);
            }
        }

        private async Task<string> ModifyTheme(SocketHandler socketHandler, string option)
        {
            string message;
            if (!AlreadyExistSemaphore(option))
                repository.SemaphoreSlimThemes.Add(new SemaphoreSlimTheme()
                {
                    SemaphoreSlim = new SemaphoreSlim(1),
                    Theme = repository.Themes.Find(x => x.Name == option)
                });
            repository.SemaphoreSlimThemes.Find(x => x.Theme.Name == option).SemaphoreSlim.WaitAsync();
            var packet2 = await socketHandler.ReceivePackgAsync();
            string[] messageArray2 = packet2.Data.Split('#');
            string name = messageArray2[0];
            message = AddNewTheme(name, messageArray2, option);
            repository.SemaphoreSlimThemes.Find(x => x.Theme.Name == option).SemaphoreSlim.Release();
            return message;
        }

        private string AddNewTheme(string name, string[] messageArray2, string option)
        {
            string message;
            if (name != "")
            {
                string description = messageArray2[1];
                Theme theme = new Theme() {Name = name, Description = description};
                Theme themeName = repository.Themes.Find(x => x.Name == option);
                repository.Themes.Remove(themeName);
                if (!AlreadyExistTheme(name))
                {
                    repository.Themes.Add(theme);
                    message = "The theme " + option + " was modified";
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

            return message;
        }

        public async Task DeleteThemeAsync(SocketHandler socketHandler)
        {
            await SendThemesAsync(socketHandler);
            string message;
            var packet = await socketHandler.ReceivePackgAsync();
            string oldName = packet.Data;
            if (oldName != "Back")
            {
                if (!AlreadyExistSemaphore(oldName))
                    repository.SemaphoreSlimThemes.Add(new SemaphoreSlimTheme()
                    {
                        SemaphoreSlim = new SemaphoreSlim(1),
                        Theme = repository.Themes.Find(x => x.Name == oldName)
                    });
                repository.SemaphoreSlimThemes.Find(x => x.Theme.Name == oldName).SemaphoreSlim.WaitAsync();
                message = DeleteTheme(oldName);
                repository.SemaphoreSlimThemes.Find(x => x.Theme.Name == oldName).SemaphoreSlim.Release();
                Packet packg3 = new Packet("RES", "4", message);
                await socketHandler.SendPackgAsync(packg3);
            }
        }

        private string DeleteTheme(string oldName)
        {
            string message;
            if (AlreadyExistTheme(oldName))
            {
                Theme themeName = repository.Themes.Find(x => x.Name == oldName);
                repository.Themes.Find(x => x.Name == oldName);
                if (!IsAssociatedAPost(themeName))
                {
                    repository.Themes.Remove(themeName);
                    message = "The theme " + oldName + " was deleted";
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

            return message;
        }

        private bool AlreadyExistSemaphore(string oldName)
        {
            return repository.SemaphoreSlimThemes
                .Find(x => x.Theme.Name == oldName) != null;
        }

        private async Task SendThemesAsync(SocketHandler socketHandler)
        {
            string themes = "";
            foreach (var theme in repository.Themes)
            {
                SemaphoreSlimTheme semaphoreSlimTheme = repository.SemaphoreSlimThemes
                    .Find(x => x.Theme.Name == theme.Name);

                if (semaphoreSlimTheme == null || semaphoreSlimTheme.SemaphoreSlim.CurrentCount > 0)
                {
                    themes += theme.Name + "#";
                }
            }
            themes += "Back" + "#";
            Packet packg = new Packet("RES", "4", themes);
            await socketHandler.SendPackgAsync(packg);
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