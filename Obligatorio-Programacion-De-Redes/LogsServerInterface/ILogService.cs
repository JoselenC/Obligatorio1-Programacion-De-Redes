using System.Collections.Generic;
using DomainObjects;

namespace LogsServerInterface
{
    public interface ILogService
    {
        void AddLog(string log);
        List<Log> Get();
        List<Log> GetByCreationDate(string creationDate);
        List<Log> GetByPost(string postName);
        List<Log> GetByTheme(string themeName);
        List<Log> GetByType(string type);
    }
}