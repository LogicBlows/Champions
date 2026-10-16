using ChampionPortal.Application.Commands.Admin;
using FluentValidation;

namespace ChampionPortal.Application.Validators;

public class CreateAdminCompetencyCommandValidator : AbstractValidator<CreateAdminCompetencyCommand>
{
    public CreateAdminCompetencyCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty().MaximumLength(32);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.SortOrder).GreaterThanOrEqualTo(0);
    }
}

public class UpdateAdminCompetencyCommandValidator : AbstractValidator<UpdateAdminCompetencyCommand>
{
    public UpdateAdminCompetencyCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.SortOrder).GreaterThanOrEqualTo(0);
    }
}
