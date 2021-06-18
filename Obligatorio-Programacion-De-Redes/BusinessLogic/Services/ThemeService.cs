using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.Managers;
using DataHandler;
using Domain;
using Protocol;

namespace BusinessLogic.Services
{
    public class ThemeService : IThemeService
    {
        
        private ManagerRepository repository;
        private ManagerThemeRepository _themeRepository;
        private ManagerPostRepository _postRepository;
        private RabbitHelper rabbitClient;
        private SemaphoreSlim semaphoreSlim;
        public ThemeService(ManagerRepository vRepository, RabbitHelper rabbitHelper,ManagerPostRepository managerPostRepository, 
        ManagerThemeRepository managerThemeRepository)
        {
            _postRepository = managerPostRepository;
            _themeRepository = managerThemeRepository;
            repository = vRepository;
            this.rabbitClient = rabbitHelper;
        }
    
        public async Task AddThemeAsync(SocketHandler socketHandler)
        {
            var packet = await socketHandler.ReceivePackgAsync();
            String[] messageArray = packet.Data.Split('#');
            string name = messageArray[0];
            rabbitClient.SendMessage("New theme" + name);
            if (name != "Back")
            {
                string description = messageArray[1];
                string message;

                if (name != "")
                {
                    if (!AlreadyExistTheme(name))
                    {
                        Theme theme = new Theme() { Name = name, Description = description };
                        _themeRepository.Themes.Add(theme);
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
            try
            {
                Theme theme = _themeRepository.Themes.Find(x => x.Name == name);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
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
        //    if (!AlreadyExistSemaphore(option))
           //     repository.SemaphoreSlimThemes.Add(new SemaphoreSlimTheme()
             //   {
               //     SemaphoreSlim = new SemaphoreSlim(1),
                 //   Theme = _themeRepository.Themes.Find(x => x.Name == option)
               // });
         //   repository.SemaphoreSlimThemes.Find(x => x.Theme.Name == option).SemaphoreSlim.WaitAsync();
            var packet2 = await socketHandler.ReceivePackgAsync();
            string[] messageArray2 = packet2.Data.Split('#');
            string name = messageArray2[0];
            message = AddNewTheme(name, messageArray2, option);
          //  repository.SemaphoreSlimThemes.Find(x => x.Theme.Name == option).SemaphoreSlim.Release();
            return message;
        }

        private string AddNewTheme(string name, string[] messageArray2, string option)
        {
            string message;
            if (name != "")
            {
                string description = messageArray2[1];
                Theme theme = new Theme() {Name = name, Description = description};
                Theme themeName = _themeRepository.Themes.Find(x => x.Name == option);
                if (!AlreadyExistTheme(name))
                {
                    _themeRepository.Themes.Update(themeName,theme);
                    message = "The theme " + option + " was modified";
                    rabbitClient.SendMessage("Modify theme" + themeName + "new name: "+ theme);
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
             //   if (!AlreadyExistSemaphore(oldName))
              //  {
                //    repository.SemaphoreSlimThemes.Add(new SemaphoreSlimTheme()
                  //  {
                    //    SemaphoreSlim = new SemaphoreSlim(1),
                      //  Theme = _themeRepository.Themes.Find(x => x.Name == oldName)
                   // });
              //  }

           //     repository.SemaphoreSlimThemes.Find(x => x.Theme.Name == oldName).SemaphoreSlim.WaitAsync();
                message = DeleteTheme(oldName);
             //   repository.SemaphoreSlimThemes.Find(x => x.Theme.Name == oldName).SemaphoreSlim.Release();
                Packet packg3 = new Packet("RES", "4", message);
                await socketHandler.SendPackgAsync(packg3);
            }
        }

        private string DeleteTheme(string oldName)
        {
            string message;
            if (AlreadyExistTheme(oldName))
            {
                Theme themeName = _themeRepository.Themes.Find(x => x.Name == oldName);
                _themeRepository.Themes.Find(x => x.Name == oldName);
                if (!IsAssociatedAPost(themeName))
                {
                    rabbitClient.SendMessage("Delete theme" + themeName);
                    _themeRepository.Themes.Delete(themeName);
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
            try
            {
                repository.SemaphoreSlimThemes
                    .Find(x => x.Theme.Name == oldName);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        private async Task SendThemesAsync(SocketHandler socketHandler)
        {
            string themes = "";
            foreach (var theme in _themeRepository.Themes.Get())
            {
                try
                {
                  //  SemaphoreSlimTheme semaphoreSlimTheme = repository.SemaphoreSlimThemes
                   //     .Find(x => x.Theme.Name == theme.Name);
                }
                catch (KeyNotFoundException)
                {
                    
                }
                themes += theme.Name + "#";
            }
            themes += "Back" + "#";
            Packet packg = new Packet("RES", "4", themes);
            await socketHandler.SendPackgAsync(packg);
        }

        private bool IsAssociatedAPost(Theme theme)
        {
            foreach (var post in _postRepository.Posts.Get())
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