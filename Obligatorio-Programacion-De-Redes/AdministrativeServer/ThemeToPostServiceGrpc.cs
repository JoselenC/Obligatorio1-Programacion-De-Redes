using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Services;
using Domain;
using Grpc.Net.Client;
using GrpcServicesInterfaces;

namespace AdministrativeServer
{
    public class ThemeToPostServiceGrpc:IThemeToPostServiceGrpc
    {
        private readonly ThemeToPostGrpc.ThemeToPostGrpcClient _client;
        private readonly IMapper _mapper;
        public ThemeToPostServiceGrpc()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5002");
            _client = new ThemeToPostGrpc.ThemeToPostGrpcClient(channel);
            
            var config = new MapperConfiguration(
                cfg =>
                {
                });
            _mapper = config.CreateMapper();
        }
        
        public async Task<string> AssociateThemeToPost(string nameTheme,string namePost)
        {
            AssociateThemeToPostReply reply = await _client.AssociateThemeToPostAsync(
                new AssociateThemeToPostRequest {PostName = namePost, ThemeName = nameTheme}
                    );
            return _mapper.Map<string>(reply);
        }

    }
}