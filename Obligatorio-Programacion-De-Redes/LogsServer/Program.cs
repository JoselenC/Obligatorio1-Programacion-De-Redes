using BusinessLogic.Managers;
using DataAccess;

namespace LogsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagerLogRepository managerLogRepository = new DataBaseLogRepository();
            var rabbitClient = new RabbitHelper(managerLogRepository);
            rabbitClient.ReceiveMessages();
            
        }
    }
}