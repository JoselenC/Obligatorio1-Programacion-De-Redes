using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.Managers;
using DataAccess;
using DomainObjects;
using DomainObjects.Exceptions;
using Grpc.Core;
using Server.Exceptions;

namespace Server.ServerGrpc.Services
{
    public class ThemeService : ThemeGrpc.ThemeGrpcBase
    {
        private readonly IMapper _mapper;
        private ManagerThemeRepository _themeRepository;
        private RabbitHelper _rabbitHelper;
        public ThemeService()
        {
            _rabbitHelper = new RabbitHelper();
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
            try
            {
                var themeRequest = _mapper.Map<Theme>(request.Theme);
                themeRequest.ValidateName(themeRequest.Name);
                if (!AlreadyExistThisTheme(themeRequest))
                {
                    var themeRepsonse = _themeRepository.Themes.Add(themeRequest);
                    _rabbitHelper.SendMessage("Theme "+request.Theme.Name + " was added" +"#"+"theme"+ "#" + request.Theme.Name);
                    return new AddThemesReply
                    {
                        Theme = _mapper.Map<ThemeMessage>(themeRepsonse)
                    };
                }
                _rabbitHelper.SendMessage("Theme "+request.Theme.Name + " wasn't added, already exist"+"#"+"theme"+ "#" + request.Theme.Name);
                throw new RpcException(new Status(StatusCode.AlreadyExists, "Post Already exist"));
            }
            catch (InvalidNameLength)
            {
                _rabbitHelper.SendMessage("Theme "+request.Theme.Name + " wasn't added, empty name"+"#"+"theme"+ "#" + request.Theme.Name);
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Post name empty"));
            }
        }

        public bool AlreadyExistThisTheme(Theme theme)
        {
            try
            {
                _themeRepository.Themes.Find(x=>x.Name==theme.Name);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public override async Task<ModifyThemeReply> ModifyTheme(ModifyThemeRequest request, ServerCallContext context)
        {
            try
            {
                var themeRequest = _mapper.Map<Theme>(request.Theme);
                var theme = _themeRepository.Themes.Find(x => x.Name == themeRequest.Name);
                var themeRepsonse = _themeRepository.Themes.Update(theme, themeRequest);
                _rabbitHelper.SendMessage("Theme "+request.Theme.Name + " was modified"+"#"+"theme"+ "#" + request.Theme.Name);
                return new ModifyThemeReply
                {
                    Theme = _mapper.Map<ThemeMessage>(themeRepsonse)
                };
            }
            catch (KeyNotFoundException)
            {
                _rabbitHelper.SendMessage("Theme "+request.Theme.Name + " wasn't modified, not exist"+"#"+"theme"+ "#" + request.Theme.Name);
                throw new RpcException(new Status(StatusCode.NotFound, "Not found"));
            }
        }

        public override async Task<DeleteThemeReply> DeleteTheme(DeleteThemeRequest request, ServerCallContext context)
        {
            try
            {
                var theme = _themeRepository.Themes.Find(x => x.Name == request.Theme.Name);
                _themeRepository.Themes.Delete(theme);
                _rabbitHelper.SendMessage("Theme "+request.Theme.Name + " was deleted"+"#"+"theme"+ "#" + request.Theme.Name);
                return new DeleteThemeReply { };
            }
            catch (KeyNotFoundException)
            {
                _rabbitHelper.SendMessage("Theme "+request.Theme.Name + " wasn't deleted, not exist"+"#"+"theme"+ "#" + request.Theme.Name);
                throw new RpcException(new Status(StatusCode.NotFound, "Not found"));
            }
        }
    }
}