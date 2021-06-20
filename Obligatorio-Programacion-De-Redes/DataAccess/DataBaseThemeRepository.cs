using BusinessLogic.Managers;
using DataAccess.DtoOjects;
using DataAccess.Mappers;
using DomainObjects;

namespace DataAccess
{
    public class DataBaseThemeRepository:ManagerThemeRepository
    {
        public DataBaseThemeRepository()
        {
            Themes = new DataBaseRepository<Theme, ThemeDto>(new ThemeMapper());
        }
    }
    }
