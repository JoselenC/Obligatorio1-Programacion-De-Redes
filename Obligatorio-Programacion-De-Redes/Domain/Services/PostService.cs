using System;
using System.Collections.Generic;
using BusinessLogic;
using Library;
using Protocol;

namespace Domain.Services
{
    public class PostService
    {
        private MemoryRepository repository;
        public PostService(MemoryRepository repository)
        {
            this.repository = repository;
        }
        public void AddPost(SocketHandler socketHandler,HeaderHandler headerHandler)
        {
            var dataLength = socketHandler.Receive(HeaderConstants.DataLength);
            int length = BitConverter.ToInt32(dataLength);
            var data = socketHandler.Receive(length);
            string message = System.Text.Encoding.UTF8.GetString(data);
            string[] messageArray = message.Split("#");
            string name = messageArray[0];
            string creationDate = messageArray[1];
            Console.WriteLine("Post name: " + name + "Creation date"+ creationDate);
            Post post = new Post() {Name = name, CreationDate = creationDate};
            repository.Posts.Add(post);
        }

        public string ReceiveString(SocketHandler socketHandler)
        {
            return "";
        }
        
        public void ModifyPost(SocketHandler socketHandler)
        {
            string name = ReceiveString(socketHandler);
            List<Post> postByName= repository.Posts.FindAll(x => x.Name == name);
            foreach (var post in postByName)
            {
                Post newPost = new Post(); //aca cargar el nuevo post
                repository.Posts.Remove(post);
                repository.Posts.Add(newPost);

            }
        }

        public void DeletePost(SocketHandler socketHandler)
        {
            string name = ReceiveString(socketHandler);
            List<Post> postByName= repository.Posts.FindAll(x => x.Name == name);
            foreach (var post in postByName)
            {
                repository.Posts.Remove(post);

            }
        }

        public void AsociateTheme()
        {
            throw new System.NotImplementedException();
        }
    }
}