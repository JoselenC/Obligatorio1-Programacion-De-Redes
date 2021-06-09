using System.Diagnostics.CodeAnalysis;
using System.IO;
using DataAccess.DtoOjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace MSP.BetterCalm.DataAccess
{
    public class ContextDb: DbContext
    { 
        public DbSet<FileDto> Files { get; set; }
        public DbSet<PostDto> Posts { get; set; }
        public DbSet<ThemeDto> Themes { get; set; }
        public DbSet<ClientDto> Clients { get; set; }
        
        public DbSet<SemaphoreSlimPostDto> SemaphoresSlimPostDto { get; set; }
        
        public DbSet<SemaphoreSlimThemeDto> SemaphoresSlimThemeDto { get; set; }
        public ContextDb() { }
        public ContextDb(DbContextOptions<ContextDb> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostThemeDto>()
                .HasKey(mc => new { PostId = mc.PostId, ThemeId = mc.ThemeId});
            modelBuilder.Entity<PostThemeDto>()
                .HasOne(mc => mc.Post)
                .WithMany(m => m.PostsThemesDto)
                .HasForeignKey(mc => mc.PostId);
            modelBuilder.Entity<PostThemeDto>()
                .HasOne(mc => mc.Theme)
                .WithMany(c => c.PostsThemesDto)
                .HasForeignKey(mc => mc.ThemeId);
            modelBuilder.Entity<PostDto>()
                .HasOne(b => b.FileDto)
                .WithOne(i => i.Post)
                .HasForeignKey<FileDto>(b => b.Id);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString(@"BetterCalmDB");
                optionsBuilder.UseSqlServer(connectionString);
        
            }
        }        
    }
}