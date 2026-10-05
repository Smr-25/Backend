using EternaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EternaApp.Data;

public class EternaDbContext : DbContext
{
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<PortfolioImage> PortfolioImages { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Feature> Features { get; set; }

    public EternaDbContext(DbContextOptions<EternaDbContext> options) : base(options)
    {
    }
}