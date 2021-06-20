using System;
using System.Collections.Generic;

namespace DomainObjects
{
    public class File
    {
        public string Name { get; set; }
        public double Size { get; set; }
        
        public List<Theme> Themes { get; set; }
        public Post Post { get; set; }
        public DateTime UploadDate { get; set; }
        public int Id { get; set; }
        
        public override bool Equals(object? obj)
        {
            return ((File) obj)?.Name == Name;
        }
    }
}