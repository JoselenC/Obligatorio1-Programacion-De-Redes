using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Managers;
using DataAccess;
using Domain;
using Grpc.Core;

namespace Server.ServerGrpc.Services
{
    public class ThemeToPostService : ThemeToPostGrpc.ThemeToPostGrpcBase
    {
        private readonly IMapper _mapper;
        private ManagerThemeRepository _themeRepository;
        private ManagerPostRepository _postRepository;
        
        public ThemeToPostService()
        {
            _postRepository = new DataBasePostRepository();
            _themeRepository = new ManagerThemeRepository();
            var config = new MapperConfiguration(
                conf =>
                {
                    conf.CreateMap<ThemeMessage, Theme>();
                    conf.CreateMap<Theme, ThemeMessage>();
                });
            _mapper = config.CreateMapper();
        }

     
    }
}