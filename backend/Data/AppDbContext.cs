using backend.Models;
using Microsoft.EntityFrameworkCore;
namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }

    }
}