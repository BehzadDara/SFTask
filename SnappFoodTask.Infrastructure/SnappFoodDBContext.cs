using Microsoft.EntityFrameworkCore;
using SnappFoodTask.Domain.Models;

namespace SnappFoodTask.Infrastructure
{
    public class SnappFoodDBContext : DbContext
    {
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<User>? Users { get; set; }

        public SnappFoodDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(x => x.Id);

            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().HasIndex(x => x.Title).IsUnique();
            modelBuilder.Entity<Product>()
                .HasData(
                    new Product { Id = 1, Title = "Init product",InventoryCount = 3, Price = 100, Discount = 20 }
                );

            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasMany(x => x.Orders)
                .WithOne(x => x.Buyer);
            modelBuilder.Entity<User>()
                .HasData(
                    new User { Id = 1, Name = "A"},
                    new User { Id = 2, Name = "B"},
                    new User { Id = 3, Name = "C"}
                );
        }
    }
}
