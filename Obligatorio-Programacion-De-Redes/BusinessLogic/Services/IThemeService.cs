using System.Threading.Tasks;
using DataHandler;

namespace Domain.Services
{
    public interface IThemeService
    {
        Task AddThemeAsync(SocketHandler socketHandler);
        Task ModifyThemeAsync(SocketHandler socketHandler);
        Task DeleteThemeAsync(SocketHandler socketHandler);
    }
}