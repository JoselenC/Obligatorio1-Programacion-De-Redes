using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServicesInterfaces;

namespace AdministrativeServer
{
    public class ThemeToPostServiceGrpc:IThemeToPostServiceGrpc
    {
        private readonly ThemeToPostGrpc.ThemeToPostGrpcClient _client;
        public ThemeToPostServiceGrpc()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5002");
            _client = new ThemeToPostGrpc.ThemeToPostGrpcClient(channel);
        }

        public async Task<string> AssociateThemeToPost(string nameTheme, string namePost)
        {
            nameTheme ??= "";
            namePost ??= "";
            AssociateThemeToPostReply reply = await _client.AssociateThemeToPostAsync(
                new AssociateThemeToPostRequest
                    {ThemeToPost = new ThemeToPost() {PostName = namePost, ThemeName = nameTheme}}
            );
            return "Associate theme " + reply.ThemeToPost.ThemeName + " to post " + reply.ThemeToPost.PostName;
        }

        public async Task DisasociateThemeToPost(string nameTheme, string namePost)
        {
            await _client.DissasociateThemeToPostAsync(
                new DissasociateThemeToPostRequest
                    {ThemeToPost = new ThemeToPost() {PostName = namePost, ThemeName = nameTheme}}
            );
        }
    }
}