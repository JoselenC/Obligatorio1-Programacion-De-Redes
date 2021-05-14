using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serverHandler = new Client();
            await serverHandler.StartClientAsync();
        }
    }
}