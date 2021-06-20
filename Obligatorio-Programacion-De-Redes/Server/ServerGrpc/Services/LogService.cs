using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.Managers;
using DataAccess;
using DomainObjects;
using DomainObjects.Exceptions;
using Grpc.Core;

namespace Server.ServerGrpc.Services
{
    public class LogService: LogGrpc.LogGrpcBase
    {
        private readonly RabbitHelper _rabbitHelper;
        private readonly IMapper _mapper;
        private readonly ManagerLogRepository _logRepository;
        private readonly ManagerThemeRepository _themeRepository;
        public LogService()
        {
            _rabbitHelper = new RabbitHelper();
            _themeRepository = new DataBaseThemeRepository();
            _logRepository = new DataBaseLogRepository();
            var config = new MapperConfiguration(
                conf =>
                {
                    conf.CreateMap<LogMessage, Log>();
                    conf.CreateMap<Log, LogMessage>();
                });
            _mapper = config.CreateMapper();
        }


        public override async Task<AddLogReply> AddLog(AddLogRequest request, ServerCallContext context)
        {
                Log logToAdd = new Log() {Message = request.Log.Message, CreationDate = DateTime.Now.Date.ToString()};
                _logRepository.Logs.Add(logToAdd);
                return new AddLogReply
                {
                    Log = new LogMessage()
                    {
                        Message = logToAdd.Message, CreationDate = logToAdd.CreationDate
                    }
                };
        }

        public override async Task<GetLogsReply> GetLogs(GetLogsRequest request, ServerCallContext context)
        {
            GetLogsReply reply = new GetLogsReply();
            IEnumerable<Log> logs =  _logRepository.Logs.Get();
            foreach (var log in logs)
            {
                reply.Logs.Add(new LogMessage()
                {
                    Message = log.Message,
                    CreationDate = log.CreationDate
                });
            }
            return reply;
        }
    }
}