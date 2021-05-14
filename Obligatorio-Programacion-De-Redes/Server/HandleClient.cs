using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using Domain;
using Domain.Services;
using Protocol;

namespace Server
{
    public class HandleClient
    {
        private TcpListener _tcpListener;
         public HandleClient(TcpListener vTcpListener)
         {
             _tcpListener = vTcpListener;
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
                _exit = true;
                ClientConnected client = repository.ClientsConnections.Find(x =>
                 x.LocalEndPoint ==_tcpListener.Server.RemoteEndPoint.ToString());
              repository.ClientsConnections.Remove(client);
              Console.WriteLine("Removing client....");
              connectedClients.Remove(_tcpListener.Server);
            }
        }
        
    }
}