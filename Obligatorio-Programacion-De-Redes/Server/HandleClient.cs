using System;
using System.Collections.Generic;
using System.Net.Sockets;
using BusinessLogic;
using DataHandler;
using Domain;
using Domain.Services;
using Protocol;



namespace ClientHandler
{
    public class HandleClient
    {
        public void HandleClientMethod(Socket clientSocket,MemoryRepository repository,bool _exit, List<Socket> connectedClients,SocketHandler socketHandler)
        { 
            
            try
            {
                while (!_exit)
                {
                    var packet = socketHandler.ReceivePackg();
                    var command= Int32.Parse(packet.Command);
                    switch (command)
                    {
                        case CommandConstants.CommandAddPost:
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
                            new FileService(repository).UploadFile(socketHandler,clientSocket);
                            break;
                        case CommandConstants.SearchPost:
                            new PostService(repository).SearchPost(socketHandler);
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
                Console.WriteLine("Removing client....");
                connectedClients.Remove(clientSocket);
            }
        }
        
        public void ListenForConnections(Socket socketServer, MemoryRepository repository,bool _exit, List<Socket> ConnectedClients)
        {
            try
            {
                var socketConnected = socketServer.Accept();
                ConnectedClients.Add(socketConnected);
                ClientConnection clientConnection = new ClientConnection()
                {
                    TimeOfConnection = new DateTime().ToLocalTime().ToString(),
                    LocalEndPoint = socketConnected.LocalEndPoint.ToString(),
                    Ip = "127.0.0.1"
                };
                repository.ClientsConnections.Add(clientConnection);
                SocketHandler socketHandler = new SocketHandler(socketConnected);
                new HandleClient().HandleClientMethod(socketConnected, repository, _exit, ConnectedClients,
                    socketHandler);
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
    }
}