using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using BusinessLogic;
using DataHandler;
using Domain.Services;
using Protocol;

namespace Client
{
    public class ServerHandler
    {
         public void HandleClientMethod(Socket clientSocket,MemoryRepository repository,bool _exit)
        {
            SocketHandler socketHandler = new SocketHandler(clientSocket);
            try
            {
                while (!_exit)
                {
                    var headerHandler = new HeaderHandler();
                    var buffer = new byte[HeaderConstants.CommandLength + HeaderConstants.DataLength];
                    
                   new HomePageClient().Menu(clientSocket, socketHandler);
                    buffer = socketHandler.Receive(HeaderConstants.CommandLength + HeaderConstants.DataLength);
                    Tuple<short, int> header = headerHandler.DecodeHeader(buffer);
                    switch (header.Item1)
                    {
                        case CommandConstants.CommandClientList:
                            new ClientService(repository).ClientList(socketHandler);
                            break;
                        case CommandConstants.CommandShowThemePostByCreationDate:
                            Console.WriteLine("holasd");
                            new PostService(repository).ShowThemePostByCreationDate(socketHandler);
                            break;
                        case CommandConstants.CommandShowThemePostByTheme:
                            new PostService(repository).ShowThemePostByTheme(socketHandler);
                            break;
                        case CommandConstants.CommandShowThemePostByDateAndTheme:
                            new PostService(repository).ShowThemePostByDateAndTheme(socketHandler);
                            break;
                        case CommandConstants.CommandShowPost:
                            new PostService(repository).ShowPost(socketHandler);
                            break;
                        case CommandConstants.CommandShowFilePost:
                            new PostService(repository).ShowFilePost(socketHandler);
                            break;
                        case CommandConstants.CommandShowThemeList:
                            new ThemeService(repository).ShowThemeList(socketHandler);
                            break;
                        case CommandConstants.CommandShowThemeWithMorePosts:
                            new PostService(repository).ShowThemeWithMorePosts(socketHandler);
                            break;
                        case CommandConstants.CommandShowFileList:
                            new FileService(repository).UploadFile(socketHandler);
                            break;
                        case CommandConstants.CommandBack:
                            Console.WriteLine("The client logged out");
                            //Your Command code here
                            break;
                        default:
                            Console.WriteLine("No valid command received");
                            break;
                    }
                
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("Removing client....");
            }
        }
    }
}