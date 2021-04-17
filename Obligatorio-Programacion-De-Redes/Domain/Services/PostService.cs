using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic;
using Protocol;
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

        public void AddPost(SocketHandler socketHandler)
        {
            var messageArray = socketHandler.ReceiveMessage();
            string name = messageArray[0];
            string creationDate = messageArray[1];
            string message= "";
            if (!AlreadyExistPost(name))
            {
                Console.WriteLine("Post name: " + name + "Creation date"+ creationDate);
                Post post = new Post() {Name = name, CreationDate = creationDate};
                repository.Posts.Add(post);
                message = "The post" + name + " was created";
            }
            else
            {
                message = "The post" + name + "already exist";
            }
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
        }

        private bool AlreadyExistPost(string name)
        {
            Post post = repository.Posts.Find(x => x.Name == name);
            if (post == null)
                return false;
            return true;
        }

        public void ModifyPost(SocketHandler socketHandler)
        {
            string[] messageArray = socketHandler.ReceiveMessage();
            string oldName = messageArray[0];
            string name = messageArray[1];
            string newCreationDate = messageArray[2];
            string message;
            if (AlreadyExistPost(oldName))
            {
                Post postByName = repository.Posts.Find(x => x.Name == oldName);
                repository.Posts.Remove(postByName);
                Post newPost = new Post() {Name = name, CreationDate = newCreationDate};
                repository.Posts.Add(newPost);
                message = "The post" + name + "was modified";
            }
            else
            {
                message = "The post" + name + "already exist";
            }
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
        }

        public void DeletePost(SocketHandler socketHandler)
        {
            string[] messageArray = socketHandler.ReceiveMessage();
            string name = messageArray[0];
            string message;
            if (AlreadyExistPost(name))
            {
                Post postByName = repository.Posts.Find(x => x.Name == name);
                repository.Posts.Remove(postByName);
                message = "The post" + name + "was deleted";
            }
            else
            {
                message = "The post" + name + "not exist";
            }
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
        }

        
        public void AsociateTheme(SocketHandler socketHandler)
        {
            string[] messageArray = socketHandler.ReceiveMessage();
            string namePost = messageArray[0];
            string nameTheme = messageArray[1];
            string message = "";
            if (AlreadyExistTheme(nameTheme))
            {
                Post postByName = repository.Posts.Find(x => x.Name == namePost);
                Theme theme = repository.Themes.Find(x => x.Name == nameTheme);
                if (postByName.Theme == null)
                    postByName.Theme = new List<Theme>();
                postByName.Theme.Add(theme);
                message = "The theme" + nameTheme + "was associated";
            }
            else
            {
                message = "The theme" + nameTheme + " not exist";
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

        public List<Post> OrderPostByCreationDate()
        {
            IOrderedEnumerable<Post> orderedList = repository.Posts.OrderBy((x => x.CreationDate));
            return orderedList.ToList();
        }
        
        public List<Post> OrderPostTheme()
        {
            IOrderedEnumerable<Post> orderedList = repository.Posts.OrderBy((x => x.CreationDate));
            return orderedList.ToList();
        }

        public void SearchPost(SocketHandler socketHandler)
        {
            string[] messageArray = socketHandler.ReceiveMessage();
            string namePost = messageArray[0];
            string message;
            if (AlreadyExistPost(namePost))
            {
                Post post = repository.Posts.Find(x => x.Name == namePost);
                message = post.Name + "#" + post.CreationDate;
            }
            else

            {
                message = "The post" + namePost + "not exist";
            }

            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
        }
    }
}