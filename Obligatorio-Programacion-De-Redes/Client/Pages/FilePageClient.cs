using System;
using System.ComponentModel;
using System.Net.Sockets;
using DataHandler;
using Protocol;
using ProtocolFiles;

namespace Client
{
    public class FilePageClient
    {

        private string ReceiveListPost(SocketHandler socketHandler, string message)
        {
            var packet = socketHandler.ReceivePackg();
            String[] postsNAmes = packet.Data.Split('#');
            int index = new MenuClient().ShowMenu(postsNAmes,message);
            string optionSelect = postsNAmes[index - 1];
            return optionSelect;
        }

        public void AssociateFile(SocketHandler socketHandler, Socket SocketClient)
        {
            ProtocolHandler protocolHandler = new ProtocolHandler();
            Packet packg1 = new Packet("REQ", "8", "Associate file");
            socketHandler.SendPackg(packg1);
            string title = "Select post to associate file";
            string optionSelect1 = ReceiveListPost(socketHandler, title);
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
                        protocolHandler.SendFileAsync(path, SocketClient, socketHandler, optionSelect1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("File was associated");
                        new HomePageClient().Menu(SocketClient, socketHandler);
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
            new HomePageClient().Menu(SocketClient, socketHandler);
        }
    
    }
}