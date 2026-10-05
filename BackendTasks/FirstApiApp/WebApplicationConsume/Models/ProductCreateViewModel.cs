using System.ComponentModel.DataAnnotations;

namespace WebApplicationConsume.Models;
public class ProductCreateViewModel
{
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    [Display(Name = "Product Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    [Display(Name = "Price")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Category is required")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "At least one color must be selected")]
    [Display(Name = "Colors")]
    public List<int> ColorsId { get; set; } = new List<int>();

    // For dropdowns
    public List<CategoryReturnDto>? Categories { get; set; }
    public List<ColorReturnDto>? Colors { get; set; }
}
