
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Grpc.Net.Client;
using GrpcServices;

namespace ServicesGRPC
{
    public class ThemeServiceGrpc : IThemeServiceGrpc
    {
        private readonly ThemeGrpc.ThemeGrpcClient _client;
        private readonly IMapper _mapper;
        public ThemeServiceGrpc(GrpcChannel channel)
        {
            _client = new ThemeGrpc.ThemeGrpcClient(channel);
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<ThemeMessage, Theme>();
                });
            _mapper = config.CreateMapper();
        }
        
        public async Task<Theme> AddThemeAsyc(Theme theme)
        {
            var themeMessage = _mapper.Map<ThemeMessage>(theme);
            var reply = await _client.AddThemeAsync(
                new AddThemesRequest {Theme = themeMessage}
            );
            return _mapper.Map<Theme>(reply);
        }
        
        public async Task<Theme> ModifyThemeAsyc(Theme theme)
        {
            var themeMessage = _mapper.Map<ThemeMessage>(theme);
            var reply = await _client.ModifyThemeAsync(
                new ModifyThemeRequest{Theme = themeMessage}
            );
            return _mapper.Map<Theme>(reply);
        }
        
        public async Task<Theme> DeleteThemeAsyc(Theme theme)
        {
            var themeMessage = _mapper.Map<ThemeMessage>(theme);
            var reply = await _client.DeleteThemeAsync(
                new DeleteThemeRequest {Theme = themeMessage}
            );
            return _mapper.Map<Theme>(reply);
        }
        
    }
}
