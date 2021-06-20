using System.Threading.Tasks;
using BusinessLogic.Managers;

namespace Server.Server.Pages
{
    public class HomePageServer
    {
        public async Task MenuAsync(ManagerRepository repository, bool exit, 
            ManagerPostRepository postRepository,ManagerThemeRepository themeRepository)
        {
            string[] _options = {"Client list", "Posts", "Themes", "File", "Exit"};
            while (!exit)
            {
                int option = new MenuServer().ShowMenu(_options,"Menu");
                switch (option)
                {
                    case 1:
                        new ClientPageServer().ShowClientList(repository, postRepository,themeRepository);
                        break;
                    case 2:
                        new PostPageServer().Menu(repository, postRepository,themeRepository);
                        break;
                    case 3:
                        new ThemePageServer().Menu(repository,themeRepository,postRepository);
                        break;
                    case 4:
                        new FilePageServer().ShowFileList(repository,postRepository,themeRepository);
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}