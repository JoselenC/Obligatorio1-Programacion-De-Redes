using System;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;
using ProtocolFiles;

namespace Domain.Services
{
    public class FileService
    {
        private MemoryRepository repository;
        public FileService(MemoryRepository repository)
        {
            this.repository = repository;
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
        
        public void UploadFile(SocketHandler socketHandler,Socket socketClient)
        {
            SendListPost(socketHandler);
            ProtocolHandler protocolHandler = new ProtocolHandler();
            string[] fileData=protocolHandler.ReceiveFile(socketClient,socketHandler);
            File file = new File()
            {
                Name = fileData[0],
                Size = Double.Parse(fileData[1])
            };
            Post post=repository.Posts.Find(x => x.Name == fileData[3]);
            repository.Posts.Remove(post);
            post.File = file;
            repository.Posts.Add(post);
            repository.Files.Add(file);
        }
    }
}