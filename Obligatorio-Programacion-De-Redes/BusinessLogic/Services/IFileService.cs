using System.Threading.Tasks;
using DataHandler;

namespace BusinessLogic.Services
{
    public interface IFileService
    {
        Task UploadFile(SocketHandler socketHandler);
    }
}