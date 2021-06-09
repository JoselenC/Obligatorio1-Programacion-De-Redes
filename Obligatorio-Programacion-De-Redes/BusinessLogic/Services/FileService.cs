using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Managers;
using DataHandler;
using Domain;
using LogServer;
using Protocol;
using ProtocolFiles;

namespace BusinessLogic.Services
{
    public class FileService : IFileService
    {
        private ManagerRepository repository;
        private ManagerPostRepository _postRepository;
        private Log log;
        public FileService(ManagerRepository vRepository, ManagerPostRepository postRepository,Log log)
        {
            _postRepository = postRepository;
            repository = vRepository;
            this.log = log;
        }

        private async Task SendListPostAsync(SocketHandler socketHandler)
        {
            string posts = "";
            for (int i = 0; i < _postRepository.Posts.Get().Count; i++)
            {
                posts +=_postRepository.Posts.Get()[i].Name+"#";
            }
            posts += "Back" + "#";
            Packet packg = new Packet("RES", "4", posts);
            await socketHandler.SendPackgAsync(packg);
        }

        public async Task UploadFile(SocketHandler socketHandler)
        {
            if (_postRepository.Posts.Get().Count != 0)
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
                    Post oldPost = _postRepository.Posts.Find(x => x.Name == fileData[0]);
                    Post post = _postRepository.Posts.Find(x => x.Name == fileData[0]);
                    if (post.File == null) post.File = new File();
                    post.File = file;
                    if (file.Themes == null) file.Themes = new List<Theme>();
                    file.Themes = post.Themes;
                    if (file.Post == null) file.Post = new Post();
                    file.Post = post;
                    _postRepository.Posts.Update(oldPost,post);
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