using System;
using BusinessLogic.Managers;
using DataAccess;
using LogsServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport",true); 
            ManagerLogRepository managerLogRepository = new DataBaseLogRepository();
            var rabbitClient = new RabbitHelper(managerLogRepository);
            rabbitClient.ReceiveMessages();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}