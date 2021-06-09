using System.Collections;
using System.Collections.Generic;
using Domain;

namespace DataAccess.DtoOjects
{
    public class ThemeDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        
        public string Description { get; set; }
        
        public ICollection<PostThemeDto> PostsThemesDto { get; set; }
        
        public ICollection<FileThemeDto> FilesThemesDto { get; set; }


        public override bool Equals(object? obj)
        {
            return ((ThemeDto) obj).Name == Name;
        }
    }
}