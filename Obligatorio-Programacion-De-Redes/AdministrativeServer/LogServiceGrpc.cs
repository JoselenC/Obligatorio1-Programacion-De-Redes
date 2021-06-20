using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DomainObjects;
using Grpc.Net.Client;
using LogsServerInterface;

namespace AdministrativeServer
{
    public class LogServiceGrpc:ILogService
    {
        private readonly LogGrpc.LogGrpcClient _client;
        private readonly IMapper _mapper;
        public LogServiceGrpc()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5002");
            _client = new LogGrpc.LogGrpcClient(channel);
            
            var config = new MapperConfiguration(
                conf =>
                {
                    conf.CreateMap<LogMessage, Log>();
                    conf.CreateMap<Log, LogMessage>();
                });
            _mapper = config.CreateMapper();
        }

        public async Task<List<Log>> GetLogsAsync()
        {
            var reply = await _client.GetLogsAsync(new GetLogsRequest());
            return _mapper.Map<List<Log>>(reply.Logs);
        }

        public Task<List<Log>> GetByCreationDateAsync(string creationDate)
        {
            
            foreach (var log in GetLogsAsync().Result)
            {
                throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }

        public async Task<Log> AddLogAsync(Log log)
        {
            log.Message ??= "";
            log.CreationDate ??= "";
            var postMessage = _mapper.Map<LogMessage>(log);
            AddLogReply reply = await _client.AddLogAsync(
                new AddLogRequest {Log =  postMessage}
            );
            return _mapper.Map<Log>(reply.Log);
        }

    }
}