using Microsoft.EntityFrameworkCore;
using Military_Asset_Management_System.Models;

namespace Military_Asset_Management_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Base> Bases { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent multiple cascade paths in Transfer table
            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.FromBase)
                .WithMany()
                .HasForeignKey(t => t.FromBaseId)
                .OnDelete(DeleteBehavior.Restrict);  // 👈 restrict delete

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.ToBase)
                .WithMany()
                .HasForeignKey(t => t.ToBaseId)
                .OnDelete(DeleteBehavior.Restrict);  // 👈 restrict delete
        }
    }
}
