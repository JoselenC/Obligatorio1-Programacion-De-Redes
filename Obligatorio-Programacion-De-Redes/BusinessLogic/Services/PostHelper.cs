using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Managers;
using DomainObjects;
using Protocol;

namespace BusinessLogic.Services
{
    public class PostHelper
    {
        public async Task SendListThemesPostAsync(SocketHandler socketHandler, Post postByName)
        {
            string themes = "";
            if (postByName?.Themes != null)
            {
                foreach (var t in postByName.Themes)
                {
                    themes += t.Name + "#";
                }
            }
            themes += "Back" + "#";
            Packet package = new Packet("RES", "2", themes);
            await socketHandler.SendPackageAsync(package);
        }
        
                
        public async Task SendListPostAsync(SocketHandler socketHandler, ManagerPostRepository postRepository)
        {
            string posts = "";
            List<Post> listPosts = postRepository.Posts.Get();
            foreach (var t in listPosts)
            {
                posts +=t.Name + "#";
            }
            posts += "Back" + "#";
            Packet package = new Packet("RES", "2", posts);
            await socketHandler.SendPackageAsync(package);
        }
        
        public async Task SendListThemesAsync(SocketHandler socketHandler,ManagerThemeRepository themeRepository)
        {
            string posts = "";
            if (themeRepository.Themes != null)
            {
                for (int i = 0; i < themeRepository.Themes.Get().Count; i++)
                {
                    posts += themeRepository.Themes.Get()[i].Name + "#";
                }
            }
            posts += "Back" + "#";
            Packet package = new Packet("RES", "2", posts);
            await socketHandler.SendPackageAsync(package);
        }
        
        public bool AlreadyExistTheme(string name, ManagerThemeRepository themeRepository)
        {
            Theme theme = themeRepository.Themes.Find(x => x.Name == name);
            return theme != null;
        }
        
        public bool AlreadyExistPost(string name, ManagerPostRepository postRepository)
        {
            try
            {
                postRepository.Posts.Find(x => x.SameName(name));
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }
        
        public string DeletePost(string name,ManagerPostRepository postRepository)
        {
            string message;
            if (new PostHelper().AlreadyExistPost(name,postRepository))
            {
                lock (postRepository.Posts)
                {
                    Post postByName = postRepository.Posts.Find(x => x.Name == name);
                    postRepository.Posts.Find(x => x.Name == name);
                    postRepository.Posts.Delete(postByName);
                    message = "The post " + name + " was deleted";
                }
            }
            else
            {
                message = "Not deleted, the post" + name + " does not exist";
            }

            return message;
        }

        public async Task<string> ModifyPost(SocketHandler socketHandler, string oldName,
            ManagerPostRepository postRepository, RabbitHelper rabbitClient)
        {
            var packet2 = await socketHandler.ReceivePackageAsync();
            lock (postRepository.Posts)
            {
                string[] messageArray2 = packet2.Data.Split('#');
                string name = messageArray2[0];
                var message = name != "" ? AddNewPost(messageArray2, name, oldName, postRepository, rabbitClient) 
                    : "The theme name cannot be empty";
                return message;
            }
        }

        private string AddNewPost(string[] messageArray2, string name, string oldName, ManagerPostRepository postRepository, RabbitHelper rabbitClient)
        {
            string message;
            string newCreationDate = messageArray2[1];
            Post postByName = postRepository.Posts.Find(x => x.Name == oldName);
            if (!new PostHelper().AlreadyExistPost(name, postRepository))
            {
                Post newPost = new Post() {Name = name, CreationDate = newCreationDate};
                if (postByName.File != null) newPost.File = postByName.File;
                if (postByName.Themes != null) newPost.Themes = postByName.Themes;
                postRepository.Posts.Update(postByName,newPost);
                message = "Modify post: " + oldName + " new name: "+name + " new creation date: "+ newCreationDate;
            }
            else
            {
                message = "Not modified, the post" + name + " already exist";
            }
            rabbitClient.SendMessage(message);
            return message;
        }

        public async Task SendListThemesToPostAsync(SocketHandler socketHandler, ManagerThemeRepository themeRepository)
        {
            string posts = "";
            if (themeRepository.Themes != null)
            {
                for (int i = 0; i < themeRepository.Themes.Get().Count; i++)
                {
                    posts += themeRepository.Themes.Get()[i].Name + "#";
                }
                Packet package = new Packet("RES", "2", posts);
                await socketHandler.SendPackageAsync(package);
            }         
        }
        
        public string Disassociate(string postName, string nameThemeDisassociate, string nameNewTheme, 
            ManagerThemeRepository themeRepository, ManagerPostRepository postRepository)
        {
            string message;
            Post oldPost = postRepository.Posts.Find(x => x.Name == postName);
            Post newPost = postRepository.Posts.Find(x => x.Name == postName);
            if (AlreadyExistTheme(nameThemeDisassociate,themeRepository) && AlreadyExistTheme(nameNewTheme,themeRepository))
            {
                Theme theme = themeRepository.Themes.Find(x => x.Name == nameThemeDisassociate);
                Theme newTheme = themeRepository.Themes.Find(x => x.Name == nameNewTheme);
                if (newPost.Themes != null && newPost.Themes.Contains(theme))
                {
                    newPost.Themes.Remove(theme);
                    newPost.Themes.Add(newTheme);
                }
                message = "The theme " + nameThemeDisassociate + " was disassociate and " + nameNewTheme +
                          " was associate";
                postRepository.Posts.Update(oldPost, newPost);
            }
            else if (!AlreadyExistTheme(nameThemeDisassociate,themeRepository))
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