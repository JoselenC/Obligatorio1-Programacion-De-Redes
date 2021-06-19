using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Managers;
using DataAccess;
using Domain;
using Grpc.Core;

namespace Server.ServerGrpc.Services
{
    public class ThemeService : ThemeGrpc.ThemeGrpcBase
    {
        private readonly IMapper _mapper;
        private ManagerThemeRepository _themeRepository;

        public ThemeService()
        {
            _themeRepository = new DataBaseThemeRepository();
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
            var themeRepsonse = _themeRepository.Themes.Add(themeRequest);
            return new AddThemesReply
            {
                Theme = _mapper.Map<ThemeMessage>(themeRepsonse)
            };
        }
        
        public override async Task<ModifyThemeReply> ModifyTheme(ModifyThemeRequest request, ServerCallContext context)
        {
            var themeRequest = _mapper.Map<Theme>(request.Theme);
            var theme =  _themeRepository.Themes.Find(x=>x.Name==themeRequest.Name);
            var themeRepsonse = _themeRepository.Themes.Update(theme,themeRequest);
            return new ModifyThemeReply
            {
                Theme = _mapper.Map<ThemeMessage>(themeRepsonse)
            };
        }
        public override async Task<DeleteThemeReply> DeleteTheme(DeleteThemeRequest request, ServerCallContext context)
        {
            var themeRequest = _mapper.Map<Theme>(request.Theme);
            _themeRepository.Themes.Delete(themeRequest);
            return new DeleteThemeReply{ };
        }
    }
}