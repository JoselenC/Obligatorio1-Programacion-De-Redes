using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using BusinessLogic;
using Domain;
using Domain.Services;
using Library;
using Protocol;

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
            MemoryRepository repository = new MemoryRepository();
            var threadServer = new Thread(() => ListenForConnections(socketServer,repository));
            threadServer.Start();

            Console.WriteLine("Bienvenido al server, presione enter para salir....");
            Console.ReadLine();
            _exit = true;
            socketServer.Close(0);
            
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

        private static void ListenForConnections(Socket socketServer,MemoryRepository repository)
        {
            while (!_exit)
            {
                try
                {
                    var socketConnected = socketServer.Accept();
                    ConnectedClients.Add(socketConnected);
                    Console.WriteLine("Acepte una nueva conexion");
                    var threadClient = new Thread(() => HandleClient(socketConnected,repository));
                    threadClient.Start();
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
            Console.WriteLine("Saliendo del listen...");
        }

        private static void HandleClient(Socket clientSocket,MemoryRepository repository)
        {
            SocketHandler socketHandler = new SocketHandler(clientSocket);
            try
            {
                while (!_exit)
                {
                    var headerHandler = new HeaderHandler();
                    var buffer = new byte[HeaderConstants.CommandLength + HeaderConstants.DataLength];
                    socketHandler.Receive(HeaderConstants.CommandLength + HeaderConstants.DataLength ); //Revisar este largo 
                    Console.WriteLine("Recibi nueva data");
                    Tuple<short, int> header = headerHandler.DecodeHeader(buffer);
                    switch (header.Item1)
                    {
                       
                        case CommandConstants.CommandAddPost:
                            Console.WriteLine("Recibi comando uno...");
                            new PostService(repository).AddPost(socketHandler,headerHandler);
                            break;
                        case CommandConstants.CommandModifyPost:
                            new PostService(repository).ModifyPost(socketHandler);
                            break;
                        case CommandConstants.CommandDeletePost:
                           new PostService(repository).DeletePost(socketHandler);
                            break;
                        case CommandConstants.CommandAsociateTheme:
                            Console.WriteLine("Recibi comando uno...");
                            new PostService(repository).AsociateTheme();
                            break;
                        case CommandConstants.CommandAddTheme:
                            Console.WriteLine("Recibi comando dos...");
                            new ThemeService().AddTheme();
                            break;
                        case CommandConstants.CommandModifyTheme:
                            Console.WriteLine("El cliente se desconectó");
                            new ThemeService().ModifyTheme();
                            break;
                        case CommandConstants.CommandDeleteTheme:
                            Console.WriteLine("El cliente se desconectó");
                            new ThemeService().DeleteTheme();
                            break;
                        case CommandConstants.CommandBack:
                            Console.WriteLine("El cliente se desconectó");
                            //Your Command code here
                            break;
                        default:
                            Console.WriteLine("No valid command received");
                            break;
                    }
                }
                Console.WriteLine("Sali del while...");
            }
            catch (SocketException e)
            {
                Console.WriteLine("Removing client....");
                ConnectedClients.Remove(clientSocket);
            }
            Console.WriteLine("Saliendo del thread cliente....");
        }

      
    }
}
