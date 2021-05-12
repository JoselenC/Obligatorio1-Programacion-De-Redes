using System;
using System.Collections.Generic;
using System.Configuration;
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
        public async void HandleClientMethod(Socket clientSocket,MemoryRepository repository,bool _exit, List<Socket> connectedClients,SocketHandler socketHandler)
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
                            await new FileService(repository).UploadFile(socketHandler,clientSocket);
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
                ClientConnected client = repository.ClientsConnections.Find(x =>
                    x.LocalEndPoint ==clientSocket.RemoteEndPoint.ToString());
                repository.ClientsConnections.Remove(client);
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
                ClientConnected clientConnection = new ClientConnected()
                {
                    TimeOfConnection = DateTime.Now.ToString(), 
                    LocalEndPoint = socketConnected.RemoteEndPoint.ToString(),
                    Ip = ConfigurationManager.AppSettings["ServerIp"]
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