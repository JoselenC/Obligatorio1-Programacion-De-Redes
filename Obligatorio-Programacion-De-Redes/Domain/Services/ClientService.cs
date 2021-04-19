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
            throw new System.NotImplementedException();
        }
    }
}