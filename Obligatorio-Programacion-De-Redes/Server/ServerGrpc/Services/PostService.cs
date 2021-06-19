using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Managers;
using BusinessLogic.Services;
using DataAccess;
using Domain;
using Grpc.Core;

namespace Server.ServerGrpc.Services
{
    public class PostService : PostGrpc.PostGrpcBase
    {
        private IPostService postService;
        private readonly IMapper _mapper;
        private ManagerPostRepository _postRepository;
        public PostService()
        {
            _postRepository = new DataBasePostRepository();
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
            
            Post post = _mapper.Map<Post>(request.Post);
         
            var postRepsonse =  _postRepository.Posts.Add(post);
            return new AddPostsReply
            {
                Post = _mapper.Map<PostMessage>(postRepsonse)
            };
        }
        
        public override async Task<ModifyPostReply> ModifyPost(ModifyPostRequest request, ServerCallContext context)
        {
            var postRequest = _mapper.Map<Post>(request.Post);
            var post =  _postRepository.Posts.Find(x=>x.Name==postRequest.Name);
            var postRepsonse = _postRepository.Posts.Update(post,postRequest);
            return new ModifyPostReply
            {
                Post = _mapper.Map<PostMessage>(postRepsonse)
            };
        }
        public override async Task<DeletePostReply> DeletePost(DeletePostRequest request, ServerCallContext context)
        {
            var postRequest = _mapper.Map<Post>(request.Post);
            _postRepository.Posts.Delete(postRequest);
            return new DeletePostReply { };
        }
    }
}