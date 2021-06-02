using System.Threading.Tasks;
using Domain;

namespace GrpcServices
{
    public interface IPostServiceGrpc
    {
        Task<Post> AddPostAsyc(Post post);
        Task<Post> ModifyPostAsyc(Post post);
        Task<Post> DeletePostAsyc(Post post);
    }
}