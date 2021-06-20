using System.Threading.Tasks;
using Protocol;

namespace BusinessLogic.IServices
{
    public interface IThemeService
    {
        Task AddThemeAsync(SocketHandler socketHandler);
        Task ModifyThemeAsync(SocketHandler socketHandler);
        Task DeleteThemeAsync(SocketHandler socketHandler);
    }
}