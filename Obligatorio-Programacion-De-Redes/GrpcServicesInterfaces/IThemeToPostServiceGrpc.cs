using System.Threading.Tasks;

namespace GrpcServicesInterfaces
{
    public interface IThemeToPostServiceGrpc
    {
        public Task<string> AssociateThemeToPost(string nameTheme, string namePost);
    }
}