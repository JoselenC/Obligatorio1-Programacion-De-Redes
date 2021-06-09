using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic;
using Domain;
using Grpc.Core;
using GrpcServices;
using Microsoft.Extensions.Logging;

namespace ServerGRPC.ServerGrpc.Services
{
    public class ThemeService : ThemeGrpc.ThemeGrpcBase
    {
        private readonly IThemeServiceGrpc _themeService;
        private readonly IMapper _mapper;
        private ManagerRepository _repository;

        public ThemeService(ILogger<ThemeService> logger, IThemeServiceGrpc themeService,
            ManagerRepository repository)
        {
            _repository = repository;
            _themeService = themeService;
            var config = new MapperConfiguration(
                conf =>
                {
                    conf.CreateMap<ThemeMessage, Theme>();
                    conf.CreateMap<Theme, ThemeMessage>();
                });
            _mapper = config.CreateMapper();
        }

        public override async Task<AddThemesReply> AddTheme(AddThemesRequest request, ServerCallContext context)
        {
            var themeRequest = _mapper.Map<Theme>(request.Theme);
            var themeRepsonse = _repository.Themes.Add(themeRequest);
            return new AddThemesReply
            {
                Theme = _mapper.Map<ThemeMessage>(themeRepsonse)
            };
        }
        
        public override async Task<ModifyThemeReply> ModifyTheme(ModifyThemeRequest request, ServerCallContext context)
        {
            var themeRequest = _mapper.Map<Theme>(request.Theme);
            var theme =  _repository.Themes.Find(x=>x.Name==themeRequest.Name);
            var themeRepsonse = _repository.Themes.Update(theme,themeRequest);
            return new ModifyThemeReply
            {
                Theme = _mapper.Map<ThemeMessage>(themeRepsonse)
            };
        }
        public override async Task<DeleteThemeReply> DeleteTheme(DeleteThemeRequest request, ServerCallContext context)
        {
            var themeRequest = _mapper.Map<Theme>(request.Theme);
             _repository.Themes.Delete(themeRequest);
            return new DeleteThemeReply{ };
        }
    }
}