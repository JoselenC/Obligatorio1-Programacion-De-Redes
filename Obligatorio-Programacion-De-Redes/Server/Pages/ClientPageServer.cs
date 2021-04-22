using System;
using System.Net.Sockets;
using BusinessLogic;
using Domain;
using DataHandler;

namespace ClientHandler
{
    public class ClientPageServer
    {
        public void ShowClientList(MemoryRepository repository,SocketHandler socketHandler,Socket socketClient)
        {
            Console.Clear();
            if (repository.ClientsConnections.Count == 0 || repository.ClientsConnections==null)
            {
                Console.WriteLine("No hay clientes conectados");
                new HomePageServer().Menu(repository,socketClient,socketHandler);
            }
            else
            {
                for (int i = 0; i < repository.ClientsConnections.Count; i = i + 1)
                {
                    int prefix = i + 1;
                    ClientConnection clientConnection = repository.ClientsConnections[i];
                    Console.WriteLine("Client " + prefix + ":  " + " Hour of connection: " + 
                                     clientConnection.TimeOfConnection + "Port: " + 
                                     clientConnection.LocalEndPoint + "Ip: " + 
                                     clientConnection.Ip + "\n");
                }
                Console.WriteLine(repository.ClientsConnections.Count+1 + "Back");
            }

            Console.ReadLine();
            new HomePageServer().Menu(repository,socketClient,socketHandler);
        }

    }
}