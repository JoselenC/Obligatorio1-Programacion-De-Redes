﻿using System.Threading.Tasks;
using DomainObjects;
using GrpcServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Fillter;

namespace WebAPI.Controllers
{
    [ApiController]
    [FilterExceptions]
    [Route("themes")]
   
    public class ThemeController:ControllerBase
    {
        private IThemeServiceGrpc _themeServiceGrpc;

        public ThemeController(IThemeServiceGrpc themeServiceGrpc)
        {
            _themeServiceGrpc = themeServiceGrpc;
        }

        [HttpPost]
        public async Task<IActionResult> AddTheme([FromBody] Theme theme)
        {
            var response= await _themeServiceGrpc.AddThemeAsync(theme);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult>  ModifyTheme([FromBody] Theme theme)
        {
            var response = await _themeServiceGrpc.ModifyThemeAsync(theme);
            return Ok(response);
        }

        [HttpDelete ("{name}")]
        public async Task<IActionResult>  DeleteTheme([FromQuery] string name)
        {
            var response = await _themeServiceGrpc.DeleteThemeAsync(new Theme(){Name = name});
            return Ok(response);
        }
    }
}