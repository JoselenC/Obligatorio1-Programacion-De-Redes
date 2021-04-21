using System.Collections.Generic;
using Domain;

namespace BusinessLogic
{
    public class MemoryRepository
    {
        public List<Post> Posts { get; set; }
        public List<Theme> Themes { get; set; }
        public List<File> Files { get; set; }
        public List<ClientConnection> ClientsConnections { get; set; }

        public MemoryRepository()
        {
            Posts = new List<Post>();
            Themes = new List<Theme>();
            Files = new List<File>();
            ClientsConnections = new List<ClientConnection>();
        }
    }
}