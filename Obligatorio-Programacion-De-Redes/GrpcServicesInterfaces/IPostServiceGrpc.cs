using System.Threading.Tasks;
using DomainObjects;

namespace GrpcServicesInterfaces
{
    public interface IPostServiceGrpc
    {
        Task<Post> AddPostAsync(Post post);
        Task<Post> ModifyPostAsync(Post post);
        Task<Post> DeletePostAsync(Post post);
    }
}