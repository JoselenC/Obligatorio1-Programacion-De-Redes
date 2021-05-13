using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using Protocol;
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
        
        private async Task SendListPostAsync(SocketHandler socketHandler)
        {
            string posts = "";
            for (int i = 0; i < repository.Posts.Count; i++)
            {
                posts +=repository.Posts[i].Name+"#";
            }
            posts += "Back" + "#";
            Packet packg = new Packet("RES", "4", posts);
            await socketHandler.SendPackgAsync(packg);
        }

        public async Task UploadFile(SocketHandler socketHandler)
        {
            if (repository.Posts.Count != 0)
            {
                await SendListPostAsync(socketHandler);
                ProtocolHandler protocolHandler = new ProtocolHandler();
                string[] fileData = await protocolHandler.ReceiveFileAsync(socketHandler);
                string option = fileData[0];
                if (option != "Back")
                {
                    File file = new File()
                    {
                        Name = fileData[2],
                        Size = Double.Parse(fileData[1]),
                        UploadDate = DateTime.Now
                      
                    };
                    Post post = repository.Posts.Find(x => x.Name == fileData[0]);
                    repository.Posts.Remove(post);
                    if (post.File == null) post.File = new File();
                    post.File = file;
                    if (file.Themes == null) file.Themes = new List<Theme>();
                    file.Themes = post.Themes;
                    if (file.Post == null) file.Post = new Post();
                    file.Post = post;
                    repository.Posts.Add(post);
                    repository.Files.Add(file);
                }
            }
            else
            {
                await SendListPostAsync(socketHandler);
            }
        }
    }
}