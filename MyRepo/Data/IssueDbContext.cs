using Microsoft.EntityFrameworkCore;
using MyRepo.Models;

namespace MyRepo.Data
{
    public class ModelDbContext : DbContext
    {
        public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options) 
        {
        }
        public ModelDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<Issue> Issues { get; set; }

        public DbSet<UploadedFile> UploadedFiles { get; set; }

    }
}

