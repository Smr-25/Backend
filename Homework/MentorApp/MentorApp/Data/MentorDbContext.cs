using MentorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MentorApp.Data;

public class MentorDbContext(DbContextOptions<MentorDbContext> options) : DbContext(options)
{
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<WhyUs> WhyUss { get; set; }
    public DbSet<Pricing> Pricings { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<PricingService> PricingServices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pricing>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<PricingService>()
            .HasKey(ps => new { ps.PricingId, ps.ServiceId });
    }
}