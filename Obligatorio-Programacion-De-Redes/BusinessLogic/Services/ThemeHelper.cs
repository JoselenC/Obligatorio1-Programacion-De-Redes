using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using BusinessLogic.Managers;
using DomainObjects;
using Protocol;

namespace BusinessLogic.Services
{
    public class ThemeHelper
    {
        public bool AlreadyExistTheme(string name, ManagerThemeRepository themeRepository)
        {
            try
            {
                themeRepository.Themes.Find(x => x.Name == name);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public async Task<string> ModifyTheme(SocketHandler socketHandler, string option,
            ManagerThemeRepository themeRepository, RabbitHelper rabbitClient)
        {
            var packet2 = await socketHandler.ReceivePackageAsync();
            string[] messageArray2 = packet2.Data.Split('#');
            string name = messageArray2[0];
            var message = AddNewTheme(name, messageArray2, option, themeRepository, rabbitClient);
            return message;
        }

        private string AddNewTheme(string name, string[] messageArray2, string option,
            ManagerThemeRepository themeRepository, RabbitHelper rabbitClient)
        {
            string message;
            if (name != "")
            {
                string description = messageArray2[1];
                Theme theme = new Theme() {Name = name, Description = description};
                Theme themeName = themeRepository.Themes.Find(x => x.Name == option);
                if (!AlreadyExistTheme(name, themeRepository))
                {
                    lock (themeRepository.Themes)
                    {
                        themeRepository.Themes.Update(themeName, theme);
                        message = "The theme " + option + " was modified";
                    }
                }
                else
                {
                    message = "Not modify, the theme " + name + " already exist";
                }
            }
            else
            {
                message = "The theme " + name + " cannot be empty";
            }
            rabbitClient.SendMessage(message+"#"+"theme"+ "#" + name);
            return message;
        }

        public string DeleteTheme(string oldName, ManagerThemeRepository themeRepository, RabbitHelper rabbitClient,
            ManagerPostRepository postRepository)
        {
            string message;
            if (AlreadyExistTheme(oldName, themeRepository))
            {
                Theme themeName = themeRepository.Themes.Find(x => x.Name == oldName);
                themeRepository.Themes.Find(x => x.Name == oldName);
                if (!IsAssociatedAPost(themeName, postRepository))
                {
                    lock (themeRepository.Themes)
                    {
                        themeRepository.Themes.Delete(themeName);
                    }
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
            rabbitClient.SendMessage(message+"#"+"theme"+ "#" + oldName);
            return message;
        }

        public async Task SendThemesAsync(SocketHandler socketHandler, ManagerThemeRepository themeRepository)
        {
            string themes = "";
            foreach (var theme in themeRepository.Themes.Get())
            {
                themes += theme.Name + "#";
            }

            themes += "Back" + "#";
            Packet package = new Packet("RES", "4", themes);
            await socketHandler.SendPackageAsync(package);
        }

        private bool IsAssociatedAPost(Theme theme, ManagerPostRepository postRepository)
        {
            foreach (var post in postRepository.Posts.Get())
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