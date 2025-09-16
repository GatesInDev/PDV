using PDV.Core.Entities; // Para ter acesso às entidades

using Microsoft.EntityFrameworkCore; // Para ter acesso ao DbContext e DbSet

namespace PDV.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Stock>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(cid => cid.CategoryId);

            modelBuilder.Entity<Product>()
                .HasOne(s => s.Stock)
                .WithOne(p => p.Product)
                .HasForeignKey<Stock>(p => p.ProductId);
        }
    }
}
