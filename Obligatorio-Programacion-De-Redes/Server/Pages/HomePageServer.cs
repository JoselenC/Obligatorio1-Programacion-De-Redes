using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using Protocol;
using Server;

namespace ClientHandler
{
    public class HomePageServer
    {
        
        public async Task MenuAsync(MemoryRepository repository,Socket SocketClient, SocketHandler socketHandler)
        {
            string[] _options = {"Client list", "Posts", "Themes", "File", "Exit"};
            int option = new MenuServer().ShowMenu(_options,"MenuAsync");

                switch (option)
                {
                    case 1:
                        new ClientPageServer().ShowClientList(repository,socketHandler, SocketClient);
                        break;
                    case 2:
                        new PostPageServer().Menu(repository,SocketClient, socketHandler);
                        break;
                    case 3:
                        new ThemePageServer().Menu(repository,SocketClient, socketHandler);
                        break;
                    case 4:
                        new FilePageServer().ShowFileList(repository,socketHandler, SocketClient);
                        break;
                    case 5:
                        SocketClient.Shutdown(SocketShutdown.Both);
                        SocketClient.Close();
                        break;
                    default:
                        break;
                
            }
        }
    }
}