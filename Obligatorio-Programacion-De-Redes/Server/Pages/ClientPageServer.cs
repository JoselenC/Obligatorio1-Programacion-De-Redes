using System;
using System.Net.Sockets;
using Domain;
using DataHandler;

namespace Server
{
    public class ClientPageServer
    {
        
        public void ShowClientList(SocketHandler socketHandler,Socket socketClient)
        {
            string[] data = socketHandler.ReceiveMessage();
            if (data.Length == 1)
            {
                Console.WriteLine(data[0]);
            }
            else
            {
                for (int i = 0; i < data.Length - 3; i = i + 3)
                {
                    Console.WriteLine("Client " + i+1 + "Hour of connection: " + data[i] + "Port: " + data[i+1] + "Ip: " + data[i+2] + "\n");
                }
            }
        }

    }
}