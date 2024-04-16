using ATDBackend.Database.Models;
using Microsoft.EntityFrameworkCore;
//DB update test

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

        }
    }
}
