using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using BusinessLogic;
using ClientHandler;
using DataHandler;
using Domain;

namespace Server
{
    class Program
    {
        public static bool _exit = false;
        public static readonly List<Socket> ConnectedClients = new List<Socket>();

        static void Main(string[] args)
        {
            Socket socketServer = new ConnectionConfig().Connect();
            SocketHandler socketHandler = new SocketHandler(socketServer);
            MemoryRepository repository = new MemoryRepository();
            var thread = new Thread(a => new HomePageServer().Menu(repository,socketServer,socketHandler));
            thread.Start();
          
            while (!_exit)
            {
                var threadServer = new Thread(() => new HandleClient().ListenForConnections(socketServer, repository,_exit,ConnectedClients));
                threadServer.Start();
            }

            foreach (var socketClient in ConnectedClients)
            {
                try
                {
                    
                    socketClient.Shutdown(SocketShutdown.Both);
                    socketClient.Close(1);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Socket client is already closed");
                }

                Console.WriteLine("Saliendo del Main Thread...");
            }
        }
        
    }
}