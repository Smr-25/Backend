using System.ComponentModel.DataAnnotations;

namespace WebApplicationConsume.Models;

public class CategoryCreateViewModel
{
    [Required(ErrorMessage = "Category name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    [Display(Name = "Category Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Please select an image")]
    [Display(Name = "Category Image")]
    public IFormFile File { get; set; }
}
