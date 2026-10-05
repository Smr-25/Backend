using MentorApp.Models;

namespace MentorApp.ViewModels;

public class PricingVm
{
    public List<Pricing> Pricings { get; set; } = new();
    public List<Service> Services { get; set; } = new();
}

