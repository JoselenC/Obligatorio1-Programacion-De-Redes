using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading.Tasks;
using DataHandler;
using Protocol;
using ProtocolFiles;

namespace Client
{
    public class FilePageClient
    {

        private async Task<string> ReceiveListPostAsync(SocketHandler socketHandler, string message)
        {
            var packet = await socketHandler.ReceivePackgAsync();
            String[] postsNAmes = packet.Data.Split('#');
            int index = await new MenuClient().ShowMenuAsync(postsNAmes,message);
            string optionSelect = postsNAmes[index - 1];
            return optionSelect;
        }

        public async Task AssociateFileAsync(SocketHandler socketHandler, Socket SocketClient)
        {
            ProtocolHandler protocolHandler = new ProtocolHandler();
            Packet packg1 = new Packet("REQ", "8", "Associate file");
            await socketHandler.SendPackgAsync(packg1);
            string title = "Select post to associate file";
            string optionSelect1 = await ReceiveListPostAsync(socketHandler, title);
            if (optionSelect1 != "Back")
            {
                bool correctPath = false;
                Console.WriteLine("Path: ");
                string path = Console.ReadLine();
                while (!correctPath)
                {
                    while (path == "")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The path cannot be empty");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Path: ");
                        path = Console.ReadLine();
                    }
                    try
                    {
                        await protocolHandler.SendFileAsync(path, SocketClient, socketHandler, optionSelect1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("File was associated");
                        await new HomePageClient().MenuAsync(SocketClient, socketHandler);
                        correctPath = true;
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter a correct path");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Path: ");
                        path = Console.ReadLine();
                    }
                }
            }
            await new HomePageClient().MenuAsync(SocketClient, socketHandler);
        }
    
    }
}