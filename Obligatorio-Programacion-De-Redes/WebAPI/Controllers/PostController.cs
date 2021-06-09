using System.Threading.Tasks;
using Domain;
using GrpcServices;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("posts")]
   
    public class PostController:ControllerBase
    {
        private IPostServiceGrpc _postServiceGrpc;

        PostController(IPostServiceGrpc postServiceGrpc)
        {
            _postServiceGrpc = postServiceGrpc;
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] Post post)
        {
            var response= await _postServiceGrpc.AddPostAsyc(post);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult>  ModifyPost([FromBody] Post post, [FromQuery] string oldName)
        {
            var response = await _postServiceGrpc.ModifyPostAsyc(post);
            return Ok(response);
        }

        [HttpDelete ("{postName}")]
        public async Task<IActionResult>  DeletePost([FromQuery] string postName)
        {
            var response = await _postServiceGrpc.DeletePostAsyc(new Post(){Name = postName});
            return Ok(response);
        }
    }
}