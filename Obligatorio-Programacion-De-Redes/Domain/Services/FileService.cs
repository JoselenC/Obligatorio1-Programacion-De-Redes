using BusinessLogic;
using DataHandler;

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
          
        }
    }
}