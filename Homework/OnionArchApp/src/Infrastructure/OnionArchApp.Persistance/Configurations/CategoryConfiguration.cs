using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchApp.Domain.Entities;

namespace OnionArchApp.Persistance.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
        builder.HasData(new Category
            {
                Id = 1,
                Name = "Electronics"
            }, new Category
            {
                Id = 2,
                Name = "Books",
            }, new Category
            {
                Id = 3,
                Name = "Clothing"
            },
            new Category
            {
                Id = 4,
                Name = "Home & Kitchen"
            }
            , new Category
            {
                Id = 5,
                Name = "Sports & Outdoors"
            }
        );
    }
}