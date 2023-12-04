using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Database.DBContexts
{
    public class UsersDBContext : DbContext
    {
        public UsersDBContext(DbContextOptions<UsersDBContext> options) : base(options)
        {

        }

        public DbSet<ATDBackend.Database.Models.User> Users { get; set; }
    }
}
