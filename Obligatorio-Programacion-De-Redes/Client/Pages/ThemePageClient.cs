using System;
using System.Threading.Tasks;
using Protocol;

namespace Client
{
    public class ThemePageClient
    {
        public async Task MenuAsync(SocketHandler socketHandler)
        {
            string[] _options = {"Add theme", "Modify theme", "Delete theme","Back"};
            int option = await new MenuClient().ShowMenuAsync(_options,"Theme menu");
            switch (option)
            {
                case 1:
                    Packet packg1 = new Packet("REQ", "5", "Add post");
                    await socketHandler.SendPackageAsync(packg1);
                    await AddThemeAsync(socketHandler);
                    break;
                case 2:
                    Packet packg6 = new Packet("REQ", "6", "Modify theme");
                    await socketHandler.SendPackageAsync(packg6);
                    await ModifyThemeAsync(socketHandler);
                    break;
                case 3:
                    Packet packg7 = new Packet("REQ", "7", "Delete theme");
                    await socketHandler.SendPackageAsync(packg7);
                    await DeleteThemeAsync(socketHandler);
                    break;
                case 4:
                    await new HomePageClient().MenuAsync(socketHandler,true);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;

            }
        }

        private async Task DeleteThemeAsync(SocketHandler socketHandler)
        {
            string optionSelect = await ReceiveListThemesAsync(socketHandler,"Themes");
            if (optionSelect == "Back")
            {
                Packet packg = new Packet("REQ", "4", optionSelect);
                await socketHandler.SendPackageAsync(packg);
                await MenuAsync(socketHandler);
            }
            else
            {
                Packet packg = new Packet("REQ", "4", optionSelect);
                await socketHandler.SendPackageAsync(packg);
                var packet = await socketHandler.ReceivePackageAsync();
                Console.WriteLine(packet.Data);
                await MenuAsync(socketHandler);
            }
        }
        
        private async Task<string> ReceiveListThemesAsync(SocketHandler socketHandler,string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message+"\n");
            Console.ForegroundColor = ConsoleColor.White;
            var packet = await socketHandler.ReceivePackageAsync();
            String[] postsNAmes= packet.Data.Split('#');
            int index = await new MenuClient().ShowMenuAsync(postsNAmes,"Themes");
            string optionSelect = postsNAmes[index-1];
            return optionSelect;
        }

        private async Task ModifyThemeAsync(SocketHandler socketHandler)
        {
            string optionSelected = await ReceiveListThemesAsync(socketHandler,"Themes");
            if (optionSelected == "Back")
            {
                Packet packg = new Packet("REQ", "4", optionSelected);
                await socketHandler.SendPackageAsync(packg);
                await MenuAsync(   socketHandler);
            }
            else
            {
                Packet packg2 = new Packet("REQ", "4", optionSelected);
                await socketHandler.SendPackageAsync(packg2);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("-----New data theme-----\n");
                Console.Write("Name: ");
                Console.ForegroundColor = ConsoleColor.White;
                string name = Console.ReadLine();
                while (name == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("The name cannot be empty: \n");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Name:  ");
                    Console.ForegroundColor = ConsoleColor.White;
                    name = Console.ReadLine();
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Description: ");
                Console.ForegroundColor = ConsoleColor.White;
                string description = Console.ReadLine();
                string message = name + "#" + description;
                Packet packg = new Packet("REQ", "4", message);
                await socketHandler.SendPackageAsync(packg);
                var packet = await socketHandler.ReceivePackageAsync();
                Console.WriteLine(packet.Data);
                await MenuAsync(   socketHandler);
            }
        }

        public async Task AddThemeAsync(SocketHandler socketHandler)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("-----New theme----- \n");
            Console.Write("Name: ");
            Console.ForegroundColor = ConsoleColor.White;
            string name = Console.ReadLine();
            while (name == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("The name cannot be empty: \n");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Name:  \n");
                Console.ForegroundColor = ConsoleColor.White;
                name = Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Description: ");
            Console.ForegroundColor = ConsoleColor.White;
            string description = Console.ReadLine();
            string message = name + "#" + description;
            Packet packg = new Packet("REQ", "4", message);
            await socketHandler.SendPackageAsync(packg);
            var packet = await socketHandler.ReceivePackageAsync();
            Console.WriteLine(packet.Data);
            await MenuAsync(   socketHandler);
        }

        
    }
}