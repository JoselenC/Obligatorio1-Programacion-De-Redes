using BusinessLogic.Managers;
using DataAccess.DtoOjects;
using DataAccess.Mappers;
using Domain;

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
