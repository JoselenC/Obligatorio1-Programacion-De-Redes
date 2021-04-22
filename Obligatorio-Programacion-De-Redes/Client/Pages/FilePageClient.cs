using System;
using System.ComponentModel;
using System.Net.Sockets;
using DataHandler;
using ProtocolFiles;

namespace Client
{
    public class FilePageClient
    {
        
        private static string ReceiveListPost(SocketHandler socketHandler,string message)
        {
            string[] postsNAmes = socketHandler.ReceiveMessage();
            int index = new MenuClient().ShowMenu(postsNAmes,message);
            string optionSelect = postsNAmes[index-1];
            return optionSelect;
        }
        
        public void AssociateFile(SocketHandler socketHandler, Socket SocketClient)
        {
            ProtocolHandler protocolHandler = new ProtocolHandler();
            socketHandler.SendData(8,SocketClient);
            string title="Select post to associate file";
            string optionSelect1 = ReceiveListPost(socketHandler,title);
            if (optionSelect1 == "Back")
            {
                socketHandler.SendMessage(optionSelect1);
                new HomePageClient().Menu(SocketClient, socketHandler);
            }
            else
            {
                Console.WriteLine("Path: ");
                string path= Console.ReadLine();
                try
                {
                    protocolHandler.SendFile(path, SocketClient, socketHandler, optionSelect1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("File was added");
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error al agregrar el archivo");
                }
            }
        }
    }
}