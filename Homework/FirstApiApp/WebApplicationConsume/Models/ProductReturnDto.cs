namespace WebApplicationConsume.Models;

public class ProductReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public List<ColorsInProductDto> ProductColors { get; set; }
}

public class ColorsInProductDto
{
    public string ColorName { get; set; }
}
