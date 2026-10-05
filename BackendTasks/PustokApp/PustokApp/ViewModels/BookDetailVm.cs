namespace PustokApp.ViewModels;

public class BookDetailVm
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int DiscountPercent { get; set; }
    public decimal Price { get; set; }
    public string Code { get; set; }
    public bool InStock { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsNew { get; set; }
    public string MainImageUrl { get; set; }
    public string HoverImageUrl { get; set; }
    public List<string> TagNames { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public List<string> BookImagesUrls { get; set; }
}