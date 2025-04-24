using InsuranceTgBot.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceTgBot.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<DriverLicense> DriverLicenses { get; set; }
        public DbSet<VehicleDocument> VehicleDocuments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HistoryRecord> HistoryRecords { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
    }
}
