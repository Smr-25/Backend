namespace MentorApp.Models;

public class PricingService
{
    public int PricingId { get; set; }
    public Pricing? Pricing { get; set; }
    
    public int ServiceId { get; set; }
    public Service? Service { get; set; }
}

