using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using Domain;
using Domain.Services;
using Protocol;



namespace ClientHandler
{
    public class HandleClient
    {
        public async Task HandleClientMethodAsync(Socket clientSocket,MemoryRepository repository,bool _exit, List<Socket> connectedClients,SocketHandler socketHandler)
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
                            await new FileService(repository).UploadFile(socketHandler,clientSocket);
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
                    x.LocalEndPoint ==clientSocket.RemoteEndPoint.ToString());
                repository.ClientsConnections.Remove(client);
                Console.WriteLine("Removing client....");
                connectedClients.Remove(clientSocket);
            }
        }
        
        public async Task ListenForConnectionsAsync(Socket socketServer, MemoryRepository repository,bool _exit, List<Socket> ConnectedClients)
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
                await new HandleClient().HandleClientMethodAsync(socketConnected, repository, _exit, ConnectedClients,
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