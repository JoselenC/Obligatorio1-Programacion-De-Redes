using System.Collections.Generic;

namespace DataAccess.DtoOjects
{
    public class PostDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        
        public ICollection<PostThemeDto>  PostsThemesDto { get; set; }
        
        public string CreationDate { get; set; }

        public int FileId { get; set; }
        public FileDto FileDto { get; set; }
    }
}