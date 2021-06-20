using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.Managers;
using DataAccess;
using DomainObjects;
using Grpc.Core;

namespace Server.ServerGrpc.Services
{
    public class ThemeToPostService : ThemeToPostGrpc.ThemeToPostGrpcBase
    {
        private ManagerThemeRepository _themeRepository;
        private ManagerPostRepository _postRepository;
        private RabbitHelper _rabbitHelper;
        public ThemeToPostService()
        {
            _rabbitHelper = new RabbitHelper();
            _postRepository = new DataBasePostRepository();
            _themeRepository = new DataBaseThemeRepository();
        }

      public override async Task<AssociateThemeToPostReply> AssociateThemeToPost(AssociateThemeToPostRequest request,
          ServerCallContext context)
      {
          try
          {
              Post post = _postRepository.Posts.Find(x => x.Name == request.ThemeToPost.PostName);
              Post newPost = _postRepository.Posts.Find(x => x.Name == request.ThemeToPost.PostName);
              Theme theme = _themeRepository.Themes.Find(x => x.Name == request.ThemeToPost.ThemeName);
              newPost.Themes.Add(theme);
              _postRepository.Posts.Update(post, newPost);
              _rabbitHelper.SendMessage("Theme "+request.ThemeToPost.ThemeName + " was associated to post " + request.ThemeToPost.PostName);
              return new AssociateThemeToPostReply
              {
                 ThemeToPost= new ThemeToPost()
                 {
                     PostName = newPost.Name, ThemeName = theme.Name
                 }
              };
          }
          catch (KeyNotFoundException)
          {
              _rabbitHelper.SendMessage("Theme "+request.ThemeToPost.ThemeName + " wasn't associated to post " + request.ThemeToPost.PostName);
              throw new RpcException(new Status(StatusCode.NotFound, "Not found"));
          }
      }

      public override async Task<DissasociateThemeToPostReply> DissasociateThemeToPost(DissasociateThemeToPostRequest request, ServerCallContext context)
      {
          try
          {
              Post post = _postRepository.Posts.Find(x => x.Name == request.ThemeToPost.PostName);
              Post newPost = _postRepository.Posts.Find(x => x.Name == request.ThemeToPost.PostName);
              Theme theme = _themeRepository.Themes.Find(x => x.Name == request.ThemeToPost.ThemeName);
              if (newPost.Themes != null)
              {
                  newPost.Themes.Remove(theme);
                  _postRepository.Posts.Update(post, newPost);
              }
              _rabbitHelper.SendMessage("Theme "+request.ThemeToPost.ThemeName + " was dissasociated to post " + request.ThemeToPost.PostName);
              return new DissasociateThemeToPostReply();
          }
          catch (KeyNotFoundException)
          {
              _rabbitHelper.SendMessage("Theme "+request.ThemeToPost.ThemeName + " wasn't dissasociated to post " + request.ThemeToPost.PostName);
              throw new RpcException(new Status(StatusCode.NotFound, "Not found"));
          }
      }
    }
}