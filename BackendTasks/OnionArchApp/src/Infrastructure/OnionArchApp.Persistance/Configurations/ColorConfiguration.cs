using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchApp.Domain.Entities;

namespace OnionArchApp.Persistance.Configurations;

public class ColorConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.HasMany(c => c.ProductColors)
            .WithOne(pc => pc.Color)
            .HasForeignKey(pc => pc.ColorId);
        builder.HasData(
            new Color { Id = 1, Name = "Red", HexCode = "#FF0000" },
            new Color { Id = 2, Name = "Blue", HexCode = "#0000FF" },
            new Color { Id = 3, Name = "Green", HexCode = "#00FF00" },
            new Color { Id = 4, Name = "Black", HexCode = "#000000" },
            new Color { Id = 5, Name = "White", HexCode = "#FFFFFF" }
        );
    }
}