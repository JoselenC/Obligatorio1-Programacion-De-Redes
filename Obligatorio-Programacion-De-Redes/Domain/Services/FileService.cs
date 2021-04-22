using System.Net.Sockets;
using BusinessLogic;
using DataHandler;
using ProtocolFiles;

namespace Domain.Services
{
    public class FileService
    {
        private MemoryRepository repository;
        public FileService(MemoryRepository repository)
        {
            this.repository = repository;
        }
        public void UploadFile(SocketHandler socketHandler)
        {
            ProtocolHandler protocolHandler = new ProtocolHandler();
            protocolHandler.ReceiveFile(new TcpClient());
        }
    }
}