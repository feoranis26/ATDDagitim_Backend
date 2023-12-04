using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            IConfiguration conf = (
                JsonConfigurationExtensions
                    .AddJsonFile(
                        new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()),
                        "appsettings.json"
                    )
                    .Build()
            );
            string stringUrl = conf["ConnectionStrings:MyWepApiConnection"];

            optionsBuilder.UseNpgsql(stringUrl);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
