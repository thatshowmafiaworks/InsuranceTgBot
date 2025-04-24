using InsuranceTgBot.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceTgBot.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(a => a.UserData)
                .WithOne(a => a.User)
                .HasForeignKey<UserData>(a => a.UserId);
        }
        public DbSet<DriverLicense> DriverLicenses { get; set; }
        public DbSet<VehicleDocument> VehicleDocuments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HistoryRecord> HistoryRecords { get; set; }
        public DbSet<UserData> UserDatas { get; set; }
    }
}
