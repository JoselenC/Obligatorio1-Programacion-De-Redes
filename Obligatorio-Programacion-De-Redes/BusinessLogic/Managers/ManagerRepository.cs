using DomainObjects;

namespace BusinessLogic.Managers
{
    public abstract class ManagerRepository
    {
        public IRepository<File> Files { get; set; }
        public IRepository<Client> Clients { get; set; }
        
        
    }
}