namespace MenuApp.Dtos.Products;

public class ProductReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsNew { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
}