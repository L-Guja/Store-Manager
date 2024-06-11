using CatalogRepository.DTO;
using Microsoft.EntityFrameworkCore;

namespace CatalogRepository
{
    public class CatalogDbContext : DbContext
    {
        private const string ConnectionString =
            @"server = DESKTOP-K9OTDM7.\sqlserver; database = Northwind; integrated security = true; TrustServerCertificate = true";

        public CatalogDbContext() : base(GetOptions()) { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        private static DbContextOptions GetOptions()
        {
            var options = new DbContextOptionsBuilder();
            options.UseSqlServer(ConnectionString);
            return options.Options;
        }
    }
}