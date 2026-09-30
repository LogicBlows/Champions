using ChampionPortal.Application.Commands.ActivityTypes;
using FluentValidation;

namespace ChampionPortal.Application.Validators;

public class ProposeActivityTypeCommandValidator : AbstractValidator<ProposeActivityTypeCommand>
{
    public ProposeActivityTypeCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.SuggestedPoints).GreaterThan(0).When(x => x.SuggestedPoints.HasValue);
    }
}

public class ReviewActivityTypeCommandValidator : AbstractValidator<ReviewActivityTypeCommand>
{
    public ReviewActivityTypeCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Points).GreaterThan(0).When(x => x.Approved);
    }
}
