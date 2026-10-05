using OnionArchApp.Domain.Entities.Common;

namespace OnionArchApp.Domain.Entities;

public class Color : BaseEntity
{
    public string Name { get; set; } = null!;
    public string HexCode { get; set; } = null!;
    public List<ProductColor> ProductColors { get; set; } = [];
}