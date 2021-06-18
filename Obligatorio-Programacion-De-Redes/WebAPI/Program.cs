using BusinessLogic.Managers;
using DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ManagerLogRepository _managerLogRepository = new DataBaseLogRepository();
            var rabbitClient = new RabbitHelper(_managerLogRepository);
            rabbitClient.ReceiveMessages();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}