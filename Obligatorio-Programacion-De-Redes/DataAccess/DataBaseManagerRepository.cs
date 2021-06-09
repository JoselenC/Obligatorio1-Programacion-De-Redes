using BusinessLogic;
using DataAccess.DtoOjects;
using DataAccess.Mappers;
using Domain;

namespace DataAccess
{
    public class DataBaseManagerRepository:ManagerRepository
    {
        public DataBaseManagerRepository()
        {
            Posts = new DataBaseRepository<Post, PostDto>(new PostMapper());
            Themes = new DataBaseRepository<Theme, ThemeDto>(new ThemeMapper());
            Files = new DataBaseRepository<File, FileDto>(new FileMapper());
            Clients = new DataBaseRepository<Client, ClientDto>(new ClientMapper());
            SemaphoreSlimPosts = new DataBaseRepository<SemaphoreSlimPost, SemaphoreSlimPostDto>(new SemaphorePostMapper());
            SemaphoreSlimThemes = new DataBaseRepository<SemaphoreSlimTheme, SemaphoreSlimThemeDto>(new SemaphoreThemeMapper());
        }
    }
}