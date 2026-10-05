using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PustokApp.Models;

namespace PustokApp.Data.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasMany(x=>x.BookImages)
            .WithOne(x=>x.Book)
            .HasForeignKey(x=>x.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x=>x.Name).IsRequired().HasMaxLength(150);
    }
}
