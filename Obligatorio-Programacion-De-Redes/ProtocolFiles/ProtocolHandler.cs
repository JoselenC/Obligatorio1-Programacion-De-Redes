using DataHandler;
using Protocol;
using System;
using System.Net.Sockets;
using System.Text;

namespace ProtocolFiles
{
    public class ProtocolHandler
    {
        public long GetFileParts(long filesize)
        {
            var parts = filesize / ProtocolConstant.MaxPacketSize;
            return parts * ProtocolConstant.MaxPacketSize == filesize ? parts : parts + 1;
        }
        
        public string[] ReceiveFile(Socket client,SocketHandler socketHandler)
        {
            var fileStreamHandler = new FileStreamHandler();

            using (var networkStream = new NetworkStream(client))
            {
                var header = Read(ProtocolHelper.GetLength(), networkStream);
                var fileNameSize = BitConverter.ToInt32(header, 0);
                var fileSize = BitConverter.ToInt32(header, ProtocolConstant.FileNameLength);
               
                var fileName = Encoding.UTF8.GetString(Read(fileNameSize, networkStream));
                
                var parts = GetFileParts(fileSize);
                var offset = 0;
                var currentPart = 1;

                var rawFileInMemory = new byte[fileSize];

                while (fileSize > offset)
                {
                    if (currentPart == parts)
                    {
                        var lastPartSize = fileSize - offset;
                        var data = Read(lastPartSize, networkStream);
                        Array.Copy(data, 0, rawFileInMemory, offset, lastPartSize);
                        offset += lastPartSize;
                    }
                    else
                    {
                        var data = Read(ProtocolConstant.MaxPacketSize, networkStream);
                        Array.Copy(data, 0, rawFileInMemory, offset, ProtocolConstant.MaxPacketSize);
                        offset += ProtocolConstant.MaxPacketSize;
                    }

                    currentPart++;
                }

                fileStreamHandler.WriteFile(fileName, rawFileInMemory);
            }

            var packet = socketHandler.ReceivePackg();
            return packet.Data.Split('#');
        }

        public byte[] Read(int length, NetworkStream stream)
        {
            int dataReceived = 0;
            var data = new byte[length];
            while (dataReceived < length)
            {
                var received = stream.Read(data, dataReceived, length - dataReceived);
                if (received == 0)
                {
                    throw new Exception("The connection was lost");
                }

                dataReceived += received;
            }
            return data;    
        }
        
        public void SendFile(string path,Socket connectedClient,SocketHandler socketHandler,string postName)
        {
            if (path != "")
            {
                var fileHandler = new FileHandler();
                var fileStreamHandler = new FileStreamHandler();
                var fileSize = fileHandler.GetFileSize(path);
                var fileName = fileHandler.GetFileName(path);
                var header = ProtocolHelper.CreateHeader(fileName, fileSize);

                using (var connectionStream = new NetworkStream(connectedClient))
                {
                    Console.WriteLine($"FileName is: {fileName}, file size is: {fileSize}");

                    connectionStream.Write(header);
                    connectionStream.Write(Encoding.UTF8.GetBytes(fileName));

                    var rawFile = fileStreamHandler.ReadFile(path);
                    var parts = GetFileParts(fileSize);

                    long offset = 0;
                    long currentPart = 1;

                    while (fileSize > offset)
                    {
                        Console.WriteLine($"Sending part {currentPart} of {parts}");
                        if (currentPart == parts)
                        {
                            var lastPartSize = fileSize - offset;
                            var dataToSend = new byte[lastPartSize];
                            Array.Copy(rawFile, offset, dataToSend, 0, lastPartSize);
                            offset += lastPartSize;
                            connectionStream.Write(dataToSend);
                        }
                        else
                        {
                            var dataToSend = new byte[ProtocolConstant.MaxPacketSize];
                            Array.Copy(rawFile, offset, dataToSend, 0, ProtocolConstant.MaxPacketSize);
                            offset += ProtocolConstant.MaxPacketSize;
                            connectionStream.Write(dataToSend);
                        }

                        currentPart++;
                    }
                }

                string message = postName + "#" + fileSize + "#" + fileName;
                Packet packg = new Packet("REQ", "4", message);
                socketHandler.SendPackg(packg);
            }
            else
            {
                string message = "Not associated";
                Packet packg = new Packet("REQ", "4", message);
                socketHandler.SendPackg(packg);
            }
        }

    }
}