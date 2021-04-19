using System;
using BusinessLogic;
using DataHandler;

namespace Domain.Services
{
    public class ThemeService
    {
        
        private MemoryRepository repository;
        public ThemeService(MemoryRepository repository)
        {
            this.repository = repository;
        }
        public void AddTheme(SocketHandler socketHandler)
        {
            var messageArray = socketHandler.ReceiveMessage();
            string name = messageArray[0];
            string description = messageArray[1];
            string message;
            if (name != "")
            {
                if (!AlreadyExistTheme(name))
                {
                    Theme theme = new Theme() {Name = name, Description = description};
                    repository.Themes.Add(theme);
                    message = "The theme " + name + " was added";
                }
                else
                {
                    message = "The theme " + name + " already exist";
                }
            }
            else
            {
                message = "The theme name cannot be empty";
            }
            socketHandler.SendMessage(message);
        }
        
        private bool AlreadyExistTheme(string name)
        {
            Theme theme = repository.Themes.Find(x => x.Name == name);
            if (theme == null)
                return false;
            return true;
        }

        public void ModifyTheme(SocketHandler socketHandler)
        {
            string posts="";
            foreach (var post in repository.Themes)
            {
                posts += post.Name + "#";
            }
            posts+="Back";
            socketHandler.SendMessage(posts);
            string message;
            string[] messageArray = socketHandler.ReceiveMessage();
            string option = messageArray[0];
            if (option != "Back")
            {
                string name = messageArray[1];
                if (name != "")
                {
                    string description = messageArray[2];
                    Theme theme = new Theme() {Name = name, Description = description};
                    if (!AlreadyExistTheme(name))
                    {
                        Theme themeName = repository.Themes.Find(x => x.Name == option);
                        repository.Themes.Remove(themeName);
                        repository.Themes.Add(theme);
                        message = "The theme " + option + " was modified";
                    }
                    else
                    {
                        message = "The theme " + name + " already exist";
                    }
                }
                else
                {
                    message = "The theme name cannot be empty";
                }

                socketHandler.SendMessage(message);
            }
        }

        public void DeleteTheme(SocketHandler socketHandler)
        {
            string posts="";
            foreach (var post in repository.Themes)
            {
                posts += post.Name + "#";
            }
            posts+="Back";
            socketHandler.SendMessage(posts);
            string message;
            string[] messageArray = socketHandler.ReceiveMessage();
            string oldName = messageArray[0];
            if (oldName != "Back")
            {
                if (AlreadyExistTheme(oldName))
                {
                    Theme themeName = repository.Themes.Find(x => x.Name == oldName);
                    if (!IsAssociatedAPost(themeName))
                    {
                        repository.Themes.Remove(themeName);
                    }
                    else
                    {
                        message = "The theme " + oldName + " is associated with a post";
                        socketHandler.SendMessage(message);
                    }
                    message = "The theme " + oldName + " was deleted";
                }
                else
                {
                    message = "The theme " + oldName + " not exist";
                }
                socketHandler.SendMessage(message);
            }
        }

        private bool IsAssociatedAPost(Theme theme)
        {
            foreach (var post in repository.Posts)
            {
                if (post.Theme.Contains(theme))
                    return true;
            }
            return false;
        }

        public void ShowThemeList(SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }
    }
}