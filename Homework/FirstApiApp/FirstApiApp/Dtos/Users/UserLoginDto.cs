using FluentValidation;

namespace FirstApiApp.Dtos.Users;

public class UserLoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}