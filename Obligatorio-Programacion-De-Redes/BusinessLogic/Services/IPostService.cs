using System.Threading.Tasks;
using DataHandler;

namespace Domain.Services
{
    public interface IPostService
    {
        Task AddPostAsync(SocketHandler socketHandler);
        Task DeletePostAsync(SocketHandler socketHandler);
        Task ModifyPostAsync(SocketHandler socketHandler);
        Task AsociateThemeAsync(SocketHandler socketHandler);
        Task AsociateThemeToPostAsync(SocketHandler socketHandler);
        Task SearchPostAsync(SocketHandler socketHandler);
        Task DisassociateThemeAsync(SocketHandler socketHandler);
    }
}