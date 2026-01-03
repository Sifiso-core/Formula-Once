using FastEndpoints;
using FluentValidation;
using FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor._Dtos;

namespace FormulaOnce.Teams.Endpoints.Constructors.CreateConstructor.Validators;

public class CreateSeasonStatsValidator : Validator<UpdateSeasonStatsDto>
{
    public CreateSeasonStatsValidator()
    {
        RuleFor(x => x.SeasonPosition).GreaterThan(0);
        RuleFor(x => x.SeasonPoints).GreaterThanOrEqualTo(0);

        // Cross-property logic: Points should generally be >= 0
        RuleFor(x => x.GrandPrixStats.GrandPrixPoints).GreaterThanOrEqualTo(0);

        // Ensure Podium count doesn't exceed race count for the season
        RuleFor(x => x.GrandPrixStats.GrandPrixPodiums)
            .LessThanOrEqualTo(x => x.GrandPrixStats.GrandPrixRaces)
            .WithMessage("Season podiums cannot exceed the number of races entered.");

        RuleFor(x => x.SprintStats.SprintRaces).GreaterThanOrEqualTo(0);
    }
}