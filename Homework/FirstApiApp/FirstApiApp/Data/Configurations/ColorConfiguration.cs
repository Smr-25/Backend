using FirstApiApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstApiApp.Data.Configurations;

public class ColorConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.HasData(
            new Color { Id = 1, Name = "Red" },
            new Color { Id = 2, Name = "Blue" },
            new Color { Id = 3, Name = "Green" },
            new Color { Id = 4, Name = "Yellow" },
            new Color { Id = 5, Name = "Black" },
            new Color { Id = 6, Name = "White" }
        );
    }
}