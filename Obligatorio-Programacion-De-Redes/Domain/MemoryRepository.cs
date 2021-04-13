﻿using System.Collections.Generic;
using Domain;

namespace BusinessLogic
{
    public class MemoryRepository
    {
        public List<Post> Posts { get; set; }
        public List<Theme> Themes { get; set; }
        public List<Archive> Archive { get; set; }

        public MemoryRepository()
        {
            Posts = new List<Post>();
            Themes = new List<Theme>();
            Archive = new List<Archive>();
        }
    }
}