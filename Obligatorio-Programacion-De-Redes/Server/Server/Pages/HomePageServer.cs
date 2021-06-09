using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using Protocol;
using Server;
using ServerGRPC.Server;

namespace Server
{
    public class HomePageServer
    {
        public async Task MenuAsync(ManagerRepository repository, bool exit)
        {
            string[] _options = {"Client list", "Posts", "Themes", "File", "Exit"};
            while (!exit)
            {
                int option = new MenuServer().ShowMenu(_options,"Menu");
                switch (option)
                {
                    case 1:
                        new ClientPageServer().ShowClientList(repository);
                        break;
                    case 2:
                        new PostPageServer().Menu(repository);
                        break;
                    case 3:
                        new ThemePageServer().Menu(repository);
                        break;
                    case 4:
                        new FilePageServer().ShowFileList(repository);
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}