using FluentValidation;
using OnionArchApp.Domain.Enums;

namespace OnionArchApp.Application.Dtos.Product;

public record ProductUpdateDto(string? Name, decimal? Price, ProductStatus? Status, int? CategoryId);

public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateDtoValidator()
    {
        When(p => p.Name is not null, () =>
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(50).WithMessage("Product name must not exceed 50 characters.");
        });

        When(p => p.Price is not null, () =>
        {
            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");
        });

        When(p => p.CategoryId is not null, () =>
        {
            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be a positive integer.");
        });
    }
}