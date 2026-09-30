using ChampionPortal.Application.Commands.Activities;
using FluentValidation;

namespace ChampionPortal.Application.Validators;

public class SubmitActivityCommandValidator : AbstractValidator<SubmitActivityCommand>
{
    public SubmitActivityCommandValidator()
    {
        RuleFor(x => x.ActivityTypeId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(2000);
    }
}

public class EndorseActivityCommandValidator : AbstractValidator<EndorseActivityCommand>
{
    public EndorseActivityCommandValidator()
    {
        RuleFor(x => x.ActivityLogId).NotEmpty();
        RuleFor(x => x.Comment).MaximumLength(1000);
    }
}
