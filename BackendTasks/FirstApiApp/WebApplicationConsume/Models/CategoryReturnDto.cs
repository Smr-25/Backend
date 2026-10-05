namespace WebApplicationConsume.Models;

public class CategoryReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public List<ProductInCategoryDto> Products { get; set; }
    public int ProductsCount { get; set; }
}

public class ProductInCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
