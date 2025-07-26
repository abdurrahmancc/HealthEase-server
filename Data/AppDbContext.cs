using HealthEase.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace HealthEase.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
    }
}
