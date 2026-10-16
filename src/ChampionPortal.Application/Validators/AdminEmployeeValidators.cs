using ChampionPortal.Application.Commands.Admin;
using FluentValidation;

namespace ChampionPortal.Application.Validators;

public class CreateAdminEmployeeCommandValidator : AbstractValidator<CreateAdminEmployeeCommand>
{
    public CreateAdminEmployeeCommandValidator()
    {
        RuleFor(x => x.EmployeeCode).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.CompetencyId).NotEmpty();
    }
}

public class UpdateAdminEmployeeCommandValidator : AbstractValidator<UpdateAdminEmployeeCommand>
{
    public UpdateAdminEmployeeCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.CompetencyId).NotEmpty();
    }
}
