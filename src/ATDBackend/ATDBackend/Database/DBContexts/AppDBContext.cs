using ATDBackend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Database.DBContexts
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SchoolSeed> Inventory { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Seed_in> Seeds_in { get; set; }
        public DbSet<Seed> Seeds { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Define relationships and indexes here
        {
            //Unique indexes
            modelBuilder.Entity<Role>().HasIndex(e => e.RoleName).IsUnique();
            modelBuilder.Entity<School>().HasIndex(e => e.Name).IsUnique();
            modelBuilder.Entity<Seed>().HasIndex(e => e.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(e => e.Username).IsUnique();

        }
    }
}
