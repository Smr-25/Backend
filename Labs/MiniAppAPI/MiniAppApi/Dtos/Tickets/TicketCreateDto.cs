using FluentValidation;

namespace MiniAppApi.Dtos.Tickets;

public class TicketCreateDto
{
    public string Type { get; set; }
    public decimal Price { get; set; }
    public int EventId { get; set; }
}
public class TicketCreateByEventDto
{
    public string Type { get; set; }
    public decimal Price { get; set; }
}
public class TicketCreateDtoValidator : AbstractValidator<TicketCreateDto>
{
    public TicketCreateDtoValidator()
    {
        RuleFor(t => t.Type)
            .NotEmpty().WithMessage("Ticket type is required.")
            .MaximumLength(50).WithMessage("Ticket type cannot exceed 100 characters.");

        RuleFor(t => t.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Ticket price must be a non-negative value.");

        RuleFor(t => t.EventId)
            .GreaterThan(0).WithMessage("Event ID must be a positive integer.");
    }
}