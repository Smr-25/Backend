using FirstApiApp.Models;

namespace FirstApiApp.Dtos.Products;

public class ProductReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; } 
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UptadeDate { get; set; }
    public List<ColorInProductReturnDto> ProductColors { get; set; }
}

public class CategoryInProductReturnDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}
public class ColorInProductReturnDto
{
    public string ColorName { get; set; }
}