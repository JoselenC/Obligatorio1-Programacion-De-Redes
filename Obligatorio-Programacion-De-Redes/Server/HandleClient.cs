using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using BusinessLogic;
using Client;
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
                    new HomePageServer().Menu(clientSocket, socketHandler);
                    buffer = socketHandler.Receive(HeaderConstants.CommandLength + HeaderConstants.DataLength);
                    Tuple<short, int> header = headerHandler.DecodeHeader(buffer);
                    switch (header.Item1)
                    {
                        case CommandConstants.CommandAddPost:
                            Console.WriteLine("add post");
                                new PostService(repository).AddPost(socketHandler);
                                break;
                        case CommandConstants.CommandModifyPost:
                            new PostService(repository).ModifyPost(socketHandler);
                            break;
                        case CommandConstants.CommandDeletePost:
                            new PostService(repository).DeletePost(socketHandler);
                            break;
                        case CommandConstants.CommandAsociateTheme:
                            new PostService(repository).AsociateTheme(socketHandler);
                            break;
                        case CommandConstants.CommandAsociateThemeToPost:
                            new PostService(repository).AsociateThemeToPost(socketHandler);
                            break;
                        case CommandConstants.CommandDisassociateTheme:
                            new PostService(repository).DisassociateTheme(socketHandler);
                            break;
                        case CommandConstants.CommandAddTheme:
                            new ThemeService(repository).AddTheme(socketHandler);
                            break;
                        case CommandConstants.CommandModifyTheme:
                            new ThemeService(repository).ModifyTheme(socketHandler);
                            break;
                        case CommandConstants.CommandDeleteTheme:
                            new ThemeService(repository).DeleteTheme(socketHandler);
                            break;
                        case CommandConstants.CommandUploadFile:
                            new FileService(repository).UploadFile(socketHandler);
                            break;
                        case CommandConstants.SearchPost:
                            new PostService(repository).SearchPost(socketHandler);
                            break;
                        case CommandConstants.CommandBack:
                            Console.WriteLine("The client logged out");
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

        public void ShowMenu(string[] _options)
        {
            bool salir = false;
            int index = 0;
            Console.WriteLine("----Menu----");
            for (var i = 0; i < _options.Length; i++)
            {
                var prefix = "  ";
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    prefix = "> ";
                }

                Console.WriteLine($"{prefix}{_options[i]}");
            }

        }

    }
}