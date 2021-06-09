using System.Collections.Generic;
using System.Linq;
using DataAccess.DtoOjects;
using Domain;
using Microsoft.EntityFrameworkCore;
using MSP.BetterCalm.DataAccess;

namespace DataAccess.Mappers
{
    public class PostMapper: IMapper<Post, PostDto>
    {
        public PostDto DomainToDto(Post obj, ContextDb context)
        {
            PostDto postDto = context.Posts.FirstOrDefault(x => x.Name == obj.Name);
            if (postDto is null)
            {
                postDto = new PostDto()
                {
                    Name = obj.Name,
                    CreationDate = obj.CreationDate,
                };
                if (obj.File != null)
                    postDto.FileDto = new FileMapper().DomainToDto(obj.File, context);
            }
            else
            {
                context.Entry(postDto).Collection("PostsThemesDto").Load();
            }

            List<ThemeDto> themes = CreateThemes(obj.Themes, context);
            postDto.PostsThemesDto = new List<PostThemeDto>();
            foreach (var themeDto in themes)
            {
                postDto.PostsThemesDto.Add(new PostThemeDto(){Post = postDto, Theme = themeDto});
            }
            return postDto;
        }
        
        public List<ThemeDto> CreateThemes(List<Theme> themes, ContextDb context)
        {
              
            List<ThemeDto> themesDto = new List<ThemeDto>();
            DbSet<ThemeDto> themesSet = context.Set<ThemeDto>();
            List<PostThemeDto> postThemeDtos = new List<PostThemeDto>();
            if (!(themes is null))
            {
                foreach (Theme theme in themes)
                {
                    ThemeDto themeDto= themesSet.FirstOrDefault(x => x.Name == theme.Name);
                    if (themeDto == null)
                    {
                        themeDto = new ThemeDto()
                        {
                            Description = theme.Description,
                            Name = theme.Name
                        };
                    }
                    if (theme.Posts != null)
                    {
                        foreach (var post in theme.Posts)
                        {
                            postThemeDtos = new List<PostThemeDto>()
                            {
                                new PostThemeDto()
                                {
                                    Theme = themeDto,
                                }
                            };
                        }
                    }
                    themeDto.PostsThemesDto = postThemeDtos;
                    themesDto.Add(themeDto);
                }
            }
            return themesDto;
        }

        public Post DtoToDomain(PostDto obj, ContextDb context)
        {
            DbSet<ThemeDto> themeSet = context.Set<ThemeDto>();
            List<Theme> themes = new List<Theme>();
            context.Entry(obj).Collection("PostsThemesDto").Load();
            if (!(obj.PostsThemesDto is null))
            {
                foreach (PostThemeDto postThemeDto in obj.PostsThemesDto)
                { 
                    ThemeDto themeDto= themeSet.FirstOrDefault(x => x.Id == postThemeDto.ThemeId);
                    if (themeDto != null)
                    {
                        Theme theme = new Theme()
                        {
                            Name = themeDto.Name,
                            Description = themeDto.Description,
                            Id = themeDto.Id
                        };
                        themes.Add(theme);
                    }
                }
            }
            
            Post post = new Post()
            {
                Name = obj.Name,
                Id = obj.Id,
                CreationDate = obj.CreationDate,
                Themes = themes
            };
            if(obj.FileDto!=null)
              post.File = new FileMapper().DtoToDomain(obj.FileDto, context);
            return post;
        }

        public PostDto UpdateDtoObject(PostDto objToUpdate, Post updatedObject, ContextDb context)
        {
            ThemeMapper themeMapper = new ThemeMapper();
            objToUpdate.Name = updatedObject.Name;
            objToUpdate.CreationDate = updatedObject.CreationDate;
            if(updatedObject.File!=null)
             objToUpdate.FileDto = new FileMapper().DomainToDto(updatedObject.File,context);
            var diffListOldValuesThemes = UpdatePostsThemesDto(objToUpdate, updatedObject, context, themeMapper);
            objToUpdate.PostsThemesDto = diffListOldValuesThemes;
            return objToUpdate;
        }
        
        private static List<PostThemeDto> UpdatePostsThemesDto(PostDto objToUpdate, Post updatedObject, ContextDb context,
            ThemeMapper themeMapper)
        {
            List<PostThemeDto> postsThemesToDelete = objToUpdate.PostsThemesDto.Where(x =>
                x.Post.Name == objToUpdate.Name).ToList();
            
            foreach (PostThemeDto postTheme in postsThemesToDelete)
            {
                context.Set<PostThemeDto>().Remove(postTheme);
                context.SaveChanges();
            }
            List<Theme> newThemessToAdd = new List<Theme>();
            List<PostThemeDto> postsThemesToAdd = new List<PostThemeDto>();
            if (updatedObject.Themes != null)
            {
                foreach (var theme in updatedObject.Themes)
                {
                    Theme themeToAdd = themeMapper.DtoToDomain(context.Themes
                        .FirstOrDefault(x => x.Name == theme.Name), context);
                    newThemessToAdd.Add(themeToAdd);
                }

                postsThemesToAdd.AddRange(newThemessToAdd.Select(x => new PostThemeDto() {
                    Theme = themeMapper.DomainToDto(x, context), ThemeId = x.Id,
                    Post = objToUpdate, PostId = objToUpdate.Id}));
            }
            return postsThemesToAdd;
        }
        
    }
}