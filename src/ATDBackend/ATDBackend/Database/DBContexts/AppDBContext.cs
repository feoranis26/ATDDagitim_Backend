using ATDBackend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Database.DBContexts
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options) { }

        public DbSet<ATDBackend.Database.Models.Category> Categories { get; set; }
        public DbSet<ATDBackend.Database.Models.Order> Orders { get; set; }
        public DbSet<ATDBackend.Database.Models.Role> Roles { get; set; }
        public DbSet<ATDBackend.Database.Models.School_Seed> Inventory { get; set; }
        public DbSet<ATDBackend.Database.Models.School> Schools { get; set; }
        public DbSet<ATDBackend.Database.Models.Seed_in> Seeds_in { get; set; }
        public DbSet<ATDBackend.Database.Models.Seed> Seeds { get; set; }
        public DbSet<ATDBackend.Database.Models.SeedCategory> SeedCategories { get; set; }
        public DbSet<ATDBackend.Database.Models.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Define relationships and indexes here
        {
            //Unique indexes
            modelBuilder.Entity<SeedCategory>().HasIndex(e => e.Category_name).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(e => e.Role_name).IsUnique();
            modelBuilder.Entity<School>().HasIndex(e => e.Name).IsUnique();

            //Relationships

        }
    }
}
