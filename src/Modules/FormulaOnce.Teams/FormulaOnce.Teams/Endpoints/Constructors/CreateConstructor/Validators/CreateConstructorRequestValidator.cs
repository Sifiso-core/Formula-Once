using FastEndpoints;
using FluentValidation;

namespace FormulaOnce.Teams.Endpoints.Constructors.CreateConstructor.Validators;

public class CreateConstructorRequestValidator : Validator<CreateConstructorRequest>
{
    public CreateConstructorRequestValidator()
    {
        // Metadata Validation
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Constructor name is required.")
            .MaximumLength(100);

        RuleFor(x => x.BaseLocation)
            .NotEmpty().WithMessage("Base location is required.")
            .MaximumLength(200);

        RuleFor(x => x.EstablishedYear)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .WithMessage($"Established year must be between 1900 and {DateTime.UtcNow.Year}.");

        // Stats Validation
        RuleFor(x => x.Stats)
            .NotNull().WithMessage("Constructor statistics are required.");

        RuleFor(x => x.Stats.AllTimeSummary).SetValidator(new CreateAllTimeSummaryValidator());
        RuleFor(x => x.Stats.SeasonStats).SetValidator(new CreateSeasonStatsValidator());
    }
}