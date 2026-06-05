using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchApp.Domain.Entities;
using OnionArchApp.Domain.Enums;

namespace OnionArchApp.Persistance.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        builder.HasData(new Product
        {
            Id = 1,
            Name = "Smartphone",
            Status = ProductStatus.Featured,
            Price = 699.99m,
            CategoryId = 1
        }, new Product
        {
            Id = 2,
            Name = "Laptop",
            Status = ProductStatus.None,
            Price = 999.99m,
            CategoryId = 1
        }, new Product
        {
            Id = 3,
            Name = "Novel Book",
            Status = ProductStatus.None,
            Price = 19.99m,
            CategoryId = 2
        }, new Product
        {
            Id = 4,
            Name = "T-Shirt",
            Status = ProductStatus.New,
            Price = 14.99m,
            CategoryId = 3
        }, new Product
        {
            Id = 5,
            Name = "Blender",
            Status = ProductStatus.New,
            Price = 49.99m,
            CategoryId = 4
        }, new Product
        {
            Id = 6,
            Name = "Yoga Mat",
            Price = 29.99m,
            CategoryId = 5
        });
    }
}