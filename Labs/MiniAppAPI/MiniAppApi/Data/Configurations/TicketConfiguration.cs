using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniAppApi.Models;

namespace MiniAppApi.Data.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Type)
                   .IsRequired()
                   .HasMaxLength(50);
        builder.Property(t => t.Price).HasColumnType("decimal(18,2)").IsRequired();
        builder.HasOne(t => t.Event)
               .WithMany(e => e.Tickets)
               .HasForeignKey(t => t.EventId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Ticket { Id = 1, Type = "Standard", Price = 50.00m, EventId = 1 },
            new Ticket { Id = 2, Type = "VIP", Price = 150.00m, EventId = 1 },
            new Ticket { Id = 3, Type = "Standard", Price = 40.00m, EventId = 2 },
            new Ticket { Id = 4, Type = "Balcony", Price = 70.00m, EventId = 2 }
        );
    }
}
