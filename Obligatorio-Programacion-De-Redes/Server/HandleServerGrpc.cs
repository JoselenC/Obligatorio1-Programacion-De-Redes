using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServices;
using ServicesGRPC;

namespace Server
{
    public class HandleServerGrpc
    {
        private IPostServiceGrpc _postServiceGrpc;
        private IThemeServiceGrpc _themeServiceGrpc;
        
        public HandleServerGrpc(GrpcChannel channel)
        {
            _postServiceGrpc = new PostServiceGrpc(channel);
            _themeServiceGrpc = new ThemeServiceGrpc(channel);
        }

        private async Task AddPost()
        {
            User user = await _postServiceGrpc.AddPostAsyc();
        }
    }
}