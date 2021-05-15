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
        public async Task MenuAsync(SocketHandler socketHandler,bool exit)
        {
            string[] _options = {"Posts", "Themes", "Associate file", "Search post", "Exit"};

            while (!exit)
            {
                int option = await new MenuClient().ShowMenuAsync(_options, "Menu");
                switch (option)
                {
                    case 1:
                        await new PostPageClient().MenuAsync(socketHandler);
                        break;
                    case 2:
                        await new ThemePageClient().MenuAsync(socketHandler);
                        break;
                    case 3:
                        await new FilePageClient().AssociateFileAsync(socketHandler);
                        break;
                    case 4:
                        Console.Clear();
                        Packet packg1 = new Packet("REQ", "9", "Search post");
                        await socketHandler.SendPackgAsync(packg1);
                        await new PostPageClient().SearchPostAsync(socketHandler);
                        break;
                    case 5:
                        Console.Clear();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}