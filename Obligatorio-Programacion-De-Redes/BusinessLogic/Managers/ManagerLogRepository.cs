
using DomainObjects;

namespace BusinessLogic.Managers
{
    public class ManagerLogRepository
    {
        public IRepository<Log> Logs { get; set; }

    }
}