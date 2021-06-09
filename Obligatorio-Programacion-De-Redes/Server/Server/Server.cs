using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Managers;
using BusinessLogic.Services;
using DataHandler;
using Domain;
using Grpc.Net.Client;
using LogServer;
using Server;

namespace ServerGRPC.Server
{
    public class Server
    {
    
       public static readonly List<Socket> ConnectedClients = new List<Socket>();
       private readonly TcpListener _tcpListener;
       private TcpClient _tcpClient;
       private bool exit = false;
       private Log log;

       private IPostService _postService;
       private IThemeService _themeService;
       private IFileService _fileService;
       private ManagerRepository _repository;
       private ManagerPostRepository _postRepository;
       private ManagerThemeRepository _themeRepository;
       
       public Server(ManagerRepository repository,ManagerPostRepository postRepository, ManagerThemeRepository themeRepository)
       {
           _postRepository = postRepository;
           _themeRepository = themeRepository;
           _repository = repository;
           var rabbitHelper = new RabbitHelper();
           log = new Log(rabbitHelper);
           _tcpListener = new TcpListener(IPAddress.Parse(ConfigurationManager.AppSettings["ServerIp"]), Int32.Parse(ConfigurationManager.AppSettings["ServerPort"]));
           _postService = new PostService(repository, log, postRepository, themeRepository );
           _themeService = new ThemeService(repository, log , postRepository, themeRepository);
           _fileService = new FileService(repository, postRepository, log);
       }

       public async Task StartServerAsync()
       {
           Task.Run(async () =>
           {
               new HomePageServer().MenuAsync(_repository,exit,_postRepository,_themeRepository);
           });
           
           while (!exit)
           {
               _tcpListener.Start(1);
               _tcpClient = await _tcpListener.AcceptTcpClientAsync();
               _tcpListener.Stop();
               await AddConnectedClient(_repository, ConnectedClients);
               SocketHandler socketHandler = new SocketHandler(_tcpClient.GetStream());
               await new HandleClient(_tcpListener,log,_postService,_themeService,_fileService)
                   .HandleClientMethodAsync(_repository, false, ConnectedClients, socketHandler);
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

       public async Task AddConnectedClient(ManagerRepository repository,List<Socket> ConnectedClients)
        {
            try
            {
                ConnectedClients.Add(_tcpListener.Server);
                Client clientConnection = new Client()
                {
                    TimeOfConnection = DateTime.Now.ToString(),
                    Ip = _tcpListener.LocalEndpoint.ToString(),
                };
                repository.Clients.Add(clientConnection);
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