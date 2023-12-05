using ATDBackend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Database.DBContexts
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options) { }

        public DbSet<ATDBackend.Database.Models.User> Users { get; set; }
        public DbSet<ATDBackend.Database.Models.Seed> Seeds { get; set; }
        public DbSet<ATDBackend.Database.Models.SeedCategory> SeedCategories { get; set; }
        public DbSet<ATDBackend.Database.Models.School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Define relationships and indexes here
        {
            modelBuilder.Entity<SeedCategory>().HasIndex(e => e.Category_name).IsUnique();
        }
    }
}
