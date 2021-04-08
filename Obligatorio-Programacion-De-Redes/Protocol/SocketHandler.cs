using System.Net.Sockets;

namespace Protocol
{
    public class SocketHandler:ISocketHandler
    {
        private readonly Socket _socket;

        public SocketHandler(Socket socket)
        {
            _socket = socket;
        }

        public void Send(byte[] data)
        {
            int totalSentBytes = 0;
            while (totalSentBytes < data.Length)
            {
                int sentBytes = _socket.Send(
                    data,
                    totalSentBytes,
                    data.Length - totalSentBytes,
                    SocketFlags.None);
                if (sentBytes == 0)
                {
                    throw new SocketException();
                }
                totalSentBytes += sentBytes;
            }
        }

        public byte[] Receive(int size)
        {
            int totalReceivedBytes = 0;
            var data = new byte[size];
            while (totalReceivedBytes < size)
            {
                int receivedBytes = _socket.Receive(
                    data,
                    totalReceivedBytes,
                    size - totalReceivedBytes,
                    SocketFlags.None);
                if (receivedBytes == 0)
                {
                    throw new SocketException();
                }
                totalReceivedBytes += receivedBytes;
            }

            return data;
        }
    }
}
