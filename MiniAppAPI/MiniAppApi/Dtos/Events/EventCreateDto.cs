using FluentValidation;

namespace MiniAppApi.Dtos.Events;

public class EventCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; } 
    public int OrganizerId { get; set; }
    public IFormFile LogoImage { get; set; }
}

public class EventCreateDtoValidator : AbstractValidator<EventCreateDto>
{
    public EventCreateDtoValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(150).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(e => e.Date)
            .GreaterThan(DateTime.Now).WithMessage("Event date must be in the future.");

        RuleFor(e => e.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(200).WithMessage("Location cannot exceed 200 characters.");

        RuleFor(e => e.OrganizerId)
            .GreaterThan(0).WithMessage("OrganizerId must be a positive integer.");
        
        RuleFor(e => e.LogoImage)
            .NotNull().WithMessage("Logo image is required.")
            .Must(file => file.Length > 0).WithMessage("Logo image cannot be empty.");
    }
}