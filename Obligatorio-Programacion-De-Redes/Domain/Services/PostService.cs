﻿using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic;
using DataHandler;

namespace Domain.Services
{
    public class PostService
    {
        private MemoryRepository repository;
        public PostService(MemoryRepository repository)
        {
            this.repository = repository;
        }

        private void SendListThemesPost(SocketHandler socketHandler, string namePost)
        {
            Post postByName = repository.Posts.Find(x => x.Name == namePost);
            string themes = "";
            for (int i = 0; i < postByName.Themes.Count; i++)
            {
                themes +=postByName.Themes[i].Name + "#";
            }
            themes += "Back";
            socketHandler.SendMessage(themes);
        }
        
        private void SendListPost(SocketHandler socketHandler)
        {
            string posts = "";
            for (int i = 0; i < repository.Posts.Count; i++)
            {
                posts +=repository.Posts[i].Name + "#";
            }
            posts += "Back";
            socketHandler.SendMessage(posts);
        }
        
        private void SendListThemes(SocketHandler socketHandler)
        {
            string posts = "";
            for (int i = 0; i < repository.Themes.Count; i++)
            {
                posts +=repository.Themes[i].Name + "#";
            }
            posts += "Back";
            socketHandler.SendMessage(posts);
        }

        private bool AlreadyExistTheme(string name)
        {
            Theme theme = repository.Themes.Find(x => x.Name == name);
            if (theme == null)
                return false;
            return true;
        }
        
        private bool AlreadyExistPost(string name)
        {
            Post post = repository.Posts.Find(x => x.Name == name);
            if (post == null)
                return false;
            return true;
        }
        
        public void AddPost(SocketHandler socketHandler)
        {
            var messageArray = socketHandler.ReceiveMessage();
            string name = messageArray[0];
            string creationDate = messageArray[1];
            string message= "";
            if (name != "")
            {
                if (!AlreadyExistPost(name))
                {
                    Post post = new Post() {Name = name, CreationDate = creationDate};
                    repository.Posts.Add(post);
                    message = "The post " + name + " was created";
                }
                else
                {
                    message = "The post " + name + " already exist";
                }
            }
            else
            {
                message = "The post name cannot be empty";
            }
            socketHandler.SendMessage(message);
        }
        
        public void ModifyPost(SocketHandler socketHandler)
        {
            SendListPost(socketHandler);
            string[] messageArray = socketHandler.ReceiveMessage();
            string oldName = messageArray[0];
            string message;
            if (oldName!= "Back")
            {
                string name = messageArray[1];
                if (name != "")
                {
                    string newCreationDate = messageArray[2];
                    if (AlreadyExistPost(oldName))
                    {
                        Post postByName = repository.Posts.Find(x => x.Name == oldName);
                        repository.Posts.Remove(postByName);
                        Post newPost = new Post() {Name = name, CreationDate = newCreationDate};
                        repository.Posts.Add(newPost);
                        message = "The post " + name + " was modified";
                    }
                    else
                    {
                        message = "The post " + name + " already exist";
                    }
                }
                else
                {
                    message = "The theme name cannot be empty";
                }
                socketHandler.SendMessage(message);
            }
        }

        public void DeletePost(SocketHandler socketHandler)
        {
            SendListPost(socketHandler);
            string[] messageArray = socketHandler.ReceiveMessage();
            string name = messageArray[0];
            if (name != "Back")
            {
                string message;
                if (AlreadyExistPost(name))
                {
                    Post postByName = repository.Posts.Find(x => x.Name == name);
                    repository.Posts.Remove(postByName);
                    message = "The post" + name + "was deleted";
                }
                else
                {
                    message = "The post" + name + " does not exist";
                }
                socketHandler.SendMessage(message);
            }
        }
        
        public void AsociateTheme(SocketHandler socketHandler)
        {
            SendListPost(socketHandler);
            SendListThemes(socketHandler);
            string[] messageArray = socketHandler.ReceiveMessage();
            string namePost = messageArray[0];
            string message = "";
            if (namePost != "Back")
            {
                string nameTheme = messageArray[1];
                if (AlreadyExistTheme(nameTheme))
                {
                    Post postByName = repository.Posts.Find(x => x.Name == namePost);
                    Theme theme = repository.Themes.Find(x => x.Name == nameTheme);
                    if (postByName.Themes == null)
                        postByName.Themes = new List<Theme>();
                    if(postByName.Themes.Contains(theme))
                        message = "The theme " + nameTheme + " already associated";
                    else
                    {
                        postByName.Themes.Add(theme);
                        message = "The theme " + nameTheme + " was associated";
                    }
                }
                else
                {
                    message = "The theme " + nameTheme + " does not exist";
                }
                socketHandler.SendMessage(message);
            }
        }
        
        public void AsociateThemeToPost(SocketHandler socketHandler)
        {
            SendListThemes(socketHandler);
            string[] messageArray = socketHandler.ReceiveMessage();
            string namePost = messageArray[0];
            string message = "";
            if (namePost != "Back")
            {
                string nameTheme = messageArray[1];
                if (AlreadyExistTheme(nameTheme))
                {
                    Post postByName = repository.Posts.Find(x => x.Name == namePost);
                    Theme theme = repository.Themes.Find(x => x.Name == nameTheme);
                    if (postByName.Themes == null)
                        postByName.Themes = new List<Theme>();
                    if(postByName.Themes.Contains(theme))
                        message = "The theme " + nameTheme + " already associated";
                    else
                    {
                        postByName.Themes.Add(theme);
                        message = "The theme " + nameTheme + " was associated";
                    }
                }
                else
                {
                    message = "The theme " + nameTheme + " does not exist";
                }
                socketHandler.SendMessage(message);
            }
        }

       public void SearchPost(SocketHandler socketHandler)
        {
            SendListPost(socketHandler);
            string[] messageArray = socketHandler.ReceiveMessage();
            string namePost = messageArray[0];
            if (namePost != "Back")
            {
                string message;
                if (AlreadyExistPost(namePost))
                {
                    Post post = repository.Posts.Find(x => x.Name == namePost);
                    message = post.Name + "#" + post.CreationDate;
                }
                else

                {
                    message = "The post " + namePost + " does not exist";
                } 
                socketHandler.SendMessage(message);
            }
        }

        public void DisassociateTheme(SocketHandler socketHandler)
        {
            SendListPost(socketHandler);
            string[] namePost = socketHandler.ReceiveMessage();
            SendListThemesPost(socketHandler,namePost[0]);
            string posts = "";
            foreach (var post in repository.Themes)
            {
                posts += post.Name + "#";
            }
            socketHandler.SendMessage(posts);
            string[] messageArray = socketHandler.ReceiveMessage();
            string postName = messageArray[0];
            if (postName != "Back")
            {
                string nameThemeDisassociate = messageArray[1];
                string nameNewTheme = messageArray[2];
                string message = "";
                if (AlreadyExistPost(postName))
                {
                    message = Disassociate(postName, nameThemeDisassociate, nameNewTheme);
                }
                else
                {
                    message = "The post " + postName + " does not exist";
                }
                socketHandler.SendMessage(message);
            }
        }
        
        private string Disassociate(string postName, string nameThemeDisassociate, string nameNewTheme)
        {
            string message;
            Post postByName = repository.Posts.Find(x => x.Name == postName);
            if (AlreadyExistTheme(nameThemeDisassociate) && AlreadyExistTheme(nameNewTheme))
            {
                Theme theme = repository.Themes.Find(x => x.Name == nameThemeDisassociate);
                Theme newTheme = repository.Themes.Find(x => x.Name == nameNewTheme);
                if (postByName.Themes != null && postByName.Themes.Contains(theme))
                {
                    postByName.Themes.Remove(theme);
                    postByName.Themes.Add(newTheme);
                }

                message = "The theme " + nameThemeDisassociate + " was disassociate and " + nameNewTheme +
                          " was associate";
            }
            else if (!AlreadyExistTheme(nameThemeDisassociate))
            {
                message = "The theme " + nameThemeDisassociate + " does not exist";
            }
            else
            {
                message = "The theme " + nameNewTheme + " does not exist";
            }

            return message;
        }

       
    }
}