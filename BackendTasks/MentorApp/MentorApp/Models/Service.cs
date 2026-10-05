 namespace MentorApp.Models;

public class Service
{
    public int Id { get; set; }
    public required string Title { get; set; }
    
    public List<PricingService> PricingServices { get; set; } = new List<PricingService>();
}

