namespace EternaApp.Models;

public class PortfolioImage
{
    public int Id { get; set; }
    public int PortfolioId { get; set; }
    public Portfolio Portfolio { get; set; }
    public string ImageUrl { get; set; }    
    public bool IsMain { get; set; }
    
}