using System.Threading.Tasks;
using DomainObjects;
using GrpcServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Fillter;

namespace WebAPI.Controllers
{
    [ApiController]
    [FilterExceptions]
    [Route("posts")]
   
    public class PostController:ControllerBase
    {
        private IPostServiceGrpc _postServiceGrpc;

        public PostController(IPostServiceGrpc postServiceGrpc)
        {
            _postServiceGrpc = postServiceGrpc;
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] Post post)
        {
            var response= await _postServiceGrpc.AddPostAsync(post);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult>  ModifyPost([FromBody] Post post)
        {
            var response = await _postServiceGrpc.ModifyPostAsync(post);
            return Ok(response);
        }

        [HttpDelete ("{name}")]
        public async Task<IActionResult>  DeletePost([FromQuery] string name)
        {
            await _postServiceGrpc.DeletePostAsync(new Post(){Name = name,CreationDate = "",ThemeName = ""});
            return Ok("The post was deleted");
        }
    }
}