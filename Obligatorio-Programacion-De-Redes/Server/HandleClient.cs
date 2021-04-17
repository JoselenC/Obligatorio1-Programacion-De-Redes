using System;
using System.Collections.Generic;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;
using Domain.Services;
using Protocol;

namespace Server
{
    public class HandleClient
    {
        public void HandleClientMethod(Socket clientSocket,MemoryRepository repository,bool _exit, List<Socket> connectedClients)
        {
            SocketHandler socketHandler = new SocketHandler(clientSocket);
            try
            {
                while (!_exit)
                {
                    var headerHandler = new HeaderHandler();
                    var buffer = new byte[HeaderConstants.CommandLength + HeaderConstants.DataLength]; 
                    buffer= socketHandler.Receive(HeaderConstants.CommandLength+ HeaderConstants.DataLength); 
                    Console.WriteLine("Recibi nueva data");
                    
                    Tuple<short, int> header = headerHandler.DecodeHeader(buffer);
                    switch (header.Item1)
                    {
                        case CommandConstants.CommandAddPost:
                            Console.WriteLine("Adding new post");
                            new PostService(repository).AddPost(socketHandler);
                            break;
                        case CommandConstants.CommandModifyPost:
                            Console.WriteLine("Modifying post");
                            new PostService(repository).ModifyPost(socketHandler);
                            break;
                        case CommandConstants.CommandDeletePost:
                            Console.WriteLine("Deleting post");
                            new PostService(repository).DeletePost(socketHandler);
                            break;
                        case CommandConstants.CommandAsociateTheme:
                            Console.WriteLine("Associating theme");
                            new PostService(repository).AsociateTheme(socketHandler);
                            break;
                        case CommandConstants.CommandAddTheme:
                            Console.WriteLine("Adding new theme");
                            new ThemeService(repository).AddTheme(socketHandler);
                            break;
                        case CommandConstants.CommandModifyTheme:
                            Console.WriteLine("Modifying theme");
                            new ThemeService(repository).ModifyTheme(socketHandler);
                            break;
                        case CommandConstants.CommandDeleteTheme:
                            Console.WriteLine("Deleting theme");
                            new ThemeService(repository).DeleteTheme(socketHandler);
                            break;
                        case CommandConstants.CommandUploadFile:
                            Console.WriteLine("Upload file");
                            new FileService(repository).UploadFile(socketHandler);
                            break;
                        case CommandConstants.SearchPost:
                            Console.WriteLine("Serch post");
                            new PostService(repository).SearchPost(socketHandler);
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
                connectedClients.Remove(clientSocket);
            }
        }
    }
}