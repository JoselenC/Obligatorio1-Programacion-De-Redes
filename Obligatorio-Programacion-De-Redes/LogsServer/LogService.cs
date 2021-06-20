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
            string[] dataLog = log.Split("#");
            Log logToAdd = new Log()
            {
                Message = dataLog[0], CreationDate = DateTime.Now.Date.ToShortDateString(),
                ObjectName = dataLog[2], ObjectType = dataLog[1]
            };
            _managerLogRepository.Logs.Add(logToAdd);
        }

        public List<Log> Get()
        {
           return _managerLogRepository.Logs.Get();
        }

        public List<Log> GetByCreationDate(string creationDate)
        {
            List<Log> logsByCreationDate = new List<Log>();
            foreach (var log in _managerLogRepository.Logs.Get())
            {
                if (log.CreationDate == creationDate)
                    logsByCreationDate.Add(log);
            }

            return logsByCreationDate;
        }


        public List<Log> GetByPost(string postName)
        {
            List<Log> logsByPost = new List<Log>();
            foreach (var log in _managerLogRepository.Logs.Get())
            {
                if (log.ObjectType.ToLower() == "post" && log.ObjectName.ToLower() == postName.ToLower())
                    logsByPost.Add(log);
            }

            return logsByPost;
        }
        
        public List<Log> GetByTheme(string themeName)
        {
            List<Log> logsByTheme = new List<Log>();
            foreach (var log in _managerLogRepository.Logs.Get())
            {
                if (log.ObjectType.ToLower() == "theme" && log.ObjectName.ToLower() == themeName.ToLower())
                    logsByTheme.Add(log);
            }

            return logsByTheme;
        }
        
        public List<Log> GetByType(string type)
        {
            List<Log> logsByType = new List<Log>();
            foreach (var log in _managerLogRepository.Logs.Get())
            {
                if (log.ObjectType.ToLower() == type.ToLower())
                    logsByType.Add(log);
            }

            return logsByType;
        }
    }
}