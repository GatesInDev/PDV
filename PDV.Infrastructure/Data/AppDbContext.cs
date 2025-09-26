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
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<Customer> Customers => Set<Customer>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Stock>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<StockTransaction>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Customer>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<StockTransaction>()
                .HasOne(p => p.Product)
                .WithMany(s => s.StockTransactions)
                .HasForeignKey(pid => pid.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(cid => cid.CategoryId);

            modelBuilder.Entity<Product>()
                .HasOne(s => s.Stock)
                .WithOne(p => p.Product)
                .HasForeignKey<Stock>(p => p.ProductId);

            modelBuilder.Entity<SaleProduct>()
                .HasKey(sp => new { sp.SaleId, sp.ProductId });

            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Sale)
                .WithMany(s => s.SaleProducts)
                .HasForeignKey(sp => sp.SaleId);

            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Product)
                .WithMany(p => p.SaleProducts)
                .HasForeignKey(sp => sp.ProductId);

            modelBuilder.Entity<Sale>()
               .HasOne(c => c.Customer)
               .WithMany(s => s.Sales)
               .HasForeignKey(fk => fk.CustomerId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
