using System.Threading.Tasks;
using DataHandler;

namespace Domain.Services
{
    public interface IFileService
    {
        Task UploadFile(SocketHandler socketHandler);
    }
}