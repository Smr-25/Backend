using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniAppApi.Models;

namespace MiniAppApi.Data.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(150);
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.Date).IsRequired();
     
        builder.Property(e => e.Location)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(e => e.BannerImageUrl).IsRequired(false);   
        builder.HasOne(e => e.Organizer)
            .WithMany(o => o.Events)
            .HasForeignKey(e => e.OrganizerId)
            .OnDelete(DeleteBehavior.Cascade);

    builder.HasData(
        new Event
        {
            Id = 1,
            Title = "Tech Conference 2024",
            Description = "An annual conference focusing on the latest in technology.",
            Date = new DateTime(2024, 9, 15),
        Location = "San Francisco, CA",
        BannerImageUrl = "https://example.com/images/tech-conference-2024.jpg",
        OrganizerId = 1
    },
    
    new Event
    {
        Id = 2,
        Title = "Music Festival",
        Description = "A weekend of live music performances from various artists.",
        Date = new DateTime(2024, 7, 20),
        Location = "Austin, TX",
        BannerImageUrl = "https://example.com/images/music-festival-2024.jpg",
        OrganizerId = 2
    },
    new Event
    {
        Id = 3,
        Title = "Art Expo",
        Description = "An exhibition showcasing contemporary art from local artists.",
        Date = new DateTime(2024, 11, 5),
        Location = "New York, NY",
        BannerImageUrl = "https://example.com/images/art-expo-2024.jpg",
        OrganizerId = 1
    }
    );
     }
}
