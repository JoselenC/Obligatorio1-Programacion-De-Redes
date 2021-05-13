using System;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain;
using DataHandler;
using Protocol;

namespace Client
{
    public class PostPageClient
    {

        public async Task MenuAsync(SocketHandler socketHandler)
        {
            string[] _options = {"Add post", "Modify post", "Delete post", "Associate theme", "Disassociate theme","Back"};
            int option = await new MenuClient().ShowMenuAsync(_options, "Post menu");
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Packet packg1 = new Packet("REQ", "1", "Add post");
                        await socketHandler.SendPackgAsync(packg1);
                        await AddPostAsync(socketHandler);
                        break;
                    case 2:
                        Console.Clear();
                        Packet packg2 = new Packet("REQ", "2", "Modify post");
                        await socketHandler.SendPackgAsync(packg2);
                        await ModifyPostAsync(socketHandler);
                        break;
                    case 3:
                        Console.Clear();
                        Packet packg3 = new Packet("REQ", "3", "Delete post");
                        await socketHandler.SendPackgAsync(packg3);
                        await DeletePostAsync(socketHandler);
                        break;
                    case 4:
                        Console.Clear();
                        Packet packg4 = new Packet("REQ", "4", "Associate theme");
                        await socketHandler.SendPackgAsync(packg4);
                        await AsociateThemeAsync(socketHandler);
                        break;
                    case 5:
                        Console.Clear();
                        Packet packg11 = new Packet("REQ", "11", "Disassociate theme");
                        await socketHandler.SendPackgAsync(packg11);
                        await DisassociateThemeAsync(socketHandler);
                        break;
                    case 6:
                        Console.Clear();
                        await new HomePageClient().MenuAsync(socketHandler);
                        break;
                    default:
                        break;
                }
        }

       private async Task DeletePostAsync(SocketHandler socketHandler)
       {
            string message = "Posts to delete";
            string optionSelect = await ReceiveListPostAsync(socketHandler,message);
            if (optionSelect == "Back")
            {
                Packet packg = new Packet("REQ", "3", optionSelect);
                await socketHandler.SendPackgAsync(packg);
                await MenuAsync(socketHandler);
            }
            else
            {
                Packet packg = new Packet("REQ", "3", optionSelect);
                await socketHandler.SendPackgAsync(packg);
                var packet = await socketHandler.ReceivePackgAsync();
                string messageReceive = packet.Data;
                Console.WriteLine(messageReceive);
                await MenuAsync(socketHandler);
            }
        }

        private async Task ModifyPostAsync(SocketHandler socketHandler)
        {
            string title ="Posts to modify";
            string optionSelect = await ReceiveListPostAsync(socketHandler,title);
            
            if (optionSelect == "Back")
            {
                Packet packg = new Packet("REQ", "2", optionSelect);
                await socketHandler.SendPackgAsync(packg);
                await MenuAsync(socketHandler);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("-----New data----- \n");
                Console.Write("Name: ");
                Console.ForegroundColor = ConsoleColor.White;
                string name = Console.ReadLine();
                while (name == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("The name cannot be empty: \n");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Name: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    name = Console.ReadLine();
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Creation date: ");
                Console.ForegroundColor = ConsoleColor.White;
                string creationDate = Console.ReadLine();
                while (!GoodFormat(creationDate))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("The date format must be: dd/mm/yyyy \n");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Creation date: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    creationDate = Console.ReadLine();
                }
                string message = optionSelect + "#" + name + "#" + creationDate;
                Packet packg = new Packet("REQ", "2", message);
                await socketHandler.SendPackgAsync(packg);
                var packet = await socketHandler.ReceivePackgAsync();
                string messageReceive = packet.Data;
                Console.WriteLine(messageReceive);
                await MenuAsync(socketHandler);
            }
        }

        private bool GoodFormat(string creationDate)
        {
            bool goodFormat = false;
            Regex regex = new Regex(@"\b\d{2}(/|-|.|\s)\d{2}(/|-|.|\s)(\d{4})");
            var match = regex.Match(creationDate);
            if (match.Success)
                goodFormat= true;
            if (goodFormat)
            {
                string[] date = creationDate.Split("/");
                int day = Int32.Parse(date[0]);
                int month = Int32.Parse(date[1]);
                int year = Int32.Parse(date[2]);
                if ((day>0 && day<32) && (month>0 && month<13) && (year < 2022)){
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        } 

        public async Task AddPostAsync(SocketHandler socketHandler)
        {
            var packetCantPost = await socketHandler.ReceivePackgAsync();
            string cantPost = packetCantPost.Data;
            if (Int32.Parse(cantPost) > 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("------New post------ \n");
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
                Console.Write("Creation date: ");
                Console.ForegroundColor = ConsoleColor.White;
                string creationDate = Console.ReadLine();
                while (!GoodFormat(creationDate))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("The date format must be: dd/mm/yyyy \n");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Creation date: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    creationDate = Console.ReadLine();
                }

                string message = name + "#" + creationDate;
                Packet packg = new Packet("REQ", "1", message);
                await socketHandler.SendPackgAsync(packg);
                var packet = await socketHandler.ReceivePackgAsync();
                string messageReceive = packet.Data;
                Console.WriteLine(messageReceive);
                if (messageReceive.Substring(0, 3) != "Not")
                    await AddThemeToPostAsync(socketHandler,messageReceive);
            }
            else
            {
                Console.WriteLine("Register at least one theme before adding posts");
            }
            await MenuAsync(socketHandler);
           
        }
        
        private async Task AddThemeToPostAsync(SocketHandler socketHandler, string postName)
        {
            Packet packg1 = new Packet("REQ", "12", "Add post");
            await socketHandler.SendPackgAsync(packg1);
            string title = "Themes to add to the post";
            string optionSelect = await ReceiveThemesAsync(socketHandler, title);
            string message = postName + "#" + optionSelect;
            Packet packg = new Packet("REQ", "2", message);
            await socketHandler.SendPackgAsync(packg);
            var packet = await socketHandler.ReceivePackgAsync();
            string messageReceive = packet.Data;
            Console.WriteLine(messageReceive);
            await MenuAsync(socketHandler);

        }
        
        private async Task DisassociateThemeAsync(SocketHandler socketHandler)
        {
            string title="Select post to disassociate theme";
            var optionSelect = await ReceiveListPostAsync(socketHandler,title);
            if (optionSelect == "Back")
            {
                Packet packg2 = new Packet("REQ", "11", optionSelect);
                await socketHandler.SendPackgAsync(packg2);
                await MenuAsync(socketHandler);
            }
            else
            {
                Packet packg2 = new Packet("REQ", "11", optionSelect);
                await socketHandler.SendPackgAsync(packg2);
                string title2="Select theme to disassociate to the post";
                var optionSelectThemes = await ReceiveThemesAsync(socketHandler,title2);
                if (optionSelectThemes == "Back")
                {
                    Packet packg3 = new Packet("REQ", "11", optionSelect);
                    await socketHandler.SendPackgAsync(packg3);
                    await MenuAsync(socketHandler);
                }
                else
                {
                    string title3 = "select new theme to associate to the post";
                    var optionSelectThemes2 = await ReceiveThemesAsync(socketHandler, title3);
                    string message = optionSelect + "#" + optionSelectThemes + "#" + optionSelectThemes2;
                    Packet packg4 = new Packet("REQ", "11", message);
                    await socketHandler.SendPackgAsync(packg4);
                    var packet = await socketHandler.ReceivePackgAsync();
                    string messageReceive = packet.Data;
                    Console.WriteLine(messageReceive);
                    await MenuAsync( socketHandler);
                }
                
            }
        }

        private async Task<string> ReceiveThemesAsync(SocketHandler socketHandler,string message)
        {
            var packet = await socketHandler.ReceivePackgAsync();
            String[] themesNames= packet.Data.Split('#');
            int indexThemes = await new MenuClient().ShowMenuAsync(themesNames,message);
            string optionSelectThemes = themesNames[indexThemes-1];
            return optionSelectThemes;
        }

        private async Task<string> ReceiveListPostAsync(SocketHandler socketHandler,string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("----"+message+"----\n");
            Console.ForegroundColor = ConsoleColor.White;
            var packet = await socketHandler.ReceivePackgAsync();
            String[] postsNAmes = packet.Data.Split('#');
            int index = await new MenuClient().ShowMenuAsync(postsNAmes,"Posts");
            string optionSelect = postsNAmes[index-1];
            return optionSelect;
        }

        public async Task AsociateThemeAsync(SocketHandler socketHandler)
        {
            string title="Select post to associate theme";
            string optionSelect1 = await ReceiveListPostAsync(socketHandler,title);
            if (optionSelect1 == "Back")
            {
                var packet = await socketHandler.ReceivePackgAsync();
                String[] themesNames = packet.Data.Split('#');
                Packet packg = new Packet("REQ", "4", optionSelect1);
                await socketHandler.SendPackgAsync(packg);
                await new HomePageClient().MenuAsync(socketHandler);
            }
            else
            {
                string title2="Select theme to associate the post";
                string optionSelect = await ReceiveThemesAsync(socketHandler,title2);
                if (optionSelect == "Back")
                {
                    Packet packg = new Packet("REQ", "4", optionSelect);
                    await socketHandler.SendPackgAsync(packg);
                    await new HomePageClient().MenuAsync(socketHandler);
                }
                else
                {
                    string message = optionSelect1 + "#" + optionSelect;
                    Packet packg = new Packet("REQ", "4", message);
                    await socketHandler.SendPackgAsync(packg);
                    var packet = await socketHandler.ReceivePackgAsync();
                    string messageReceive = packet.Data;
                    Console.WriteLine(messageReceive);
                    await MenuAsync(socketHandler);
                }
            }
        }

        public async Task SearchPost(SocketHandler socketHandler)
        {
            string title="Select post";
            var optionSelect = await ReceiveListPostAsync(socketHandler,title);
            if (optionSelect == "Back")
            {
                Packet packg = new Packet("REQ", "9", optionSelect);
                await socketHandler.SendPackgAsync(packg);
                await new HomePageClient().MenuAsync(socketHandler);
            }
            else
            {
                Packet packg = new Packet("REQ", "2", optionSelect);
                await socketHandler.SendPackgAsync(packg);
                var packet = await socketHandler.ReceivePackgAsync();
                String[] messageArray = packet.Data.Split('#');
                string name = messageArray[0];
                Console.WriteLine("Name:" + name);
                string creationDate = messageArray[1];
                Console.WriteLine("Creation date:" + creationDate);
                await MenuAsync( socketHandler);
            }
        }
    }
}