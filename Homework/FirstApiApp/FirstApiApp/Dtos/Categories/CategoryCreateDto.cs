using FluentValidation;

namespace FirstApiApp.Dtos.Categories;

public class CategoryCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile File { get; set; }
}

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(c => c.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(c => c.File).NotNull().WithMessage("File is required.")
            .Must(file => file != null && file.Length <= 2 *1024*1024)
            .WithMessage("File size must not exceed 2 MB.")
            .Must(file => file != null && (file.ContentType == "image/jpeg" || file.ContentType == "image/png"))
            .WithMessage("Only JPEG and PNG files are allowed.");

        RuleFor(c => c).Custom((category, context) =>
        {
            if (category.File is not null)
            {
                var allowedTypes = new[] { "image/jpeg", "image/png" };
                if (!allowedTypes.Contains(category.File.ContentType))
                {
                    context.AddFailure("File", "Only JPEG and PNG files are allowed.");
                }

                if (category.File.Length > 2 * 1024 * 1024)
                {
                    context.AddFailure("File", "File size must not exceed 2 MB.");
                }
            }
            else
            {
                context.AddFailure("File", "File is required.");
            }
        });
    }
}