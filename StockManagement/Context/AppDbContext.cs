using Microsoft.EntityFrameworkCore;
using StockManagement.Models;
using System;
using System.Linq;

namespace StockManagement.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockTransfer> StockTransfers { get; set; }
        public DbSet<StockManagement.Models.Company> Company { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Company Table
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Products)
                .WithOne(c => c.Company);

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Name)
                .IsUnique();


            // Warehouse Table
            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.Stocks)
                .WithOne(w => w.Warehouse);


            // Product Table
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Company)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CompanyID);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Stocks)
                .WithOne(p => p.Product);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.StockTransfers)
                .WithOne(p => p.Product);

            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.Name })
                .IsUnique();


            // Stock Table
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Warehouse)
                .WithMany(s => s.Stocks)
                .HasForeignKey(s => s.WarehouseID);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Product)
                .WithMany(s => s.Stocks)
                .HasForeignKey(s => s.ProductID);

            modelBuilder.Entity<Stock>()
                .HasMany(s => s.FromStockTransfers)
                .WithOne(s => s.FromStock)
                .HasForeignKey(s => s.FromStockID);

            modelBuilder.Entity<Stock>()
                .HasMany(s => s.ToStockTransfers)
                .WithOne(s => s.ToStock)
                .HasForeignKey(s => s.ToStockID);


            // StockTransfer Table
            modelBuilder.Entity<StockTransfer>()
                .HasOne(s => s.FromStock)
                .WithMany(s => s.FromStockTransfers)
                .HasForeignKey(s => s.FromStockID);

            modelBuilder.Entity<StockTransfer>()
                .HasOne(s => s.ToStock)
                .WithMany(s => s.ToStockTransfers)
                .HasForeignKey(s => s.ToStockID);
        }


        public override int SaveChanges()
        {
            // If created -> set 'CreatedAt'
            // If edited  -> set 'EditedAt'
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is ModelBase && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((ModelBase)entityEntry.Entity).CreatedAt = DateTime.Now;
                }

                else
                {
                    ((ModelBase)entityEntry.Entity).UpdatedAt = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

    }
}
