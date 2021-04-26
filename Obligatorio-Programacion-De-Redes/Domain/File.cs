using System;
using System.Collections.Generic;

namespace Domain
{
    public class File
    {
        public string Name { get; set; }
        public double Size { get; set; }
        
        public List<Theme> Themes { get; set; }

        public Post Post { get; set; }
        public DateTime UploadDate { get; set; }
    }
}