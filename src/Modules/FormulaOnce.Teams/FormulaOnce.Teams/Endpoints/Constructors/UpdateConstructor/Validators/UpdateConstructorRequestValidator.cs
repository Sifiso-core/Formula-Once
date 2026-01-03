using FastEndpoints;
using FluentValidation;

namespace FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor.Validators;

public class UpdateConstructorRequestValidator : Validator<UpdateConstructorRequest>
{
    public UpdateConstructorRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Constructor ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Team name is required.")
            .MaximumLength(100);

        RuleFor(x => x.BaseLocation)
            .NotEmpty().WithMessage("Base location is required.")
            .MaximumLength(200);

        RuleFor(x => x.EstablishedYear)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .WithMessage($"Established year must be between 1900 and {DateTime.UtcNow.Year}.");

        // Validate Nested Stats
        RuleFor(x => x.Stats).NotNull();

        RuleFor(x => x.Stats.AllTimeSummary).SetValidator(new AllTimeSummaryValidator());
        RuleFor(x => x.Stats.SeasonStats).SetValidator(new SeasonStatsValidator());
    }
}