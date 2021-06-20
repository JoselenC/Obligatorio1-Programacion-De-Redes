using System.Threading.Tasks;
using Protocol;

namespace BusinessLogic.IServices
{
    public interface IFileService
    {
        Task UploadFile(SocketHandler socketHandler);
    }
}