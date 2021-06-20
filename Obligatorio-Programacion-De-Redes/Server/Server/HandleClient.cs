using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using BusinessLogic.IServices;
using BusinessLogic.Managers;
using DomainObjects;
using Protocol;

namespace Server.Server
{
    public class HandleClient
    {
        private readonly TcpListener _tcpListener;
        private readonly IPostService _postService;
        private readonly IThemeService _themeService;
        private readonly IFileService _fileService;
         public HandleClient(TcpListener vTcpListener, IPostService postService,IThemeService themeService, IFileService fileService)
         {
             _fileService = fileService;
             _themeService = themeService;
             _postService = postService;
             _tcpListener = vTcpListener;
         }

         public async Task HandleClientMethodAsync(ManagerRepository repository, bool exit,
             List<Socket> connectedClients, SocketHandler socketHandler)
         {
             try
             {
                 while (!exit)
                 {
                     var packet = await socketHandler.ReceivePackageAsync();
                     var command = Int32.Parse(packet.Command);
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
                         case CommandConstants.CommandAssociateTheme:
                             await _postService.AssociateThemeAsync(socketHandler);
                             break;
                         case CommandConstants.CommandAssociateThemeToPost:
                             await _postService.AssociateThemeToPostAsync(socketHandler);
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
             catch (SocketException)
             {
                 exit = true;
                 Client client = repository.Clients.Find(x =>
                     x.Ip == _tcpListener.LocalEndpoint.ToString());
                 repository.Clients.Delete(client);
                 Console.WriteLine("Removing client....");
                 connectedClients.Remove(_tcpListener.Server);
             }
         }

    }
}