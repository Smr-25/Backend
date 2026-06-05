namespace EternaApp.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    List<Portfolio> Portfolios { get; set; }
}