using System;
using BusinessLogic;
using DataHandler;
using Protocol;

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
            var packet = socketHandler.ReceivePackg();
            String[] messageArray = packet.Data.Split('#');
            string name = messageArray[0];
            if (name != "Back")
            {
                string description = messageArray[1].Split('\0')[0]; ;
                string message;

                if (name != "")
                {
                    if (!AlreadyExistTheme(name))
                    {
                        Theme theme = new Theme() { Name = name, Description = description, InUse = false };
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
                Packet packg = new Packet("REQ", "4", message);
                socketHandler.SendPackg(packg);
            }
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
            string posts = "";
            foreach (var post in repository.Themes)
            {
                posts += post.Name + "#";
            }
            posts += "Back";
            Packet packg = new Packet("REQ", "4", posts);
            socketHandler.SendPackg(packg);
            string message;
            var packet = socketHandler.ReceivePackg();
            string[] messageArray = packet.Data.Split('#');
            string option = messageArray[0].Split("\0")[0];
            if (option != "Back")
            {
                string name = messageArray[1];
                if (name != "")
                {
                    string description = messageArray[2].Split("\0")[0];
                    Theme theme = new Theme() { Name = name, Description = description, InUse = false };
                    if (!AlreadyExistTheme(name))
                    {
                        Theme themeName = repository.Themes.Find(x => x.Name == option);
                        if (!theme.InUse)
                        {
                            repository.Themes.Find(x => x.Name == option).InUse = true;
                            repository.Themes.Remove(themeName);
                            repository.Themes.Add(theme);
                            message = "The theme " + option + "was modified";
                        }
                        else
                        {
                            message = "The theme " + option + "is in use";
                        }
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
                Packet packg2 = new Packet("REQ", "4", message);
                socketHandler.SendPackg(packg2);
            }
        }

        public void DeleteTheme(SocketHandler socketHandler)
        {
            string posts = "";
            foreach (var post in repository.Themes)
            {
                posts += post.Name + "#";
            }
            posts += "Back";
            Packet packg = new Packet("REQ", "4", posts);
            socketHandler.SendPackg(packg);
            string message;
            var packet = socketHandler.ReceivePackg();
            string oldName = packet.Data.Split('\0')[0];
            if (oldName != "Back")
            {
                if (AlreadyExistTheme(oldName))
                {
                    Theme themeName = repository.Themes.Find(x => x.Name == oldName);
                    if (!themeName.InUse)
                    {
                        repository.Themes.Find(x => x.Name == oldName).InUse = true;
                        if (!IsAssociatedAPost(themeName))
                        {
                            repository.Themes.Remove(themeName);
                            message = "The theme " + oldName + " was deleted";
                        }
                        else
                        {
                            message = "The theme " + oldName + " is associated with a post";
                            Packet packg2 = new Packet("REQ", "4", message);
                            socketHandler.SendPackg(packg2);
                        }
                    }
                    else
                    {
                        message = "The theme " + oldName + " in use";
                    }

                }
                else
                {
                    message = "The theme " + oldName + " not exist";
                }
                Packet packg3 = new Packet("REQ", "4", message);
                socketHandler.SendPackg(packg3);
            }
        }

        private bool IsAssociatedAPost(Theme theme)
        {
            foreach (var post in repository.Posts)
            {
                if (post.Themes.Contains(theme))
                    return true;
            }
            return false;
        }

        private void SendListThemes(SocketHandler socketHandler)
        {
            string posts = "";
            for (int i = 0; i < repository.Themes.Count; i++)
            {
                posts +=repository.Themes[i].Name + "#";
            }
            posts += "Back";
            Packet packg = new Packet("REQ", "4", posts);
            socketHandler.SendPackg(packg);
        }
        
    }
}