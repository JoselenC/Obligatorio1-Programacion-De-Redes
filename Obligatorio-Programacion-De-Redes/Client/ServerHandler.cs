using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using DataHandler;
using ProtocolFiles;

namespace Client
{
    public class ServerHandler
    {
        private readonly TcpClient _tcpClient;

        public ServerHandler()
        {
            _tcpClient = new TcpClient(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6001));
        }

        public async Task StartClientAsync()
        {
            await _tcpClient.ConnectAsync(IPAddress.Parse("127.0.0.1"), 6000);
            SocketHandler socketHandler = new SocketHandler(_tcpClient.GetStream());
            await new HomePageClient().MenuAsync(socketHandler);
        }
      
    }
}