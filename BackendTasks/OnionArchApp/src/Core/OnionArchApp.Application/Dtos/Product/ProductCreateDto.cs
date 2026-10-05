using FluentValidation;
using OnionArchApp.Domain.Enums;

namespace OnionArchApp.Application.Dtos.Product;

public record ProductCreateDto(string Name, decimal Price, ProductStatus Status, int CategoryId, List<int> ColorIds);

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(50).WithMessage("Product name must not exceed 50 characters.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");

        RuleFor(p => p.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be a positive integer.");
        RuleFor(p => p.ColorIds)
            .NotNull().WithMessage("ColorIds cannot be null.")
            .Must(colorIds => colorIds.All(id => id > 0)).WithMessage("All ColorIds must be positive integers.");
    }
}