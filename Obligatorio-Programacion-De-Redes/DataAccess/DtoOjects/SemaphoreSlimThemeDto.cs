using System.Threading;
using Domain;

namespace DataAccess.DtoOjects
{
    public class SemaphoreSlimThemeDto
    {
        public int Id { get; set; }
        public ThemeDto Theme { get; set; }
    }
}