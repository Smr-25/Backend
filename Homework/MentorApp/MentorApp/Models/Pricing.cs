namespace MentorApp.Models;

public class Pricing
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsAdvanced { get; set; }
    public bool IsFeatured { get; set; }
    
    public List<PricingService> PricingServices { get; set; } = new List<PricingService>();
}