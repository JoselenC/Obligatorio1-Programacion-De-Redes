using System;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Protocol;


namespace DataHandler
{
    public class SocketHandler 
    {
        public NetworkStream networkStream { get; set; }
        public SocketHandler(NetworkStream vNetworkStream)
        {
            networkStream = vNetworkStream;
        }

        public async Task<Packet> ReceivePackgAsync()
        {
            byte[] data = new byte[9];
            Packet packet = new Packet();
            int received = 0;
            while (received < 9)
            {
                int receivedBytes = await networkStream.ReadAsync(data);
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
                int receivedBytes = await networkStream.ReadAsync(dataBuffer);
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

        public async Task SendPackgAsync(Packet pack)
        {
            try
            {
                String fullCommand = pack.Header.ToString();
                fullCommand += pack.Command.ToString();
                fullCommand += pack.Length.ToString();
                fullCommand += pack.Data.ToString();
                byte[] send = Encoding.UTF8.GetBytes(fullCommand);
                await networkStream.WriteAsync(send);
            }
            catch (Exception e)
            {

            }
        }
    }
}
