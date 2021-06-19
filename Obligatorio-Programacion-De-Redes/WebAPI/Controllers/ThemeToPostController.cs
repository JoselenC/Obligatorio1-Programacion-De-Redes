using System.Threading.Tasks;
using GrpcServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("posts")]
   
    public class ThemeToPostController:ControllerBase
    {
        private IThemeToPostServiceGrpc _themePostServiceGrpc;

        public ThemeToPostController(IThemeToPostServiceGrpc themePostServiceGrpc)
        {
            _themePostServiceGrpc = themePostServiceGrpc;
        }

        [HttpPut]
        public async Task<IActionResult>  ModifyPost(string namePost, string nameTheme)
        {
            var response = await _themePostServiceGrpc.AssociateThemeToPost(nameTheme,namePost);
            return Ok(response);
        }
    }
}