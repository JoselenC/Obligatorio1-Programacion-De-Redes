using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Managers;
using DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Server.Server;
using ServerGRPC;

namespace Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ManagerRepository repository = new DataBaseManagerRepository();
            ManagerPostRepository managerPostRepository = new DataBasePostRepository();
            ManagerThemeRepository managerThemeRepository = new DataBaseThemeRepository();
            ServerHandler server = new ServerHandler(repository,managerPostRepository,managerThemeRepository);
            await server.StartServerAsync(args);
           
        }

        public static void RunGrpc(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        
        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        // Setup a HTTP/2 endpoint without TLS.
                        options.ListenLocalhost(5002, o => o.Protocols = 
                            HttpProtocols.Http2);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}