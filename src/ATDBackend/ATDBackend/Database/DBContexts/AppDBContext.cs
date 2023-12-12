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
        public DbSet<ATDBackend.Database.Models.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Define relationships and indexes here
        {
            //Unique indexes
            modelBuilder.Entity<Role>().HasIndex(e => e.Role_name).IsUnique();
            modelBuilder.Entity<School>().HasIndex(e => e.Name).IsUnique();
            modelBuilder.Entity<Seed>().HasIndex(e => e.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(e => e.Username).IsUnique();

            //Relationships
            modelBuilder
                .Entity<School_Seed>()
                .HasOne(p => p.School)
                .WithMany()
                .HasForeignKey(p => p.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<School_Seed>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<School_Seed>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<School_Seed>()
                .HasOne(p => p.Seed)
                .WithMany()
                .HasForeignKey(p => p.SeedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Order>()
                .HasOne(p => p.School)
                .WithMany()
                .HasForeignKey(p => p.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Order>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Seed_in>()
                .HasOne(p => p.School)
                .WithMany()
                .HasForeignKey(p => p.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Seed_in>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Seed_in>()
                .HasOne(p => p.School_Seed)
                .WithMany()
                .HasForeignKey(p => p.SchoolSeedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Seed_in>()
                .HasOne(p => p.Category_id)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Seed>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Seed>()
                .HasOne(p => p.User_id)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<User>()
                .HasOne(p => p.School_id)
                .WithMany()
                .HasForeignKey(p => p.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<User>()
                .HasOne(p => p.Role_id)
                .WithMany()
                .HasForeignKey(p => p.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
