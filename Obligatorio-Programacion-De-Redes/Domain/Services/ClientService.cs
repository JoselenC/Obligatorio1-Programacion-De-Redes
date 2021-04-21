using BusinessLogic;
using DataHandler;

namespace Domain.Services
{
    public class ClientService
    {
        public MemoryRepository Repository { get; set; }
        public ClientService(MemoryRepository repository)
        {
            Repository = repository;
        }
        public void ClientList(SocketHandler socketHandler)
        {
            string data = "";
            if (Repository.ClientsConnections.Count == 0)
            {
                data = "No clients connected";
            }
            else
            {
                foreach (var clientConnection in Repository.ClientsConnections)
                {
                    data += clientConnection.TimeOfConnection + "#" + clientConnection.LocalEndPoint + "#" + 
                            clientConnection.Ip; 
                }
            }
            socketHandler.SendMessage(data);
        }
    }
}