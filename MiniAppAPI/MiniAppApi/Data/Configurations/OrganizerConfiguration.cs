using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniAppApi.Models;

namespace MiniAppApi.Data.Configurations;

public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
{
    public void Configure(EntityTypeBuilder<Organizer> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(o => o.Email)
            .IsRequired();
        builder.Property(o => o.Phone).HasMaxLength(20);
        builder.Property(o => o.LogoImageUrl)
            .IsRequired(false);

        builder.HasData(
            new Organizer
            {
                Id = 1,
                Name = "Tech Conferences Inc.",
                Email = "samiralmammadli@gmail.com",
                Phone = "123-456-7890",
                LogoImageUrl = "https://example.com/logos/tech_conferences.png",
                Events = new List<Event>()

            },
            new Organizer
            {
                Id = 2,
                Name = "Health Summit Org.",
                Email = "",
                Phone = "987-654-3210",
                LogoImageUrl = "https://example.com/logos/health_summit.png"
            }
);
    }
}
