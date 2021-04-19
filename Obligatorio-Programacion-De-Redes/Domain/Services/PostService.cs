using System;
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
            foreach (var theme in postByName.Theme)
            {
                themes += theme.Name + "#";
            }

            themes += "Back";
            socketHandler.SendMessage(themes);
        }
        
        private void SendListPost(SocketHandler socketHandler)
        {
            string posts = "";
            foreach (var post in repository.Posts)
            {
                posts += post.Name + "#";
            }

            posts += "Back";
            socketHandler.SendMessage(posts);
        }
        
        private void SendListThemes(SocketHandler socketHandler)
        {
            string posts = "";
            foreach (var post in repository.Themes)
            {
                posts += post.Name + "#";
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
                    Console.WriteLine("Post name: " + name + "Creation date" + creationDate);
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
                    if (postByName.Theme == null)
                        postByName.Theme = new List<Theme>();
                    if(postByName.Theme.Contains(theme))
                        message = "The theme " + nameTheme + " already associated";
                    else
                    {
                        postByName.Theme.Add(theme);
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
                    if (postByName.Theme == null)
                        postByName.Theme = new List<Theme>();
                    if(postByName.Theme.Contains(theme))
                        message = "The theme " + nameTheme + " already associated";
                    else
                    {
                        postByName.Theme.Add(theme);
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
                if (postByName.Theme != null && postByName.Theme.Contains(theme))
                {
                    postByName.Theme.Remove(theme);
                    postByName.Theme.Add(newTheme);
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

       public void ShowPost(SocketHandler socketHandler)
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

       public void ShowFilePost(SocketHandler socketHandler)
       {
           throw new NotImplementedException();
       }

       public void ShowThemeWithMorePosts(SocketHandler socketHandler)
       {
           throw new NotImplementedException();
       }

       public void ShowThemePostByCreationDate(SocketHandler socketHandler)
       {
           IOrderedEnumerable<Post> orderedList = repository.Posts.OrderBy((x => x.CreationDate));
           orderedList.ToList();
           string posts = "";
           foreach (var post in orderedList)
           {
               posts += post.Name + "#";
           }

           posts += "Back";
           socketHandler.SendMessage(posts);
       }
       

       public void ShowThemePostByTheme(SocketHandler socketHandler)
       {
           string[] message = socketHandler.ReceiveMessage();
           string themeName = message[0];
           List<Post> postsByTheme = new List<Post>();
           foreach (var post in repository.Posts)
           {
               foreach (var theme in post.Theme)
               {
                   if (theme.Name == themeName && !postsByTheme.Contains(post))
                   {
                       postsByTheme.Add(post);
                   }
               }
           }
           string posts = "";
           foreach (var post in postsByTheme)
           {
               posts += post.Name + "#";
           }

           posts += "Back";
           socketHandler.SendMessage(posts);
       }

       public void ShowThemePostByDateAndTheme(SocketHandler socketHandler)
       {
           string[] message = socketHandler.ReceiveMessage();
           string themeName = message[0];
           List<Post> postsByTheme = new List<Post>();
           foreach (var post in repository.Posts)
           {
               foreach (var theme in post.Theme)
               {
                   if (theme.Name == themeName && !postsByTheme.Contains(post))
                   {
                       postsByTheme.Add(post);
                   }
               }
           }

           postsByTheme.OrderBy(x => x.CreationDate);
           string posts = "";
           foreach (var post in postsByTheme)
           {
               posts += post.Name + "#";
           }

           posts += "Back";
           socketHandler.SendMessage(posts);
       }
    }
}