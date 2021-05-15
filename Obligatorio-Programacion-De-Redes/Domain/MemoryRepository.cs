using System.Collections.Generic;
using Domain;

namespace BusinessLogic
{
    public class MemoryRepository
    {
        public List<Post> Posts { get; set; }
        public List<Theme> Themes { get; set; }
        public List<File> Files { get; set; }
        public List<ClientConnected> ClientsConnections { get; set; }

        public List<SemaphoreSlimPost> SemaphoreSlimPosts { get; set; }
        
        public List<SemaphoreSlimTheme> SemaphoreSlimThemes { get; set; }
        public MemoryRepository()
        {
            Posts = new List<Post>();
            Themes = new List<Theme>();
            Files = new List<File>();
            ClientsConnections = new List<ClientConnected>();
            SemaphoreSlimPosts = new List<SemaphoreSlimPost>();
            SemaphoreSlimThemes = new List<SemaphoreSlimTheme>();
        }
    }
}