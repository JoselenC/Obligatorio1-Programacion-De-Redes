using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using Protocol;

namespace Domain.Services
{
    public class PostService
    {
        private MemoryRepository repository;
        private SemaphoreSlim semaphoreSlim;
        public PostService(MemoryRepository repository)
        {
            this.repository = repository;
        }

        public PostService(MemoryRepository repository,SemaphoreSlim semaphore)
        {
            this.repository = repository;
            semaphoreSlim = semaphore;
        }
        private async Task SendListThemesPostAsync(SocketHandler socketHandler, string namePost)
        {
            Post postByName = repository.Posts.Find(x => x.Name == namePost);
            string themes = "";
            if (postByName!=null)
            {
                if (postByName.Themes != null)
                {
                    for (int i = 0; i < postByName.Themes.Count; i++)
                    {
                        themes += postByName.Themes[i].Name + "#";
                    }
                }
            }
            themes += "Back" + "#";
            Packet packg = new Packet("RES", "2", themes);
            await socketHandler.SendPackgAsync(packg);
        }
        
        private async Task SendListPostAsync(SocketHandler socketHandler)
        {
            string posts = "";
            if (repository.Posts != null && repository.Posts.Count!=0)
            {
                for (int i = 0; i < repository.Posts.Count; i++)
                {
                    SemaphoreSlimPost semaphoreSlimPost = repository.SemaphoreSlimPosts
                        .Find(x => x.Post.Name == repository.Posts[i].Name);
                    
                    if (semaphoreSlimPost==null || semaphoreSlimPost.SemaphoreSlim.CurrentCount > 0)
                    {
                        posts += repository.Posts[i].Name + "#";
                    }
                }
            }
            posts += "Back" + "#";
            Packet packg = new Packet("RES", "2", posts);
            await socketHandler.SendPackgAsync(packg);
        }
        
        private async Task SendListThemesAsync(SocketHandler socketHandler)
        {
            string posts = "";
            if (repository.Themes != null)
            {
                for (int i = 0; i < repository.Themes.Count; i++)
                {
                    posts += repository.Themes[i].Name + "#";
                }
            }
            posts += "Back" + "#";
            Packet packg = new Packet("RES", "2", posts);
            await socketHandler.SendPackgAsync(packg);
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
            Post post = repository.Posts.Find(x => x.SameName(name));
            if (post == null)
                return false;
            return true;
        }
        
        public async Task AddPostAsync(SocketHandler socketHandler)
        {
            Packet packg = new Packet("RES", "2", repository.Themes.Count.ToString());
            await socketHandler.SendPackgAsync(packg);
            string message = "";
            if (repository.Themes.Count > 0)
            {
                var packet = await socketHandler.ReceivePackgAsync();
                String[] messageArray = packet.Data.Split('#');                
                string name = messageArray[0];
                string creationDate = messageArray[1];
                if (name != "")
                {
                    if (!AlreadyExistPost(name))
                    {
                        Post post = new Post() { Name = name, CreationDate = creationDate };
                        repository.Posts.Add(post);
                        message = "The post " + name + " was created";
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
                Packet packg2 = new Packet("RES", "2", message);
                await socketHandler.SendPackgAsync(packg2);
            }
        }
        
        public async Task DeletePostAsync(SocketHandler socketHandler)
        {
            await SendListPostAsync(socketHandler);
            var packet = await socketHandler.ReceivePackgAsync();
            string name = packet.Data;
            if (name != "Back")
            {
                if (!AlreadyExistSemaphore(name))
                    repository.SemaphoreSlimPosts.Add(new SemaphoreSlimPost()
                    {
                        SemaphoreSlim = new SemaphoreSlim(1),
                        Post = repository.Posts.Find(x => x.Name == name)
                    });
                repository.SemaphoreSlimPosts
                    .Find(x => x.Post.Name == name).SemaphoreSlim.WaitAsync();
                string message;
                message = DeletePost(name);
                repository.SemaphoreSlimPosts
                    .Find(x => x.Post.Name == name).SemaphoreSlim.Release();
                Packet packg = new Packet("RES", "2", message);
                await socketHandler.SendPackgAsync(packg);
            }
        }

        private string DeletePost(string name)
        {
            string message;
            if (AlreadyExistPost(name))
            {
                Post postByName = repository.Posts.Find(x => x.Name == name);
                repository.Posts.Find(x => x.Name == name);
                repository.Posts.Remove(postByName);
                message = "The post " + name + " was deleted";
            }
            else
            {
                message = "Not deleted, the post" + name + " does not exist";
            }

            return message;
        }


        public async Task ModifyPostAsync(SocketHandler socketHandler)
        {
            await SendListPostAsync(socketHandler);
            var packet = await socketHandler.ReceivePackgAsync();
            string[] messageArray = packet.Data.Split('#');
            string oldName = messageArray[0];
            if (oldName != "Back")
            {
                var message = await ModifyPost(socketHandler, oldName);

                Packet packg = new Packet("RES", "2", message);
                await socketHandler.SendPackgAsync(packg);
            }
        }

        private async Task<string> ModifyPost(SocketHandler socketHandler, string oldName)
        {
            if (!AlreadyExistSemaphore(oldName))
                repository.SemaphoreSlimPosts.Add(new SemaphoreSlimPost()
                {
                    SemaphoreSlim = new SemaphoreSlim(1),
                    Post = repository.Posts.Find(x => x.Name == oldName)
                });
            repository.SemaphoreSlimPosts
                .Find(x => x.Post.Name == oldName).SemaphoreSlim.WaitAsync();
            var packet2 = await socketHandler.ReceivePackgAsync();
            string[] messageArray2 = packet2.Data.Split('#');
            string message;
            string name = messageArray2[0];
            if (name != "")
            {
                message = AddNewPost(messageArray2, name, oldName);
            }
            else
            {
                message = "The theme name cannot be empty";
            }
            repository.SemaphoreSlimPosts
                .Find(x => x.Post.Name == oldName).SemaphoreSlim.Release();
            return message;
        }

        private string AddNewPost(string[] messageArray2, string name, string oldName)
        {
            string message;
            string newCreationDate = messageArray2[1];
            Post postByName = repository.Posts.Find(x => x.Name == oldName);
            repository.Posts.Remove(postByName);
            if (!AlreadyExistPost(name))
            {
                Post newPost = new Post() {Name = name, CreationDate = newCreationDate};
                repository.Posts.Add(newPost);
                message = "The post " + oldName + " was modified";
            }
            else
            {
                message = "Not modified, the post" + name + " already exist";
            }

            return message;
        }

        private bool AlreadyExistSemaphore(string oldName)
        {
            return repository.SemaphoreSlimPosts
                .Find(x => x.Post.Name == oldName) != null;
        }

   
        public async Task AsociateThemeAsync(SocketHandler socketHandler)
        {
            await SendListPostAsync(socketHandler);
            await SendListThemesAsync(socketHandler);
            var packet = await socketHandler.ReceivePackgAsync();
            String[] messageArray = packet.Data.Split('#');
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
                        message = "Not associated, the theme " + nameTheme + " already associated";
                    else
                    {
                        repository.Themes.Remove(theme);
                        if (theme.Posts == null) 
                            theme.Posts = new List<Post>();
                        if(!theme.Posts.Contains(postByName))
                            theme.Posts.Add(postByName);
                        repository.Themes.Add(theme);
                        postByName.Themes.Add(theme);
                        message = "The theme " + nameTheme + " was associated";
                    }
                }
                else
                {
                    message = "Not associated, the theme " + nameTheme + " does not exist";
                }
                Packet packg = new Packet("RES", "2", message);
                await socketHandler.SendPackgAsync(packg);
            }
        }

        private async Task SendListThemesToPostAsync(SocketHandler socketHandler)
        {
            string posts = "";
            if (repository.Themes != null)
            {
                for (int i = 0; i < repository.Themes.Count; i++)
                {
                    posts += repository.Themes[i].Name + "#";
                }
                Packet packg = new Packet("RES", "2", posts);
                await socketHandler.SendPackgAsync(packg);
            }         
        }

        public async Task AsociateThemeToPostAsync(SocketHandler socketHandler)
        {
            await SendListThemesToPostAsync(socketHandler);
            var packet = await socketHandler.ReceivePackgAsync();
            String[] messageArray = packet.Data.Split('#');
            string namePost = messageArray[0];
            string message = "";
            string nameTheme = messageArray[1];
            if (AlreadyExistTheme(nameTheme))
            {
                Post postByName = repository.Posts.Find(x => x.Name == namePost);
                Theme theme = repository.Themes.Find(x => x.Name == nameTheme);
                repository.Themes.Remove(theme);
                if (theme.Posts == null) 
                    theme.Posts = new List<Post>();
                if(!theme.Posts.Contains(postByName))
                    theme.Posts.Add(postByName);
                repository.Themes.Add(theme);
                if (postByName.Themes == null)
                    postByName.Themes = new List<Theme>();
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
            Packet packg = new Packet("RES", "2", message);
            await socketHandler.SendPackgAsync(packg);
        }

       public async Task SearchPostAsync(SocketHandler socketHandler)
        {
            await SendListPostAsync(socketHandler);
            var packet = await socketHandler.ReceivePackgAsync();
            string namePost = packet.Data;
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
                Packet packg = new Packet("RES", "2", message);
                await socketHandler.SendPackgAsync(packg);
            }
        }

        public async Task DisassociateThemeAsync(SocketHandler socketHandler)
        {
            await SendListPostAsync(socketHandler);
            var packet = await socketHandler.ReceivePackgAsync();
            string namePost = packet.Data;
            await SendListThemesPostAsync(socketHandler,namePost);
            string themes = "";
            foreach (var theme in repository.Themes)
            {
                themes += theme.Name + "#";
            }
            Packet packg2 = new Packet("RES", "2", themes);
            await socketHandler.SendPackgAsync(packg2);
            var packet2 = await socketHandler.ReceivePackgAsync();
            String[] messageArray = packet2.Data.Split('#');
            string postName = messageArray[0];
            if (postName != "Back")
            {
                string nameThemeDisassociate = messageArray[1];
                string nameNewTheme = messageArray[2];
                string message = "";
                if (AlreadyExistPost(postName))
                {
                    message = await DisassociateAsync(postName, nameThemeDisassociate, nameNewTheme);
                }
                else
                {
                    message = "Not disassociated, the theme " + postName + " does not exist";
                }
                Packet packg = new Packet("RES", "2", message);
                await socketHandler.SendPackgAsync(packg);
            }
        }
        
        private async Task<string> DisassociateAsync(string postName, string nameThemeDisassociate, string nameNewTheme)
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