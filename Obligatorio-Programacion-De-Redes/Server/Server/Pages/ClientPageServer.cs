using System;
using BusinessLogic.Managers;
using DomainObjects;

namespace Server.Server.Pages
{
    public class ClientPageServer
    {
        public void ShowClientList(ManagerRepository repository,ManagerPostRepository managerPostRepository,ManagerThemeRepository managerThemeRepository)
        {
            Console.Clear();
            if (repository.Clients.Get().Count == 0 || repository.Clients.Get()==null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hay clientes conectados");
                Console.ForegroundColor = ConsoleColor.White;
                new HomePageServer().MenuAsync(repository,false,managerPostRepository,managerThemeRepository);
            }
            else
            {
                for (int i = 0; i < repository.Clients.Get().Count; i = i + 1)
                {
                    int prefix = i + 1;
                    Client clientConnection = repository.Clients.Get()[i];
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Client " + prefix + ":  ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" Hour of connection: " + 
                                     clientConnection.TimeOfConnection + "  Ip: " + 
                                     clientConnection.Ip + "\n");
                }

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(repository.Clients.Get().Count+1 + ".  Back");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ReadLine();
            new HomePageServer().MenuAsync(repository,false,managerPostRepository,managerThemeRepository);
        }

    }
}