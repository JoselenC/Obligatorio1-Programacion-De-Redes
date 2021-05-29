using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using Domain;
using LogServer;


namespace Server
{
    public class Server
    {
    
       public static readonly List<Socket> ConnectedClients = new List<Socket>();
       private readonly TcpListener _tcpListener;
       private TcpClient _tcpClient;
       private bool exit = false;
       private Log log;

       public Server()
       {
           var rabbitHelper = new RabbitHelper();
           log = new Log(rabbitHelper);

           _tcpListener = new TcpListener(IPAddress.Parse(ConfigurationManager.AppSettings["ServerIp"]), Int32.Parse(ConfigurationManager.AppSettings["ServerPort"]));
       }

       public async Task StartServerAsync()
       {
           MemoryRepository repository = new MemoryRepository();
           Task.Run(async () =>
           {
               new HomePageServer().MenuAsync(repository,exit);
           });

           while (!exit)
           {
               _tcpListener.Start(1);
               _tcpClient = await _tcpListener.AcceptTcpClientAsync();
               _tcpListener.Stop();
               await AddConnectedClient(repository, ConnectedClients);
               SocketHandler socketHandler = new SocketHandler(_tcpClient.GetStream());
               await new HandleClient(_tcpListener,log).HandleClientMethodAsync(repository, false, ConnectedClients, socketHandler);
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

       public async Task AddConnectedClient(MemoryRepository repository,List<Socket> ConnectedClients)
        {
            try
            {
                ConnectedClients.Add(_tcpListener.Server);
                ClientConnected clientConnection = new ClientConnected()
                {
                    TimeOfConnection = DateTime.Now.ToString(),
                    LocalEndPoint = _tcpListener.LocalEndpoint.ToString(),
                    Ip = ConfigurationManager.AppSettings["ServerIp"]
                };
                repository.ClientsConnections.Add(clientConnection);
            }
            catch (SocketException se)
            {
                Console.WriteLine("El servidor está cerrándose...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
       
    }
}