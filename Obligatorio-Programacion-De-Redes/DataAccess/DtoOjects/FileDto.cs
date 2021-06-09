using System;
using System.Collections;
using System.Collections.Generic;
using Domain;

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