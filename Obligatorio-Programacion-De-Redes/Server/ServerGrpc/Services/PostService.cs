using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.IServices;
using BusinessLogic.Managers;
using BusinessLogic.Services;
using DataAccess;
using DomainObjects;
using DomainObjects.Exceptions;
using Grpc.Core;
using Server.Exceptions;

namespace Server.ServerGrpc.Services
{
    public class PostService : PostGrpc.PostGrpcBase
    {
        private readonly RabbitHelper _rabbitHelper;
        private readonly IMapper _mapper;
        private readonly ManagerPostRepository _postRepository;
        private readonly ManagerThemeRepository _themeRepository;
        public PostService()
        {
            _rabbitHelper = new RabbitHelper();
            _themeRepository = new DataBaseThemeRepository();
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
            try
            {
                Post post = _mapper.Map<Post>(request.Post);
                post.SetName(post.Name);
                Theme theme = _themeRepository.Themes.Find(c => c.Name == request.Post.ThemeName);
                theme.SetName(theme.Name);
                if (!AlreadyExistPost(post))
                {
                    post.Themes.Add(theme);
                    var postRepsonse = _postRepository.Posts.Add(post);
                    _rabbitHelper.SendMessage("Post "+request.Post.Name + " was added");
                    return new AddPostsReply
                    {
                        Post = _mapper.Map<PostMessage>(postRepsonse)
                    };
                }
                _rabbitHelper.SendMessage("Post "+request.Post.Name + " wasn't added, already exist");
                throw new RpcException(new Status(StatusCode.AlreadyExists, "Post Already exist"));
            }
            catch (InvalidNameLength)
            {
                _rabbitHelper.SendMessage("Post "+request.Post.Name + " wasn't added, invalid empty name");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Name cannot be empty"));
            }
            catch (KeyNotFoundException)
            {
                _rabbitHelper.SendMessage("Post "+request.Post.Name + " not exist");
                throw new RpcException(new Status(StatusCode.NotFound, "Post not found"));
            }
            
        }

        public bool AlreadyExistPost(Post post)
        {
            try
            {
                _postRepository.Posts.Find(x=>x.Name==post.Name);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public override async Task<ModifyPostReply> ModifyPost(ModifyPostRequest request, ServerCallContext context)
        {
            try
            {
                var postRequest = _mapper.Map<Post>(request.Post);
                var post = _postRepository.Posts.Find(x => x.Name == postRequest.Name);
                Theme theme = _themeRepository.Themes.Find(c => c.Name == request.Post.ThemeName);
                theme.SetName(theme.Name);
                postRequest.Themes.Add(theme);
                var postRepsonse = _postRepository.Posts.Update(post, postRequest);
                _rabbitHelper.SendMessage("Post " + request.Post.Name + " was modified");
                return new ModifyPostReply
                {
                    Post = _mapper.Map<PostMessage>(postRepsonse)
                };

            }
            catch (InvalidNameLength)
            {
                _rabbitHelper.SendMessage("Post "+request.Post.Name + " wasn't modified, invalid empty name");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Name cannot be empty"));
            }
            catch (KeyNotFoundException)
            {
                _rabbitHelper.SendMessage("Post " + request.Post.Name + " wasn't modified");
                throw new RpcException(new Status(StatusCode.NotFound, "not found"));
            }
        }

        public override async Task<DeletePostReply> DeletePost(DeletePostRequest request, ServerCallContext context)
        {
            try
            {
                var post = _postRepository.Posts.Find(x => x.Name == request.Post.Name);
                _postRepository.Posts.Delete(post);
                _rabbitHelper.SendMessage("Post "+request.Post.Name + " was deleted");
                return new DeletePostReply { };
            }
            catch (KeyNotFoundException)
            {
                _rabbitHelper.SendMessage("Post "+request.Post.Name + " wasnt deleted");
                throw new RpcException(new Status(StatusCode.NotFound, "Post not found"));
            }
        }
    }
}