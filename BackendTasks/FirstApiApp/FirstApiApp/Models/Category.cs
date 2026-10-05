namespace FirstApiApp.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UptadeDate { get; set; }
    public List<Product> Products { get; set; }
    public string ImageUrl { get; set; }
}