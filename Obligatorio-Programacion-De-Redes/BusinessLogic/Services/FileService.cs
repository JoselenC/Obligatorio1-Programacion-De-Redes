using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.IServices;
using BusinessLogic.Managers;
using DomainObjects;
using Protocol;
using ProtocolFiles;

namespace BusinessLogic.Services
{
    public class FileService : IFileService
    {
        private readonly ManagerRepository _repository;
        private readonly ManagerPostRepository _postRepository;
        private readonly RabbitHelper _rabbitClient;
        public FileService(ManagerRepository vRepository, ManagerPostRepository postRepository,RabbitHelper rabbitHelper)
        {
            _postRepository = postRepository;
            _repository = vRepository;
            this._rabbitClient = rabbitHelper;
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
            await socketHandler.SendPackageAsync(packg);
        }

        public async Task UploadFile(SocketHandler socketHandler)
        {
            if (_postRepository.Posts.Get().Count != 0)
            {
                await SendListPostAsync(socketHandler);
                ProtocolHandler protocolHandler = new ProtocolHandler();
                string[] fileData = await protocolHandler.ReceiveFileAsync(socketHandler);
                _rabbitClient.SendMessage("The file " + fileData[2]+" was uploaded in the post "+ fileData[0]);
                string option = fileData[0];
                if (option != "Back")
                {
                    File file = new File()
                    {
                        Name = fileData[2],
                        Size = Double.Parse(fileData[1]),
                        UploadDate = DateTime.Now
                      
                    };
                    UploadFileInPost(fileData, file);
                }
            }
            else
            {
                _rabbitClient.SendMessage("The file was not uploaded because there was no post");
                await SendListPostAsync(socketHandler);
            }
        }

        private void UploadFileInPost(string[] fileData, File file)
        {
            Post oldPost = _postRepository.Posts.Find(x => x.Name == fileData[0]);
            Post post = _postRepository.Posts.Find(x => x.Name == fileData[0]);
            if (post.File == null) post.File = new File();
            post.File = file;
            if (file.Themes == null) file.Themes = new List<Theme>();
            file.Themes = post.Themes;
            if (file.Post == null) file.Post = new Post();
            file.Post = post;
            _postRepository.Posts.Update(oldPost, post);
            _repository.Files.Add(file);
        }
    }
}