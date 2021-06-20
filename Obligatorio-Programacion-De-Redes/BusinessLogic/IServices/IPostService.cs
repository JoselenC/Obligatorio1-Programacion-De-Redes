using System.Threading.Tasks;
using Protocol;

namespace BusinessLogic.IServices
{
    public interface IPostService
    {
        Task AddPostAsync(SocketHandler socketHandler);
        Task DeletePostAsync(SocketHandler socketHandler);
        Task ModifyPostAsync(SocketHandler socketHandler);
        Task AssociateThemeAsync(SocketHandler socketHandler);
        Task AssociateThemeToPostAsync(SocketHandler socketHandler);
        Task SearchPostAsync(SocketHandler socketHandler);
        Task DisassociateThemeAsync(SocketHandler socketHandler);
    }
}