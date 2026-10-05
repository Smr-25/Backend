
using OnionArchApp.Domain.Entities.Common;
using OnionArchApp.Domain.Enums;

namespace OnionArchApp.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public List<ProductColor> ProductColors { get; set; } = []; 
}