using System.Threading.Tasks;
using DomainObjects;

namespace GrpcServicesInterfaces
{
    public interface IThemeServiceGrpc
    {
        Task<Theme> AddThemeAsync(Theme theme);
        Task<Theme> ModifyThemeAsync(Theme theme);
        Task<Theme> DeleteThemeAsync(Theme theme);
    }
}