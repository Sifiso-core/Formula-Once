using FastEndpoints;
using FluentValidation;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit.Requests;

namespace FormulaOnce.Events.Endpoints.Race.CreateRace;

public class CreateRaceValidator : Validator<CreateRaceRequest>
{
    public CreateRaceValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Race title (e.g. 'Australian Grand Prix') is required")
            .MaximumLength(150);

        RuleFor(x => x.Season)
            .InclusiveBetween(2020, 2100).WithMessage("Season must be a valid year");

        RuleFor(x => x.Round)
            .InclusiveBetween(1, 30).WithMessage("Round must be between 1 and 30");

        RuleFor(x => x.CircuitId)
            .NotEmpty().WithMessage("A valid Circuit ID must be provided");

        RuleFor(x => x.NumberOfLaps)
            .GreaterThan(0).WithMessage("Number of laps must be greater than zero");

        RuleFor(x => x.MainRaceStartTime)
            .NotEmpty().WithMessage("Main race start time is required")
            .Must(x => x > DateTime.UtcNow).WithMessage("Race start time must be in the future");

        // Validate that if it is a Sprint weekend, the start time allows for a 3-day event
        RuleFor(x => x.MainRaceStartTime)
            .Must(x => x.DayOfWeek == DayOfWeek.Sunday)
            .WithMessage("Main races must be scheduled on a Sunday");
    }
}