using System;
using System.Threading.Tasks;
using AdministrativeServer;
using BusinessLogic.Managers;
using DataAccess;
using DomainObjects;

namespace LogsServer
{
    class Program
    {
        private static LogServiceGrpc logService = new LogServiceGrpc();
        static void Main(string[] args)
        {
            ManagerLogRepository managerLogRepository = new DataBaseLogRepository();
            var rabbitClient = new RabbitHelper(managerLogRepository);
            rabbitClient.ReceiveMessagesAsync(AddLog);
            
        }
        
        public static async Task AddLog(string message)
        {
            await logService.AddLogAsync(new Log
            {
                CreationDate = DateTime.Now.Date.ToString(),
                Message = message
            });
        }
    }
}