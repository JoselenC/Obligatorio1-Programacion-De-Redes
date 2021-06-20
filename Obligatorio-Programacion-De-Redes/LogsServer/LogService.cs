using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BusinessLogic.Managers;
using DomainObjects;
using DomainObjects.Exceptions;
using Grpc.Core;
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
            Log logToAdd = new Log() {Message = log, CreationDate = DateTime.Now.Date.ToString()};
            _managerLogRepository.Logs.Add(logToAdd);
        }

        public List<Log> Get()
        {
           return _managerLogRepository.Logs.Get();
        }
        
        public void ValidateCreationDate(string date)
        {
            bool goodFormat = false;
            Regex regex = new Regex(@"\b\d{2}(/|-|.|\s)\d{2}(/|-|.|\s)(\d{4})");
            var match = regex.Match(date);
            if (!match.Success)
                throw new InvalidCreationDate();
        }

        public List<Log> GetByCreationDate(string creationDate)
        {
            try
            {
                ValidateCreationDate(creationDate);
                List<Log> logsByCreationDate = new List<Log>();
                foreach (var log in _managerLogRepository.Logs.Get())
                {
                    if (log.CreationDate == creationDate)
                        logsByCreationDate.Add(log);
                }

                return logsByCreationDate;
            }
            catch (InvalidCreationDate)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid format creation date: the date format must be: dd/mm/yyyy \n "));
            }
        }
    }
}