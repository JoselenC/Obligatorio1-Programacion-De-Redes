using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using LogServer;
using Protocol;

namespace Domain.Services
{
    public class PostService : IPostService
    {
        private ManagerRepository repository;
        private Log log;
        public PostService(ManagerRepository vRepository,Log log)
        {
            repository = vRepository;
            this.log = log;
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
            if (repository.Posts != null && repository.Posts.Get().Count!=0)
            {
                for (int i = 0; i < repository.Posts.Get().Count; i++)
                {
                 //   SemaphoreSlimPost semaphoreSlimPost = repository.SemaphoreSlimPosts
                   //     .Find(x => x.Post.Name == repository.Posts.Get()[i].Name);
                    
                //    if (semaphoreSlimPost==null || semaphoreSlimPost.SemaphoreSlim.CurrentCount > 0)
                  //  {
                        posts += repository.Posts.Get()[i].Name + "#";
                   // }
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
                for (int i = 0; i < repository.Themes.Get().Count; i++)
                {
                    posts += repository.Themes.Get()[i].Name + "#";
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
            try
            {
                Post post = repository.Posts.Find(x => x.SameName(name));
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }
        
        public async Task AddPostAsync(SocketHandler socketHandler)
        {
            
            Packet packg = new Packet("RES", "2", repository.Themes.Get().Count.ToString());
            await socketHandler.SendPackgAsync(packg);
            string message = "";
            if (repository.Themes.Get().Count > 0)
            {
                var packet = await socketHandler.ReceivePackgAsync();
                log.SaveLog("New post" + packet.Data);
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
            log.SaveLog("Delete post" + packet.Data);
            if (name != "Back")
            {
         //       if (!AlreadyExistSemaphore(name))
           //         repository.SemaphoreSlimPosts.Add(new SemaphoreSlimPost()
             //       {
               //         SemaphoreSlim = new SemaphoreSlim(1),
                 //       Post = repository.Posts.Find(x => x.Name == name)
                  //  });
                //repository.SemaphoreSlimPosts
                  //  .Find(x => x.Post.Name == name).SemaphoreSlim.WaitAsync();
                string message;
                message = DeletePost(name);
           //     repository.SemaphoreSlimPosts
             //       .Find(x => x.Post.Name == name).SemaphoreSlim.Release();
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
                repository.Posts.Delete(postByName);
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
       //     if (!AlreadyExistSemaphore(oldName))
         //       repository.SemaphoreSlimPosts.Add(new SemaphoreSlimPost()
           //     {
             //       SemaphoreSlim = new SemaphoreSlim(1),
               //     Post = repository.Posts.Find(x => x.Name == oldName)
               // });
            //repository.SemaphoreSlimPosts
              //  .Find(x => x.Post.Name == oldName).SemaphoreSlim.WaitAsync();
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
        //    repository.SemaphoreSlimPosts
          //      .Find(x => x.Post.Name == oldName).SemaphoreSlim.Release();
            return message;
        }

        private string AddNewPost(string[] messageArray2, string name, string oldName)
        {
            log.SaveLog("Modify post, old Name: " + oldName + "new name: "+name);
            string message;
            string newCreationDate = messageArray2[1];
            Post postByName = repository.Posts.Find(x => x.Name == oldName);
            repository.Posts.Delete(postByName);
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
                log.SaveLog("Associate theme: " + nameTheme +"to post: "+ namePost);
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
                        repository.Themes.Delete(theme);
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
                for (int i = 0; i < repository.Themes.Get().Count; i++)
                {
                    posts += repository.Themes.Get()[i].Name + "#";
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
            log.SaveLog("Associate theme: " + nameTheme +"to post: "+ namePost);
            if (AlreadyExistTheme(nameTheme))
            {
                Post postByName = repository.Posts.Find(x => x.Name == namePost);
                Theme oldtheme = repository.Themes.Find(x => x.Name == nameTheme);
                Theme theme = repository.Themes.Find(x => x.Name == nameTheme);
                if (theme.Posts == null) 
                    theme.Posts = new List<Post>();
                if(!theme.Posts.Contains(postByName))
                    theme.Posts.Add(postByName);
                repository.Themes.Update(oldtheme,theme);
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
                    log.SaveLog("Search post: "+ namePost);
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
            foreach (var theme in repository.Themes.Get())
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
                log.SaveLog("Associate theme: " + nameNewTheme + "Dissasociate theme" + nameThemeDisassociate +
                            "to post: " + namePost);
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