using FluentValidation;

namespace MiniAppApi.Dtos.Organizers;

public class OrganizerCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; } 
}

public class OrganizerCreateDtoValidator : AbstractValidator<OrganizerCreateDto>
{
    public OrganizerCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.Phone)
            .MaximumLength(200).WithMessage("Phone number cannot exceed 15 characters.");

       
    }
}