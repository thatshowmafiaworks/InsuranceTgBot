using InsuranceTgBot.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceTgBot.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
