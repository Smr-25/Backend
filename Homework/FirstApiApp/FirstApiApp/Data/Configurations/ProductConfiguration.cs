using FirstApiApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstApiApp.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).HasMaxLength(500);
        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(p => p.CreatedDate).HasDefaultValue(DateTime.UtcNow.ToString("yyyy-MM-dd"));
        builder.Property(p => p.UptadeDate).IsRequired(false);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Product
            {
                Id = 1, Name = "Sample Product 1", Description = "This is a sample product 1", Price = 9.99M,
                CategoryId = 1
            },
            new Product
            {
                Id = 2, Name = "Sample Product 2", Description = "This is a sample product 2", Price = 19.99M,
                CategoryId = 1
            },
            new Product
            {
                Id = 3, Name = "Sample Product 3", Description = "This is a sample product 3", Price = 29.99M,
                CategoryId = 2
            },
            new Product
            {
                Id = 4, Name = "Sample Product 4", Description = "This is a sample product 4", Price = 39.99M,
                CategoryId = 2
            },
            new Product
            {
                Id = 5, Name = "Sample Product 5", Description = "This is a sample product 5", Price = 49.99M,
                CategoryId = 3
            },
            new Product
            {
                Id = 6, Name = "Sample Product 6", Description = "This is a sample product 6", Price = 59.99M,
                CategoryId = 3
            },
            new Product
            {
                Id = 7, Name = "Sample Product 7", Description = "This is a sample product 7", Price = 69.99M,
                CategoryId = 4
            },
            new Product
            {
                Id = 8, Name = "Sample Product 8", Description = "This is a sample product 8", Price = 79.99M,
                CategoryId = 4
            },
            new Product
            {
                Id = 9, Name = "Sample Product 9", Description = "This is a sample product 9", Price = 89.99M,
                CategoryId = 5
            },
            new Product
            {
                Id = 10, Name = "Sample Product 10", Description = "This is a sample product 10", Price = 99.99M,
                CategoryId = 5
            }
        );
    }
}