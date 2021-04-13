using System;
using System.Collections.Generic;

namespace Domain
{
    public class Post
    {
        public string Name { get; set; }
        public List<Theme>  Theme { get; set; }
        public string CreationDate { get; set; }
        public File File { get; set; }
    }
}