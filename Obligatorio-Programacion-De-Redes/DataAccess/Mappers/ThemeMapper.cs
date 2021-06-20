using System.Collections.Generic;
using System.Linq;
using DataAccess.DtoOjects;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using MSP.BetterCalm.DataAccess;

namespace DataAccess.Mappers
{
    public class ThemeMapper: IMapper<Theme, ThemeDto>
    {
        public ThemeDto DomainToDto(Theme obj, ContextDb context)
        {
            ThemeDto themeDto = context.Themes.FirstOrDefault(x => x.Name == obj.Name);
          
            if (themeDto is null)
            {
                themeDto = new ThemeDto()
                {
                    Name = obj.Name,
                    Description = obj.Description,
                };
            }
            else
            {
                context.Entry(themeDto).Collection("PostsThemesDto").Load();
            }

            List<PostDto> posts = CreatePost(obj.Posts, context);
            themeDto.PostsThemesDto = new List<PostThemeDto>();
            foreach (var post in posts)
            {
                themeDto.PostsThemesDto.Add(new PostThemeDto(){Post = post, Theme = themeDto});
            }
            return themeDto;
        }
        
        private List<PostDto> CreatePost(List<Post> posts, ContextDb context)
        {
              
            List<PostDto> postsDto = new List<PostDto>();
            DbSet<PostDto> postsSet = context.Set<PostDto>();
            List<PostThemeDto> postThemeDtos = new List<PostThemeDto>();
            if (!(posts is null))
            {
                foreach (Post post in posts)
                {
                    PostDto postDto= postsSet.FirstOrDefault(x => x.Name == post.Name);
                    if (postDto == null)
                    {
                        postDto = new PostDto()
                        {
                            Name = post.Name,
                            CreationDate = post.CreationDate
                        };
                    }
                  postDto.PostsThemesDto = postThemeDtos;
                    postsDto.Add(postDto);
                }
            }
            return postsDto;
        }

        public Theme DtoToDomain(ThemeDto obj, ContextDb context)
        {
            DbSet<PostDto> postSet = context.Set<PostDto>();
            List<Post> posts = new List<Post>();
            context.Entry(obj).Collection("PostsThemesDto").Load();
            if (!(obj.PostsThemesDto is null))
            {
                foreach (PostThemeDto postThemeDto in obj.PostsThemesDto)
                { 
                    PostDto postDto= postSet.FirstOrDefault(x => x.Id == postThemeDto.PostId);
                    if(postDto!=null)
                        posts.Add(new PostMapper().DtoToDomain(postDto,context));
                }
            }
            
            return new Theme()
            {
                Name = obj.Name,
                Id = obj.Id,
                Description = obj.Description,
                Posts = posts
            };
        }

        public ThemeDto UpdateDtoObject(ThemeDto objToUpdate, Theme updatedObject, ContextDb context)
        {
            PostMapper postMapper = new PostMapper();
            objToUpdate.Name = updatedObject.Name;
            objToUpdate.Description = updatedObject.Description;
            var diffListOldValuesPosts = UpdatePostsThemesDto(objToUpdate, updatedObject, context, postMapper);
            objToUpdate.PostsThemesDto = diffListOldValuesPosts;
            return objToUpdate;
        }
        
        private static List<PostThemeDto> UpdatePostsThemesDto(ThemeDto objToUpdate, Theme updatedObject, ContextDb context,
            PostMapper postMapper)
        {
            List<PostThemeDto> postsThemesToDelete = objToUpdate.PostsThemesDto.Where(x =>
                x.Theme.Name == objToUpdate.Name).ToList();
            
            foreach (PostThemeDto postTheme in postsThemesToDelete)
            {
                context.Set<PostThemeDto>().Remove(postTheme);
                context.SaveChanges();
            }
            List<Post> newPostsToAdd = new List<Post>();
            List<PostThemeDto> postsThemesToAdd = new List<PostThemeDto>();
            if (updatedObject.Posts != null)
            {
                foreach (var post in updatedObject.Posts)
                {
                    Post postToAdd = postMapper.DtoToDomain(context.Posts
                        .FirstOrDefault(x => x.Name == post.Name), context);
                    newPostsToAdd.Add(postToAdd);
                }

                postsThemesToAdd.AddRange(newPostsToAdd.Select(x => new PostThemeDto() {
                    Post = postMapper.DomainToDto(x, context), PostId = x.Id,
                    Theme = objToUpdate, ThemeId = objToUpdate.Id}));
            }
            return postsThemesToAdd;
        }

    }
}