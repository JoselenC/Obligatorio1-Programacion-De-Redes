using DataHandler;
using Protocol;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolFiles
{
    public class ProtocolHandler
    {
        public long GetFileParts(long filesize)
        {
            var parts = filesize / ProtocolConstant.MaxPacketSize;
            return parts * ProtocolConstant.MaxPacketSize == filesize ? parts : parts + 1;
        }

        public async Task<string[]> ReceiveFileAsync(SocketHandler socketHandler)
        {
            var fileStreamHandler = new FileStreamHandler();
            NetworkStreamHandler networkStreamHandler = new NetworkStreamHandler(socketHandler.networkStream);
            var header = await networkStreamHandler.ReadAsync(new ProtocolHelper().GetLength());
            var fileNameSize = BitConverter.ToInt32(header, 0);
            var fileSize = BitConverter.ToInt64(header, ProtocolConstant.FileNameLength);

            var fileName = Encoding.UTF8.GetString(await networkStreamHandler.ReadAsync(fileNameSize));
            var parts = GetFileParts(fileSize);
            var offset = 0;
            var currentPart = 1;
            while (fileSize > offset)
            {
                byte[] data;
                if (currentPart == parts)
                {
                    var lastPartSize = (int) (fileSize - offset);
                    data = await networkStreamHandler.ReadAsync(lastPartSize);
                    offset += lastPartSize;
                }
                else
                {
                    data = await networkStreamHandler.ReadAsync(ProtocolConstant.MaxPacketSize);
                    offset += ProtocolConstant.MaxPacketSize;
                }

                await fileStreamHandler.WriteAsync(fileName, data);
                currentPart++;
            }
            var packet = await socketHandler.ReceivePackgAsync();
            return packet.Data.Split('#');
        }

        
        public async Task SendFileAsync(string path,SocketHandler socketHandler,string postName)
        {
            if (path != "")
            {
                var fileHandler = new FileHandler();
                var fileStreamHandler = new FileStreamHandler();
                var fileSize = fileHandler.GetFileSize(path);
                var fileName = fileHandler.GetFileName(path);
                var header = new ProtocolHelper().CreateHeader(fileName, fileSize);

                NetworkStreamHandler networkStreamHandler = new NetworkStreamHandler(socketHandler.networkStream);
                await networkStreamHandler.WriteAsync(header);
                var fileNameByte = Encoding.UTF8.GetBytes(fileName);
                await networkStreamHandler.WriteAsync(fileNameByte);
                long parts = GetFileParts(fileSize);
                Console.WriteLine("Will Send {0} parts", parts);
                long offset = 0;
                long currentPart = 1;

                while (fileSize > offset)
                {
                    byte[] data;
                    if (currentPart == parts)
                    {
                        var lastPartSize = (int) (fileSize - offset);
                        data = await fileStreamHandler.ReadAsync(path, offset, lastPartSize);
                        offset += lastPartSize;
                    }
                    else
                    {
                        data = await fileStreamHandler.ReadAsync(path, offset, ProtocolConstant.MaxPacketSize);
                        offset += ProtocolConstant.MaxPacketSize;
                    }

                    await networkStreamHandler.WriteAsync(data);
                    currentPart++;
                }

                string message = postName + "#" + fileSize + "#" + fileName;
                Packet packg = new Packet("REQ", "4", message);
                await socketHandler.SendPackgAsync(packg);
            }
            else
            {
                string message = "Not associated";
                Packet packg = new Packet("REQ", "4", message);
                await socketHandler.SendPackgAsync(packg);
            }
        }

    }
}