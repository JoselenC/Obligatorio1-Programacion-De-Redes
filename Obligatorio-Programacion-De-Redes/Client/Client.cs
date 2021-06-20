using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Protocol;

namespace Client
{
    public class Client
    {
        private readonly TcpClient _tcpClient;

        public Client()
        {
            _tcpClient = new TcpClient(new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ClientIp"]), Int32.Parse(ConfigurationManager.AppSettings["ClientPort"])));
        }

        public async Task StartClientAsync()
        {
            await _tcpClient.ConnectAsync(IPAddress.Parse(ConfigurationManager.AppSettings["ClientIp"]), Int32.Parse(ConfigurationManager.AppSettings["ConnectPort"]));
            SocketHandler socketHandler = new SocketHandler(_tcpClient.GetStream());
            await new HomePageClient().MenuAsync(socketHandler,false);
            Console.Clear();
            _tcpClient.Close();
        }
      
    }
}