using System;
using System.Net.Sockets;
using BusinessLogic;
using Domain;
using DataHandler;

namespace ClientHandler
{
    public class ClientPageServer
    {
        public void ShowClientList(MemoryRepository repository)
        {
            Console.Clear();
            if (repository.ClientsConnections.Count == 0 || repository.ClientsConnections==null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hay clientes conectados");
                Console.ForegroundColor = ConsoleColor.White;
                new HomePageServer().MenuAsync(repository);
            }
            else
            {
                for (int i = 0; i < repository.ClientsConnections.Count; i = i + 1)
                {
                    int prefix = i + 1;
                    ClientConnected clientConnection = repository.ClientsConnections[i];
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Client " + prefix + ":  ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" Hour of connection: " + 
                                     clientConnection.TimeOfConnection + "  Port: " + 
                                     clientConnection.LocalEndPoint + "  Ip: " + 
                                     clientConnection.Ip + "\n");
                }

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(repository.ClientsConnections.Count+1 + ".  Back");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ReadLine();
            new HomePageServer().MenuAsync(repository);
        }

    }
}