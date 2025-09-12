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
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Stock)
                .WithOne(p => p.Product)
                .HasForeignKey<Stock>(p => p.ProductId);
        }
    }
}
