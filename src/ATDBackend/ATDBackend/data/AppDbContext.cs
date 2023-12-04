using ATDBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<seeds> Seeds { get; set; }
    }
}
