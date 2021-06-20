using System;
using System.Collections.Generic;
using BusinessLogic.Managers;
using DomainObjects;
using LogsServerInterface;

namespace LogsServer
{
    public class LogService : ILogService
    {
        private ManagerLogRepository _managerLogRepository;
        
        public LogService(ManagerLogRepository managerLogRepository)
        {
            _managerLogRepository = managerLogRepository;
           
        }

        public void AddLog(string log)
        {
            Log logToAdd = new Log() {Message = log, CreationDate = DateTime.Now.ToString()};
            _managerLogRepository.Logs.Add(logToAdd);
        }

        public List<Log> Get()
        {
           return _managerLogRepository.Logs.Get();
        }
    }
}