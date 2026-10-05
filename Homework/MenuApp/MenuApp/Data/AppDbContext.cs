using MenuApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuApp.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Product>()
            .Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETDATE()");


        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" },
            new Category { Id = 3, Name = "Category 3" }
        );
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Sample Product",
                Price = 9.99m,
                IsNew = true,
                Stock = 100,
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Another Product",
                Price = 19.99m,
                IsNew = false,
                Stock = 50,
                CategoryId = 1
            },
            new Product
            {
                Id = 3,
                Name = "Third Product",
                Price = 29.99m,
                IsNew = true,
                Stock = 75,
                CategoryId = 2
            },
            new Product
            {
                Id = 4,
                Name = "Fourth Product",
                Price = 39.99m,
                IsNew = false,
                Stock = 20,
                CategoryId = 2
            },
            new Product
            {
                Id = 5,
                Name = "Fifth Product",
                Price = 49.99m,
                IsNew = true,
                Stock = 10,
                CategoryId = 3
            }
        );
    }
}