using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Grpc.Net.Client;
using GrpcServices;

namespace ServicesGRPC
{
    public class PostServiceGrpc : IPostServiceGrpc
    {
        private PostGrpc.PostGrpcClient _client;
        private readonly IMapper _mapper;
        public PostServiceGrpc()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5001");
            _client = new PostGrpc.PostGrpcClient(channel);
            
            var config = new MapperConfiguration(
                conf =>
                {
                    conf.CreateMap<PostMessage, Post>();
                    conf.CreateMap<Post, PostMessage>();
                });
            _mapper = config.CreateMapper();
        }
        
        public async Task<Post> AddPostAsyc(Post post)
        {
            var postMessage = _mapper.Map<PostMessage>(post);
            var reply = await _client.AddPostAsync(
                new AddPostsRequest {Post = postMessage}
            );
            return _mapper.Map<Post>(reply);
        }
        
        public async Task<Post> ModifyPostAsyc(Post post)
        {
            var postMessage = _mapper.Map<PostMessage>(post);
            var reply = await _client.ModifyPostAsync(
                new ModifyPostRequest{Post = postMessage}
            );
            return _mapper.Map<Post>(reply);
        }
        
        public async Task<Post> DeletePostAsyc(Post post)
        {
            var postMessage = _mapper.Map<PostMessage>(post);
            var reply = await _client.DeletePostAsync(
                new DeletePostRequest {Post = postMessage}
            );
            return _mapper.Map<Post>(reply);
        }
        
    }
}