using Domain;

namespace BusinessLogic.Managers
{
    public class ManagerThemeRepository
    {
        public IRepository<Theme> Themes { get; set; }
    }
}