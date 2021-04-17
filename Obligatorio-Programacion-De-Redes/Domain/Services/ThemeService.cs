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
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
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
            string[] messageArray = socketHandler.ReceiveMessage();
            string oldName = messageArray[0];
            string name = messageArray[1];
            string description = messageArray[2];
            string message;
            if (AlreadyExistTheme(oldName))
            {
                Theme theme = new Theme() {Name = name, Description = description};
                if (!AlreadyExistTheme(name))
                {
                    Theme themeName = repository.Themes.Find(x => x.Name == oldName);
                    repository.Themes.Remove(themeName);
                    repository.Themes.Add(theme);
                    message = "The theme " + name + " was modify";
                }
                else
                {
                    message = "The theme " + name + " already exist";
                }
            }
            else
            {
                message = "The theme " + name + " not exist";
            }
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
        }

        public void DeleteTheme(SocketHandler socketHandler)
        {
            string[] messageArray = socketHandler.ReceiveMessage();
            string name = messageArray[0];
            string message;
            if (AlreadyExistTheme(name))
            {
                Theme themeByName = repository.Themes.Find(x => x.Name == name);
                if (!IsAssociatedAPost(themeByName))
                {
                    repository.Themes.Remove(themeByName);
                    message = "The theme " + name + " was deleted";
                }
                else
                {
                    message = "The theme " + name + " was not deleted because have a post";
                }
            }
            else
            {
                message = "The theme " + name + " not exist";
            }

            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
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
    }
}