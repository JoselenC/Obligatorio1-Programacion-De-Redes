using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.IServices;
using BusinessLogic.Managers;
using BusinessLogic.Services;
using DomainObjects;
using Protocol;
using Server.Server.Pages;

namespace Server.Server
{
    public class ServerHandler
    {
        private static readonly List<Socket> ConnectedClients = new List<Socket>();
       private readonly TcpListener _tcpListener;
       private TcpClient _tcpClient;
       private bool exit = false;

       private readonly IPostService _postService;
       private readonly IThemeService _themeService;
       private readonly IFileService _fileService;
       private readonly ManagerRepository _repository;
       private readonly ManagerPostRepository _postRepository;
       private readonly ManagerThemeRepository _themeRepository;
       
       public ServerHandler(ManagerRepository repository,ManagerPostRepository postRepository, ManagerThemeRepository themeRepository)
       {
           _postRepository = postRepository;
           _themeRepository = themeRepository;
           _repository = repository;
           var rabbitClient = new RabbitHelper();
           _tcpListener = new TcpListener(IPAddress.Parse(ConfigurationManager.AppSettings["ServerIp"]),
               Int32.Parse(ConfigurationManager.AppSettings["ServerPort"]));
           _postService = new PostService(rabbitClient, postRepository, themeRepository );
           _themeService = new ThemeService(rabbitClient , postRepository, themeRepository);
           _fileService = new FileService(repository, postRepository, rabbitClient);
       }

       public async Task StartServerAsync(string[] args)
       {
           await Task.Run(async () =>
           {
               await new HomePageServer().MenuAsync(_repository,exit,_postRepository,_themeRepository);
           });
           await Task.Run(async () => { Program.RunGrpc(args); });
           while (!exit)
           {
               _tcpListener.Start(1);
               _tcpClient = await _tcpListener.AcceptTcpClientAsync();
               _tcpListener.Stop();
               AddConnectedClient(_repository, ConnectedClients);
               SocketHandler socketHandler = new SocketHandler(_tcpClient.GetStream());
               await new HandleClient(_tcpListener,_postService,_themeService,_fileService)
                   .HandleClientMethodAsync(_repository, false, ConnectedClients, socketHandler);
           }
         
           
           foreach (var socketClient in ConnectedClients)
           {
               try
               {
                   socketClient.Shutdown(SocketShutdown.Both);
                   socketClient.Close(1);
               }
               catch (Exception)
               {
                   Console.WriteLine("Socket client is already closed");
               }

               Console.WriteLine("Exit Main Thread...");
           }
       }

       public void AddConnectedClient(ManagerRepository repository,List<Socket> connectedClients)
        {
            try
            {
                connectedClients.Add(_tcpListener.Server);
                Client clientConnection = new Client()
                {
                    TimeOfConnection = DateTime.Now.ToString(),
                    Ip = _tcpListener.LocalEndpoint.ToString(),
                };
                repository.Clients.Add(clientConnection);
            }
            catch (SocketException se)
            {
                Console.WriteLine("The server is close");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
       
    }
}