using System.Collections.Generic;
using DomainObjects;

namespace LogsServerInterface
{
    public interface ILogService
    {
        void AddLog(string log);
        List<Log> Get();
        List<Log> GetByCreationDate(string creationDate);
    }
}