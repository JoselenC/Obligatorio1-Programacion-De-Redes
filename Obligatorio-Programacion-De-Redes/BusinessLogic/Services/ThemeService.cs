using System;
using System.Threading.Tasks;
using BusinessLogic.IServices;
using BusinessLogic.Managers;
using DomainObjects;
using Protocol;

namespace BusinessLogic.Services
{
    public class ThemeService : IThemeService
    {
        private readonly ManagerThemeRepository _themeRepository;
        private readonly ManagerPostRepository _postRepository;
        private readonly RabbitHelper _rabbitClient;
        private readonly ThemeHelper _themeHelper;
        public ThemeService(RabbitHelper rabbitHelper,ManagerPostRepository managerPostRepository, 
        ManagerThemeRepository managerThemeRepository)
        {
            _themeHelper = new ThemeHelper();
            _postRepository = managerPostRepository;
            _themeRepository = managerThemeRepository;
            _rabbitClient = rabbitHelper;
        }
    
        public async Task AddThemeAsync(SocketHandler socketHandler)
        {
            var packet = await socketHandler.ReceivePackageAsync();
            String[] messageArray = packet.Data.Split('#');
            string name = messageArray[0];
            _rabbitClient.SendMessage("New theme" + name);
            if (name != "Back")
            {
                string description = messageArray[1];
                string message;

                if (name != "")
                {
                    if (!_themeHelper.AlreadyExistTheme(name, _themeRepository))
                    {
                        Theme theme = new Theme() { Name = name, Description = description };
                        _themeRepository.Themes.Add(theme);
                        message = "The theme " + name + " was added";
                        _rabbitClient.SendMessage(message);
                    }
                    else
                    {
                        message = "Not add, the theme " + name + " already exist";
                        _rabbitClient.SendMessage(message);
                    }
                }
                else
                {
                    message = "The theme name cannot be empty";
                    _rabbitClient.SendMessage(message);
                }
                Packet package = new Packet("RES", "4", message);
                await socketHandler.SendPackageAsync(package);
            }
        }
        
        public async Task ModifyThemeAsync(SocketHandler socketHandler)
        {
            await _themeHelper.SendThemesAsync(socketHandler, _themeRepository);
            var packet = await socketHandler.ReceivePackageAsync();
            string[] messageArray = packet.Data.Split('#');
            string option = messageArray[0];
            if (option != "Back")
            {
                var message = await _themeHelper.ModifyTheme(socketHandler, option, _themeRepository, _rabbitClient);
                Packet package2 = new Packet("RES", "4", message);
                await socketHandler.SendPackageAsync(package2);
            }
        }
        
        public async Task DeleteThemeAsync(SocketHandler socketHandler)
        {
            await _themeHelper.SendThemesAsync(socketHandler, _themeRepository);
            var packet = await socketHandler.ReceivePackageAsync();
            string oldName = packet.Data;
            if (oldName != "Back")
            {
                var message = _themeHelper.DeleteTheme(oldName, _themeRepository, _rabbitClient, _postRepository);
                Packet package3 = new Packet("RES", "4", message);
                await socketHandler.SendPackageAsync(package3);
            }
        }

       
    }
}