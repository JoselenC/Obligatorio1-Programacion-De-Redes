using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Server clientHandler = new Server();
            await clientHandler.StartServerAsync();
        }

    }
}