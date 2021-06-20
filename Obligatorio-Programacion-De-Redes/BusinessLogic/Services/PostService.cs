using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.IServices;
using BusinessLogic.Managers;
using DomainObjects;
using Protocol;

namespace BusinessLogic.Services
{
    public class PostService : IPostService
    {
        private readonly ManagerPostRepository _postRepository;
        private readonly ManagerThemeRepository _themeRepository;
        private readonly RabbitHelper _rabbitClient;
        private readonly PostHelper _postHelper;

        public PostService(RabbitHelper rabbitHelper, ManagerPostRepository managerPostRepository,
            ManagerThemeRepository managerThemeRepository)
        {
            _postHelper = new PostHelper();
            _postRepository = managerPostRepository;
            _themeRepository = managerThemeRepository;
            _rabbitClient = rabbitHelper;
        }

        public async Task AddPostAsync(SocketHandler socketHandler)
        {
            Packet package = new Packet("RES", "2", _themeRepository.Themes.Get().Count.ToString());
            await socketHandler.SendPackageAsync(package);
            string name = "";
            if (_themeRepository.Themes.Get().Count > 0)
            {
                var packet = await socketHandler.ReceivePackageAsync();
                String[] messageArray = packet.Data.Split('#');
                name = messageArray[0];
                string creationDate = messageArray[1];
                var message = "";
                if (name != "")
                {
                    if (!_postHelper.AlreadyExistPost(name, _postRepository))
                    {
                        lock (_postRepository.Posts)
                        {
                            Post post = new Post() {Name = name, CreationDate = creationDate};
                            _postRepository.Posts.Add(post);
                            message = "The post " + name + " was created";
                        }
                    }
                    else
                    {
                        message = "Not added, the post " + name + " already exist";
                    }
                }
                else
                {
                    message = "The post name cannot be empty";
                }
                _rabbitClient.SendMessage(message+"#"+"post"+ "#" + name);
                Packet package2 = new Packet("RES", "2", message);
                await socketHandler.SendPackageAsync(package2);
            }
        }

        public async Task DeletePostAsync(SocketHandler socketHandler)
        {
            await _postHelper.SendListPostAsync(socketHandler, _postRepository);
            var packet = await socketHandler.ReceivePackageAsync();
            string name = packet.Data;
            if (name != "Back")
            {
                _rabbitClient.SendMessage("Delete post" +"#"+"post"+ "#" +  packet.Data);
                var message = _postHelper.DeletePost(name, _postRepository);
                Packet package = new Packet("RES", "2", message);
                await socketHandler.SendPackageAsync(package);
            }
        }

        public async Task ModifyPostAsync(SocketHandler socketHandler)
        {
            await _postHelper.SendListPostAsync(socketHandler, _postRepository);
            var packet = await socketHandler.ReceivePackageAsync();
            string[] messageArray = packet.Data.Split('#');
            string oldName = messageArray[0];

            if (oldName != "Back")
            {
                var message = await _postHelper.ModifyPost(socketHandler, oldName, _postRepository, _rabbitClient);
                Packet package = new Packet("RES", "2", message);
                await socketHandler.SendPackageAsync(package);
            }
        }

        public async Task AssociateThemeAsync(SocketHandler socketHandler)
        {
            await new PostHelper().SendListPostAsync(socketHandler, _postRepository);
            await new PostHelper().SendListThemesAsync(socketHandler, _themeRepository);
            var packet = await socketHandler.ReceivePackageAsync();
            String[] messageArray = packet.Data.Split('#');
            string namePost = messageArray[0];
            string nameTheme = "";
            if (namePost != "Back")
            {
                nameTheme = messageArray[1];
                var message = "";
                if (_postHelper.AlreadyExistTheme(nameTheme, _themeRepository))
                {
                    Post postByName = _postRepository.Posts.Find(x => x.Name == namePost);
                    Theme theme = _themeRepository.Themes.Find(x => x.Name == nameTheme);
                    if (postByName.Themes == null)
                        postByName.Themes = new List<Theme>();
                    if (postByName.Themes.Contains(theme))
                        message = "Not associated, the theme " + nameTheme + " already associated";
                    else
                    {
                        _themeRepository.Themes.Delete(theme);
                        theme.Posts ??= new List<Post>();
                        if (!theme.Posts.Contains(postByName))
                            theme.Posts.Add(postByName);
                        _themeRepository.Themes.Add(theme);
                        postByName.Themes.Add(theme);
                        message = "The theme " + nameTheme + " was associated";
                    }
                }
                else
                {
                    message = "Not associated, the theme " + nameTheme + " does not exist";
                }
                _rabbitClient.SendMessage(message+"#"+"post"+ "#" +  nameTheme);
                Packet package = new Packet("RES", "2", message);
                await socketHandler.SendPackageAsync(package);
            }
        }

        public async Task AssociateThemeToPostAsync(SocketHandler socketHandler)
        {
            await _postHelper.SendListThemesToPostAsync(socketHandler, _themeRepository);
            var packet = await socketHandler.ReceivePackageAsync();
            String[] messageArray = packet.Data.Split('#');
            string namePost = messageArray[0];
            string message = "";
            string nameTheme = messageArray[1];
            if (_postHelper.AlreadyExistTheme(nameTheme, _themeRepository))
            {
                Post postByName = _postRepository.Posts.Find(x => x.Name == namePost);
                Theme oldTheme = _themeRepository.Themes.Find(x => x.Name == nameTheme);
                Theme theme = _themeRepository.Themes.Find(x => x.Name == nameTheme);
                theme.Posts ??= new List<Post>();
                if (!theme.Posts.Contains(postByName))
                    theme.Posts.Add(postByName);
                _themeRepository.Themes.Update(oldTheme, theme);
                postByName.Themes ??= new List<Theme>();
                if (postByName.Themes.Contains(theme))
                    message = "Not associated, the theme " + nameTheme + " already associated";
                else
                {
                    postByName.Themes.Add(theme);
                    message = "The theme " + nameTheme + " was associated";
                }
            }
            else
            {
                message = "Not associated, the theme " + nameTheme + " does not exist";
            }
            _rabbitClient.SendMessage(message +"#"+"theme"+ "#" + nameTheme );
            Packet package = new Packet("RES", "2", message);
            await socketHandler.SendPackageAsync(package);
        }

        public async Task SearchPostAsync(SocketHandler socketHandler)
        {
            await _postHelper.SendListPostAsync(socketHandler, _postRepository);
            var packet = await socketHandler.ReceivePackageAsync();
            string namePost = packet.Data;
            if (namePost != "Back")
            {
                string message;
                if (_postHelper.AlreadyExistPost(namePost, _postRepository))
                {
                    Post post = _postRepository.Posts.Find(x => x.Name == namePost);
                    message = post.Name + "#" + post.CreationDate;
                    _rabbitClient.SendMessage("Search post: " +"#"+"post"+ "#" + namePost);
                }
                else

                {
                    message = "The post " + namePost + " does not exist";
                    _rabbitClient.SendMessage(message+"#"+"post"+ "#" + namePost);
                }
                 
                Packet package = new Packet("RES", "2", message);
                await socketHandler.SendPackageAsync(package);
            }
        }

        public async Task DisassociateThemeAsync(SocketHandler socketHandler)
        {
            await _postHelper.SendListPostAsync(socketHandler, _postRepository);
            var packet = await socketHandler.ReceivePackageAsync();
            string namePost = packet.Data;
            Post postByName = _postRepository.Posts.Find(x => x.Name == namePost);
            await new PostHelper().SendListThemesPostAsync(socketHandler, postByName);
            string themes = "";
            foreach (var theme in _themeRepository.Themes.Get())
            {
                themes += theme.Name + "#";
            }

            Packet package2 = new Packet("RES", "2", themes);
            await socketHandler.SendPackageAsync(package2);
            var packet2 = await socketHandler.ReceivePackageAsync();
            String[] messageArray = packet2.Data.Split('#');
            string postName = messageArray[0];
            if (postName != "Back")
            {
                string nameThemeDisassociate = messageArray[1];
                string nameNewTheme = messageArray[2];
                string message = "";
           
                if (_postHelper.AlreadyExistPost(postName, _postRepository))
                {
                    message =  _postHelper.Disassociate(postName, nameThemeDisassociate, nameNewTheme,
                        _themeRepository, _postRepository);
                }
                else
                {
                    message = "Not disassociated, the theme " + postName + " does not exist";
                }
                _rabbitClient.SendMessage(message+"#"+"post"+ "#" + postName);
                Packet package = new Packet("RES", "2", message);
                await socketHandler.SendPackageAsync(package);
            }
        }


    }
}