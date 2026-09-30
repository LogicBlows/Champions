using ChampionPortal.Application.Commands.Auth;
using FluentValidation;

namespace ChampionPortal.Application.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.EmployeeCode).NotEmpty().MaximumLength(50);
    }
}
