using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using Domain;
using Domain.Services;
using LogServer;
using Protocol;

namespace Server
{
    public class HandleClient
    {
        private TcpListener _tcpListener;
        private Log log;
         public HandleClient(TcpListener vTcpListener,Log log)
         {
             this.log = log;
             _tcpListener = vTcpListener;
         }
          public async Task HandleClientMethodAsync(MemoryRepository repository,bool _exit, List<Socket> connectedClients,SocketHandler socketHandler)
        {
          SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);
            try
            {
                while (!_exit)
                {
                    var packet = await socketHandler.ReceivePackgAsync();
                    var command= Int32.Parse(packet.Command);
                    switch (command)
                    {
                        case CommandConstants.CommandAddPost:
                            await new PostService(repository,log).AddPostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandModifyPost:
                            await new PostService(repository,semaphoreSlim,log).ModifyPostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandDeletePost:
                            await new PostService(repository,semaphoreSlim,log).DeletePostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandAsociateTheme:
                            await new PostService(repository,log).AsociateThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandAsociateThemeToPost:
                            await new PostService(repository,log).AsociateThemeToPostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandDisassociateTheme:
                            await new PostService(repository,log).DisassociateThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandAddTheme:
                            await new ThemeService(repository,log).AddThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandModifyTheme:
                            await new ThemeService(repository,semaphoreSlim,log).ModifyThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandDeleteTheme:
                            await new ThemeService(repository,semaphoreSlim,log).DeleteThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandUploadFile:
                            await new FileService(repository,log).UploadFile(socketHandler);
                            break;
                        case CommandConstants.SearchPost:
                            await new PostService(repository,log).SearchPostAsync(socketHandler);
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
                _exit = true;
                ClientConnected client = repository.ClientsConnections.Find(x =>
                 x.LocalEndPoint ==_tcpListener.LocalEndpoint.ToString());
              repository.ClientsConnections.Remove(client);
              Console.WriteLine("Removing client....");
              connectedClients.Remove(_tcpListener.Server);
            }
        }
        
    }
}