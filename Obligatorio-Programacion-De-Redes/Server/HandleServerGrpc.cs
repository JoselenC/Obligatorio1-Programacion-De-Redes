using Grpc.Net.Client;
using GrpcServices;

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
        
    }
}