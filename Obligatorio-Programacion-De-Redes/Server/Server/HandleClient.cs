using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using Domain;
using Domain.Services;
using LogServer;
using Protocol;

namespace ServerGRPC.Server
{
    public class HandleClient
    {
        private TcpListener _tcpListener;
        private Log log;
        private IPostService _postService;
        private IThemeService _themeService;
        private IFileService _fileService;
         public HandleClient(TcpListener vTcpListener,Log log, 
             IPostService postService,IThemeService themeService, IFileService fileService)
         {
             _fileService = fileService;
             _themeService = themeService;
             _postService = postService;
             this.log = log;
             _tcpListener = vTcpListener;
         }
         
          public async Task HandleClientMethodAsync(ManagerRepository repository,bool _exit, List<Socket> connectedClients,SocketHandler socketHandler)
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
                            await _postService.AddPostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandModifyPost:
                            await _postService.ModifyPostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandDeletePost:
                            await _postService.DeletePostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandAsociateTheme:
                            await _postService.AsociateThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandAsociateThemeToPost:
                            await _postService.AsociateThemeToPostAsync(socketHandler);
                            break;
                        case CommandConstants.CommandDisassociateTheme:
                            await _postService.DisassociateThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandAddTheme:
                            await _themeService.AddThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandModifyTheme:
                            await _themeService.ModifyThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandDeleteTheme:
                            await _themeService.DeleteThemeAsync(socketHandler);
                            break;
                        case CommandConstants.CommandUploadFile:
                            await _fileService.UploadFile(socketHandler);
                            break;
                        case CommandConstants.SearchPost:
                            await _postService.SearchPostAsync(socketHandler);
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
                Client client = repository.Clients.Find(x =>
                 x.LocalEndPoint ==_tcpListener.LocalEndpoint.ToString());
              repository.Clients.Delete(client);
              Console.WriteLine("Removing client....");
              connectedClients.Remove(_tcpListener.Server);
            }
        }
        
    }
}