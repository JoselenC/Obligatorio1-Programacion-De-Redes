using System;
using System.Collections.Generic;
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
            var socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 30000));
            socketServer.Listen(10);
            SocketHandler socketHandler = new SocketHandler(socketServer);
            MemoryRepository repository = new MemoryRepository();
            var thread = new Thread(a => new HomePageServer().Menu(repository,socketServer,socketHandler));
            thread.Start();
          
            while (!_exit)
            {
                var threadServer = new Thread(() => ListenForConnections(socketServer, repository));
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

        private static void ListenForConnections(Socket socketServer, MemoryRepository repository)
        {
            try
            {
                var socketConnected = socketServer.Accept();
                ConnectedClients.Add(socketConnected);
                ClientConnection clientConnection = new ClientConnection()
                {
                    TimeOfConnection = new DateTime().ToLocalTime().ToString(),
                    LocalEndPoint = socketConnected.LocalEndPoint.ToString(),
                    Ip = "127.0.0.1"
                };
                repository.ClientsConnections.Add(clientConnection);
                SocketHandler socketHandler = new SocketHandler(socketConnected);
                new HandleClient().HandleClientMethod(socketConnected, repository, _exit, ConnectedClients,
                socketHandler);
            }
            catch (SocketException se)
            {
                Console.WriteLine("El servidor está cerrándose...");
                _exit = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}