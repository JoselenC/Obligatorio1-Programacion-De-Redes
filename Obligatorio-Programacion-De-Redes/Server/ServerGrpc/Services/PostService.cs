using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Grpc.Core;
using GrpcServices;
using Microsoft.Extensions.Logging;

namespace ServerGRPC.ServerGrpc.Services
{
    public class PostService : PostGrpc.PostGrpcBase
    {
        private readonly IPostServiceGrpc _postService;
        private readonly IMapper _mapper;        
        
        public PostService(ILogger<PostService> logger, IPostServiceGrpc postService)
        {
            _postService = postService;
            var config = new MapperConfiguration(
                conf =>
                {
                    conf.CreateMap<PostMessage, Post>();
                    conf.CreateMap<Post, PostMessage>();
                });
            _mapper = config.CreateMapper();
        }

        public override async Task<AddPostsReply> AddPost(AddPostsRequest request, ServerCallContext context)
        {
            
            Post postRequest = _mapper.Map<Post>(request.Post);
            
            var postRepsonse = await _postService.AddPostAsyc(postRequest);
            return new AddPostsReply
            {
                Post = _mapper.Map<PostMessage>(postRepsonse)
            };
        }
        
        public override async Task<ModifyPostReply> ModifyPost(ModifyPostRequest request, ServerCallContext context)
        {
            var postRequest = _mapper.Map<Post>(request.Post);
            var postRepsonse = await _postService.ModifyPostAsyc(postRequest);
            return new ModifyPostReply
            {
                Post = _mapper.Map<PostMessage>(postRepsonse)
            };
        }
        public override async Task<DeletePostReply> DeletePost(DeletePostRequest request, ServerCallContext context)
        {
            var postRequest = _mapper.Map<Post>(request.Post);
            await _postService.DeletePostAsyc(postRequest);
            return new DeletePostReply { };
        }
    }
}