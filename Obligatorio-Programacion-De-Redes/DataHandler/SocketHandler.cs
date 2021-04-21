using System;
using System.Net.Sockets;
using Protocol;

namespace DataHandler
{
    public class SocketHandler : ISocketHandler
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
        
        public byte[] Receive(int size,Object lockSync)
        {
            lock (lockSync)
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

        public string[] ReceiveMessage()
        {
            var dataLength = Receive(HeaderConstants.DataLength);
            int length = BitConverter.ToInt32(dataLength);
            var data = Receive(length);
            string message = System.Text.Encoding.UTF8.GetString(data);
            string[] messageArray = message.Split("#");
            return messageArray;
        }

        public void SendMessage(string message)
        {
            byte[] data2 = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength2 = BitConverter.GetBytes(data2.Length);
            Send(dataLength2);
            Send(data2);
        }

        private static byte[] ConvertDataToHeader(short command, int data)
        {
            return HeaderHandler.EncodeHeader(command, data);
        }

        public void SendData(short command, Socket SocketClient)
        {
            if (SocketClient.Send(ConvertDataToHeader(command, new Random().Next())) == 0)
            {
                throw new SocketException();
            }
        }
        
    }
}
