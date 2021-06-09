﻿using Domain;

namespace BusinessLogic
{
    public abstract class ManagerRepository
    {
        public IRepository<File> Files { get; set; }

        public IRepository<SemaphoreSlimPost> SemaphoreSlimPosts { get; set; }
        
        public IRepository<SemaphoreSlimTheme> SemaphoreSlimThemes { get; set; }
        
        public IRepository<Client> Clients { get; set; }
        
        
    }
}