using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using DataHandler;
using Protocol;
using SocketHandler = DataHandler.SocketHandler;

namespace Client
{
    public class HomePageClient
    {
        public async Task MenuAsync(Socket SocketClient, SocketHandler socketHandler)
        {
            var exit = false;
            string[] _options = { "Posts", "Themes", "Associate file", "Search post", "Exit" };

            int option = await new MenuClient().ShowMenuAsync(_options, "MenuAsync");
            switch (option)
            {
                case 1:
                    await new PostPageClient().MenuAsync(SocketClient, socketHandler, exit);
                    break;
                case 2:
                    await new ThemePageClient().MenuAsync(SocketClient, socketHandler);
                    break;
                case 3:
                    await new FilePageClient().AssociateFileAsync(socketHandler, SocketClient);
                    break;
                case 4:
                    Console.Clear();
                    Packet packg1 = new Packet("REQ", "9", "Search post");
                    await socketHandler.SendPackgAsync(packg1);
                    await new PostPageClient().SearchPost(socketHandler, SocketClient);
                    break;
                case 5:
                    Console.Clear();
                    Packet packg2 = new Packet("REQ", "6", "Add post");
                    await socketHandler.SendPackgAsync(packg2);
                    exit = true;
                    SocketClient.Shutdown(SocketShutdown.Both);
                    SocketClient.Close();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
}