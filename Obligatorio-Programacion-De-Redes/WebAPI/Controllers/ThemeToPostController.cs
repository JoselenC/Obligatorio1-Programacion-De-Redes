using System.Threading.Tasks;
using GrpcServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Fillter;

namespace WebAPI.Controllers
{
    [ApiController]
    [FilterExceptions]
    [Route("post/theme")]
   
    public class ThemeToPostController:ControllerBase
    {
        private IThemeToPostServiceGrpc _themePostServiceGrpc;

        public ThemeToPostController(IThemeToPostServiceGrpc themePostServiceGrpc)
        {
            _themePostServiceGrpc = themePostServiceGrpc;
        }

        [HttpPost]
        public async Task<IActionResult>  AssociateThemeToPost([FromBody] ThemePostDto themePostDto)
        {
            var response = await _themePostServiceGrpc.AssociateThemeToPost(themePostDto.ThemeName,themePostDto.PostName);
            return Ok(response);
        }
        
        [HttpPut]
        public async Task<IActionResult>  DissasociateThemeToPost([FromBody] ThemePostDto themePostDto)
        {
            await _themePostServiceGrpc.DisasociateThemeToPost(themePostDto.ThemeName,themePostDto.PostName);
            return Ok("Dissasociate theme " + themePostDto.ThemeName + "to post" + themePostDto.PostName);
        }
    }
}