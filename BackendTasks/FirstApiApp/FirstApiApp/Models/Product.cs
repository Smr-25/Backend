namespace FirstApiApp.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; } 
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UptadeDate { get; set; }
    
    public List<ProductColor> ProductColors { get; set; }
}
