using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Military_Asset_Management_System.Models;

namespace Military_Asset_Management_System.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=MilitaryAssetDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
