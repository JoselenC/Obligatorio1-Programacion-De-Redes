using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
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

        static async Task Main(string[] args)
        {
            Socket socketServer = new ConnectionConfig().Connect();
            SocketHandler socketHandler = new SocketHandler(socketServer);
            MemoryRepository repository = new MemoryRepository();
            await new HomePageServer().MenuAsync(repository,socketServer,socketHandler);
          
            while (!_exit)
            {
                await new HandleClient().ListenForConnectionsAsync(socketServer, repository,_exit,ConnectedClients);
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