using System.Net.Sockets;
using System.Threading.Tasks;

namespace ProtocolFiles
{
    public class NetworkStreamHandler 
    {
        private readonly NetworkStream _networkStream;

        public NetworkStreamHandler(NetworkStream networkStream)
        {
            _networkStream = networkStream;
        }

        public async Task<byte[]> ReadAsync(int length)
        {
            int dataReceived = 0;
            var data = new byte[length];
            while (dataReceived < length)
            {
                var received = await _networkStream.ReadAsync(data, dataReceived, length - dataReceived);
                if (received == 0)
                {
                    throw new SocketException();
                }
                dataReceived += received;
            }

            return data;
        }

        public async Task WriteAsync(byte[] data)
        {
            await _networkStream.WriteAsync(data, 0, data.Length);
        }
    }
}