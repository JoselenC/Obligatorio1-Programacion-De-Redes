using System.Threading.Tasks;
using Domain;

namespace GrpcServices
{
    public interface IThemeServiceGrpc
    {
        Task<Theme> AddThemeAsyc(Theme theme);
        Task<Theme> ModifyThemeAsyc(Theme theme);
        Task<Theme> DeleteThemeAsyc(Theme theme);
    }
}