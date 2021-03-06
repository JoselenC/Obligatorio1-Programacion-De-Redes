using System.Threading.Tasks;
using AutoMapper;
using DomainObjects;
using Grpc.Net.Client;
using GrpcServicesInterfaces;

namespace AdministrativeServer
{
    public class PostServiceGrpc : IPostServiceGrpc
    {
        private readonly PostGrpc.PostGrpcClient _client;
        private readonly IMapper _mapper;
        public PostServiceGrpc()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5002");
            _client = new PostGrpc.PostGrpcClient(channel);
            
            var config = new MapperConfiguration(
                conf =>
                {
                    conf.CreateMap<PostMessage, Post>();
                    conf.CreateMap<Post, PostMessage>();
                    conf.CreateMap<AddPostsReply, Post>();
                });
            _mapper = config.CreateMapper();
        }

        public async Task<Post> AddPostAsync(Post post)
        {
            post.Name ??= "";
            post.ThemeName ??= "";
            post.CreationDate ??= "";
            var postMessage = _mapper.Map<PostMessage>(post);
            AddPostsReply reply = await _client.AddPostAsync(
                new AddPostsRequest {Post = postMessage}
            );
            return _mapper.Map<Post>(reply.Post);
        }

        public async Task<Post> ModifyPostAsync(Post post)
        {
            post.Name ??= "";
            post.ThemeName ??= "";
            post.CreationDate ??= "";
                var postMessage = _mapper.Map<PostMessage>(post);
                ModifyPostReply reply = await _client.ModifyPostAsync(
                    new ModifyPostRequest {Post = postMessage}
                );
                return _mapper.Map<Post>(reply.Post);
        }

        public async Task DeletePostAsync(Post post)
        {
            post.Name ??= "";
            post.ThemeName ??= "";
            post.CreationDate ??= "";
            var postMessage = _mapper.Map<PostMessage>(post);
            DeletePostReply reply = await _client.DeletePostAsync(
                new DeletePostRequest {Post = postMessage}
            );
        }

    }
}