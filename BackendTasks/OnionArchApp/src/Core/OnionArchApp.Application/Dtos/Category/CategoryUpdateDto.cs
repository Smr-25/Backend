using FluentValidation;

namespace OnionArchApp.Application.Dtos.Category;

public record CategoryUpdateDto(string? Name);

public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(20).WithMessage("Category name must not exceed 20 characters.");
    }
}