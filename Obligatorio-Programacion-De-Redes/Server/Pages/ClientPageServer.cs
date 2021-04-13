using System;
using System.Net.Sockets;
using Domain;
using DataHandler;

namespace Server
{
    public class ClientPageServer
    {
        
        public void ShowClientList(SocketHandler socketHandler)
        {
            Console.WriteLine("Name of the post to delete"); 
            string name = Console.ReadLine();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(name);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
            Console.WriteLine("Message sent to the server");
        }

    }
}