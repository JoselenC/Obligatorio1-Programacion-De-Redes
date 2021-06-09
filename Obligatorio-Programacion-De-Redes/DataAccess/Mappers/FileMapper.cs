using System.Collections.Generic;
using System.Linq;
using DataAccess.DtoOjects;
using Domain;
using Microsoft.EntityFrameworkCore;
using MSP.BetterCalm.DataAccess;

namespace DataAccess.Mappers
{
    public class FileMapper: IMapper<File, FileDto>
    {
        public FileDto DomainToDto(File obj, ContextDb context)
        {
            FileDto fileDto = context.Files.FirstOrDefault(x => x.Name == obj.Name);
            if (fileDto is null)
            {
                fileDto = new FileDto()
                {
                    Name = obj.Name,
                    Size = obj.Size,
                    UploadDate = obj.UploadDate,
                    Post = new PostMapper().DomainToDto(obj.Post,context)
                };
            }
            else
            {
                context.Entry(fileDto).Collection("FilesThemesDto").Load();
            }

            List<ThemeDto> themes = CreateThemes(obj.Themes, context);
            fileDto.FilesThemesDto = new List<FileThemeDto>();
            foreach (var themeDto in themes)
            {
                fileDto.FilesThemesDto.Add(new FileThemeDto(){File = fileDto, Theme = themeDto});
            }
            return fileDto;
        }

        private List<ThemeDto> CreateThemes(List<Theme> themes, ContextDb context)
        {
              
            List<ThemeDto> themesDto = new List<ThemeDto>();
            DbSet<ThemeDto> themesSet = context.Set<ThemeDto>();
            List<PostThemeDto> postThemeDtos = new List<PostThemeDto>();
            if (!(themes is null))
            {
                foreach (Theme theme in themes)
                {
                    ThemeDto themeDto= themesSet.FirstOrDefault(x => x.Id == theme.Id);
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
                                    Post = new PostMapper().DomainToDto(post,context)
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

        public File DtoToDomain(FileDto obj, ContextDb context)
        {
            DbSet<ThemeDto> themesSet = context.Set<ThemeDto>();
            List<Theme> themes = new List<Theme>();
            context.Entry(obj).Collection("FilesThemesDto").Load();
            if (!(obj.FilesThemesDto is null))
            {
                foreach (FileThemeDto fileThemeDto in obj.FilesThemesDto)
                { 
                    ThemeDto themeDto= themesSet.FirstOrDefault(x => x.Id == fileThemeDto.ThemeId);
                    if(themeDto!=null)
                        themes.Add(new ThemeMapper().DtoToDomain(themeDto,context));
                }
            }
            return new File()
            {
                Name = obj.Name,
                Id = obj.Id,
                Size = obj.Size,
                UploadDate = obj.UploadDate,
                Post = new PostMapper().DtoToDomain(obj.Post,context),
                Themes = themes
            };
        }

        public FileDto UpdateDtoObject(FileDto objToUpdate, File updatedObject, ContextDb context)
        {
            ThemeMapper themeMapper = new ThemeMapper();
            objToUpdate.Name = updatedObject.Name;
            objToUpdate.Size = updatedObject.Size;
            objToUpdate.UploadDate = updatedObject.UploadDate;
            objToUpdate.Post = new PostMapper().DomainToDto(updatedObject.Post,context);
            var diffListOldValuesThemes = UpdateFilesThemesDto(objToUpdate, updatedObject, context, themeMapper);
            objToUpdate.FilesThemesDto = diffListOldValuesThemes;
            return objToUpdate;
        }
        
        private static List<FileThemeDto> UpdateFilesThemesDto(FileDto objToUpdate, File updatedObject, ContextDb context,
            ThemeMapper themeMapper)
        {
            List<FileThemeDto> filesThemesToDelete = objToUpdate.FilesThemesDto.Where(x =>
                x.FileId == objToUpdate.Id).ToList();
            
            foreach (FileThemeDto fileTheme in filesThemesToDelete)
            {
                context.Set<FileThemeDto>().Remove(fileTheme);
                context.SaveChanges();
            }
            List<Theme> newThemessToAdd = new List<Theme>();
            List<FileThemeDto> filesThemesToAdd = new List<FileThemeDto>();
            if (updatedObject.Themes != null)
            {
                foreach (var theme in updatedObject.Themes)
                {
                    Theme themeToAdd = themeMapper.DtoToDomain(context.Themes
                        .FirstOrDefault(x => x.Id == theme.Id || x.Name == theme.Name), context);
                    newThemessToAdd.Add(themeToAdd);
                }

                filesThemesToAdd.AddRange(newThemessToAdd.Select(x => new FileThemeDto() {
                    Theme = themeMapper.DomainToDto(x, context), ThemeId = x.Id,
                    File = objToUpdate, FileId = objToUpdate.Id}));
            }
            return filesThemesToAdd;
        }
    }
}