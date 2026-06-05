using FluentValidation;

namespace OnionArchApp.Application.Dtos.Category;

public record CategoryCreateDto(string Name);

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(20).WithMessage("Category name must not exceed 20 characters.");
    }
}