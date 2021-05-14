using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Server server = new Server();
            await server.StartServerAsync();
        }

    }
}