using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using LogServer;
using Protocol;
using ProtocolFiles;

namespace Domain.Services
{
    public class FileService : IFileService
    {
        private ManagerRepository repository;
        private Log log;
        public FileService(ManagerRepository vRepository,Log log)
        {
            repository = vRepository;
            this.log = log;
        }

        private async Task SendListPostAsync(SocketHandler socketHandler)
        {
            string posts = "";
            for (int i = 0; i < repository.Posts.Get().Count; i++)
            {
                posts +=repository.Posts.Get()[i].Name+"#";
            }
            posts += "Back" + "#";
            Packet packg = new Packet("RES", "4", posts);
            await socketHandler.SendPackgAsync(packg);
        }

        public async Task UploadFile(SocketHandler socketHandler)
        {
            if (repository.Posts.Get().Count != 0)
            {
                await SendListPostAsync(socketHandler);
                ProtocolHandler protocolHandler = new ProtocolHandler();
                string[] fileData = await protocolHandler.ReceiveFileAsync(socketHandler);
                log.SaveLog("Upload file" + fileData[2]);
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
                    repository.Posts.Delete(post);
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