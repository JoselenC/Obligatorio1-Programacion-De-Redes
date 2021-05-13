using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using Domain;
using Domain.Services;
using Protocol;
using ProtocolFiles;


namespace ClientHandler
{
    public class HandleClient
    {
    
       public static readonly List<Socket> ConnectedClients = new List<Socket>();
       private readonly TcpListener _tcpListener;
       private readonly FileHandler _fileHandler;
       private readonly IFileStreamHandler _fileStreamHandler;
       private TcpClient _tcpClient;

       public HandleClient()
       {
           _tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 6000);
           _fileHandler = new FileHandler();
           _fileStreamHandler = new FileStreamHandler();
       }

        public async Task StartServerAsync()
        {
            _tcpListener.Start(1);
            _tcpClient = await _tcpListener.AcceptTcpClientAsync();
            _tcpListener.Stop();
            SocketHandler socketHandler = new SocketHandler(_tcpClient.GetStream());
            MemoryRepository repository = new MemoryRepository();
            
            await new HomePageServer().MenuAsync(repository,socketHandler);
            await ListenForConnectionsAsync(false,repository,ConnectedClients);
          
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
        
        public async Task ListenForConnectionsAsync(bool _exit,MemoryRepository repository,List<Socket> ConnectedClients)
        {
            try
            {
                ConnectedClients.Add(_tcpListener.Server);
                ClientConnected clientConnection = new ClientConnected()
                {
                    TimeOfConnection = DateTime.Now.ToString(),
                    LocalEndPoint = ConfigurationManager.AppSettings["ClientPort"],
                    Ip = ConfigurationManager.AppSettings["ServerIp"]
                };
                repository.ClientsConnections.Add(clientConnection);
                SocketHandler socketHandler = new SocketHandler(_tcpClient.GetStream());
                await HandleClientMethodAsync(repository, _exit, ConnectedClients, socketHandler);
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
        
        public async Task HandleClientMethodAsync(MemoryRepository repository,bool _exit, List<Socket> connectedClients,SocketHandler socketHandler)
        {
            try
            {
                while (!_exit)
                {
                    var packet = await socketHandler.ReceivePackgAsync();
                    var command= Int32.Parse(packet.Command);
                    switch (command)
                    {
                        case CommandConstants.CommandAddPost:
                            await new PostService(repository).AddPostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandModifyPost:
                            await new PostService(repository).ModifyPostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandDeletePost:
                            await new PostService(repository).DeletePostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandAsociateTheme:
                            await new PostService(repository).AsociateThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandAsociateThemeToPost:
                            await new PostService(repository).AsociateThemeToPostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandDisassociateTheme:
                            await new PostService(repository).DisassociateThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandAddTheme:
                            await new ThemeService(repository).AddThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandModifyTheme:
                            await new ThemeService(repository).ModifyThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandDeleteTheme:
                            await new ThemeService(repository).DeleteThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandUploadFile:
                            await new FileService(repository).UploadFile(socketHandler);
                            break;
                        case CommandConstants.SearchPost:
                            await new PostService(repository).SearchPostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandBack:
                            Console.WriteLine("The client logged out");
                            break;
                        default:
                            break;
                    }
                  
                }
            }
            catch (SocketException e)
            {
                ClientConnected client = repository.ClientsConnections.Find(x =>
                 x.LocalEndPoint ==_tcpListener.Server.RemoteEndPoint.ToString());
              repository.ClientsConnections.Remove(client);
              Console.WriteLine("Removing client....");
              connectedClients.Remove(_tcpListener.Server);
            }
        }
        
     
    }
}