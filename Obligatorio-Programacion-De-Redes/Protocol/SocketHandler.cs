using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class SocketHandler 
    {
        public NetworkStream NetworkStream { get;}
        public SocketHandler(NetworkStream vNetworkStream)
        {
            NetworkStream = vNetworkStream;
        }

        public async Task<Packet> ReceivePackageAsync()
        {
            byte[] data = new byte[9];
            Packet packet = new Packet();
            int received = 0;
            while (received < 9)
            {
                int receivedBytes = await NetworkStream.ReadAsync(data);
                if (receivedBytes == 0)
                {
                    throw new SocketException();
                }
                received += receivedBytes;
            }
            String result = Encoding.Default.GetString(data);
            packet.Header = result.Substring(0, HeaderConstants.HeaderLength);
            packet.Command = result.Substring(3, HeaderConstants.CommandLength);
            packet.Length = result.Substring(5, HeaderConstants.Length);
            int length = Int32.Parse(packet.Length) - 5;
            byte[] dataBuffer = new byte[length];
            while (received < length + 9)
            {
                int receivedBytes = await NetworkStream.ReadAsync(dataBuffer);
                if (receivedBytes == 0)
                {
                    throw new SocketException();
                }

                received += receivedBytes;
            }
            result = Encoding.Default.GetString(dataBuffer);
            packet.Data = result;
            return packet;
        }

        public async Task SendPackageAsync(Packet pack)
        {
            try
            {
                string fullCommand = pack.Header;
                fullCommand += pack.Command;
                fullCommand += pack.Length;
                fullCommand += pack.Data;
                byte[] send = Encoding.UTF8.GetBytes(fullCommand);
                await NetworkStream.WriteAsync(send);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}
