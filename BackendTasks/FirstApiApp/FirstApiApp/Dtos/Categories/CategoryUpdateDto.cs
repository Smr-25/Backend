using FirstApiApp.Attributes;

namespace FirstApiApp.Dtos.Categories;

public class CategoryUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    [FileLength(2*1024*1024)]
    [FileType("image/jpeg","image/png","image/jpg")]
    public IFormFile? File { get; set; }
}