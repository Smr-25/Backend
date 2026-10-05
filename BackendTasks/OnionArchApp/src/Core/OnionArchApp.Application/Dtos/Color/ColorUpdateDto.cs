using FluentValidation;

namespace OnionArchApp.Application.Dtos.Color;

public record ColorUpdateDto(string? Name, string? HexCode);

public class ColorUpdateDtoValidator : AbstractValidator<ColorUpdateDto>
{
    public ColorUpdateDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Color name is required.")
            .MaximumLength(30).WithMessage("Color name must not exceed 30 characters.")
            .When(c => c.Name is not null);

        RuleFor(c => c.HexCode)
            .NotEmpty().WithMessage("Hex code is required.")
            .Matches("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$").WithMessage("Hex code must be a valid format.")
            .When(c => c.HexCode is not null);
    }
}