using System;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using Protocol;


namespace DataHandler
{
    public class SocketHandler 
    {
        private readonly Socket _socket;

        public SocketHandler(Socket socket)
        {
            _socket = socket;
        }
        
       public Packet ReceivePackg()
        {
                byte[] data = new byte[105];
                Packet packet = new Packet();
                int received = 0;
                while (received < 105)
                {
                    int receivedBytes = _socket.Receive(data);
                    if (receivedBytes==0)
                    {
                        throw new SocketException();
                    }
                    received += receivedBytes;
                }
                String result = Encoding.Default.GetString(data);
                packet.Header = result.Substring(0, HeaderConstants.HeaderLength);
                packet.Command = result.Substring(3, HeaderConstants.CommandLength);
                packet.Length = result.Substring(5, HeaderConstants.PacketLength);
                int length = Int32.Parse(packet.Length);
                byte[] dataBuffer = new byte[length];
                while (received < length)
                {
                    int receivedBytes = _socket.Receive(dataBuffer);
                    if (receivedBytes==0)
                    {
                        throw new SocketException();
                    }
                    received += receivedBytes;
                }
                result = Encoding.Default.GetString(dataBuffer);
                packet.Data = result.Split("\0")[0];
                return packet;
            
        }
        

        public void SendPackg(Packet pack)
        {
            try
            {
                String fullCommand = pack.Header;
                fullCommand += pack.Command.ToString().PadLeft(HeaderConstants.CommandLength, '0');
                fullCommand += pack.Length.ToString().PadLeft(HeaderConstants.PacketLength, '0');
                fullCommand += pack.Data;
                byte[] send = Encoding.UTF8.GetBytes(fullCommand);
                _socket.Send(send);
            }
            catch (Exception e)
            {

            }
            
        }
    }
}
