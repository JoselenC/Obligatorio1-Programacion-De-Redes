using System.Threading.Tasks;
using Domain;
using GrpcServices;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("themes")]
   
    public class ThemeController:ControllerBase
    {
        private IThemeServiceGrpc _themeServiceGrpc;

        ThemeController(IThemeServiceGrpc themeServiceGrpc)
        {
            _themeServiceGrpc = themeServiceGrpc;
        }

        [HttpPost]
        public async Task<IActionResult> AddTheme([FromBody] Theme theme)
        {
            var response= await _themeServiceGrpc.AddThemeAsyc(theme);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult>  ModifyTheme([FromBody] Theme theme, [FromQuery] string oldName)
        {
            var response = await _themeServiceGrpc.ModifyThemeAsyc(theme);
            return Ok(response);
        }

        [HttpDelete ("{themeName}")]
        public async Task<IActionResult>  DeleteTheme([FromQuery] string themeName)
        {
            var response = await _themeServiceGrpc.DeleteThemeAsyc(new Theme(){Name = themeName});
            return Ok(response);
        }
    }
}