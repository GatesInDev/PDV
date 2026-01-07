using Microsoft.AspNetCore.Http; // Para ter acesso ao DbContext e DbSet
using Microsoft.EntityFrameworkCore;
using PDV.Core.Entities;
using System.Text.Json; // Para ter acesso às entidades

namespace PDV.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) 
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<Config> Configs => Set<Config>();
        public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Logs> Logss { get; set; }

        private string GetUser()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Desconhecido";
        }

        public override int SaveChanges()
        {
            AdicionarAuditoria();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AdicionarAuditoria();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AdicionarAuditoria()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is not Logs &&
                            (e.State == EntityState.Added ||
                             e.State == EntityState.Modified ||
                             e.State == EntityState.Deleted))
                .ToList();

            foreach (var entry in entries)
            {
                var auditoria = new Logs
                {
                    Table = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(),
                    Date = DateTime.UtcNow,
                    User = GetUser(),
                    Data = JsonSerializer.Serialize(entry.CurrentValues.ToObject())
                };

                Logss.Add(auditoria);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Logs>()
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
