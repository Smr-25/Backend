using FluentValidation;

namespace MiniAppApi.Dtos.Events;

public class EventCreateBannerDto
{
    public IFormFile BannerImage { get; set; }
}

public class EventCreateBannerDtoValidator : AbstractValidator<EventCreateBannerDto>
{
    public EventCreateBannerDtoValidator()
    {
        RuleFor(e => e.BannerImage).NotNull().WithMessage("File is required.")
            .Must(file => file != null && file.Length <= 2 *1024*1024)
            .WithMessage("File size must not exceed 2 MB.")
            .Must(file => file != null && (file.ContentType == "image/jpeg" || file.ContentType == "image/png" || file.ContentType == "image/webp" || file.ContentType == "image/jpg"))
            .WithMessage("Only JPEG and PNG files are allowed.");
    }
}