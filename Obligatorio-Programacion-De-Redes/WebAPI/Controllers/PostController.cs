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
        public IActionResult AddPost([FromBody] Post post)
        {
            _postServiceGrpc.AddPostAsyc(post);
            return Ok(post);
        }
    }
}