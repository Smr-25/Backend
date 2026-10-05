using FluentValidation;

namespace FirstApiApp.Dtos.Products;

public class ProductCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public List<int> ColorIds { get; set; }
}

public class ProductCreateDtoValidator:AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Product description is required.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");

        RuleFor(p => p.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be a positive integer.");

        RuleFor(p => p.ColorIds)
            .NotNull().WithMessage("ColorIds cannot be null.")
            .Must(colorIds => colorIds.All(id => id > 0)).WithMessage("All ColorIds must be positive integers.");
    }
}