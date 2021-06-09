using System.Threading.Tasks;
using DataHandler;

namespace BusinessLogic.Services
{
    public interface IThemeService
    {
        Task AddThemeAsync(SocketHandler socketHandler);
        Task ModifyThemeAsync(SocketHandler socketHandler);
        Task DeleteThemeAsync(SocketHandler socketHandler);
    }
}