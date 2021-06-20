using System.Collections.Generic;
using System.Threading.Tasks;
using DomainObjects;

namespace LogsServerInterface
{
    public interface ILogService
    {
        Task<Log> AddLogAsync(Log log);
        Task<List<Log>> GetLogsAsync();
        Task<List<Log>> GetByCreationDateAsync(string creationDate);
    }
}