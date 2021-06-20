using System;
using System.Collections.Generic;

namespace DataAccess.DtoOjects
{
    public class FileDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        
        public double Size { get; set; }
        
        public ICollection<FileThemeDto> FilesThemesDto { get; set; }
        
        public PostDto Post { get; set; }
        
        public DateTime UploadDate { get; set; }
    }
}