using FastEndpoints;
using FluentValidation;
using FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor._Dtos;

namespace FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor.Validators;

public class SeasonStatsValidator : Validator<UpdateSeasonStatsDto>
{
    public SeasonStatsValidator()
    {
        RuleFor(x => x.SeasonPosition).GreaterThan(0);
        RuleFor(x => x.SeasonPoints).GreaterThanOrEqualTo(0);
        RuleFor(x => x.DNFs).GreaterThanOrEqualTo(0);

        // Nested GP Stats validation
        RuleFor(x => x.GrandPrixStats.GrandPrixRaces).GreaterThanOrEqualTo(0);
        RuleFor(x => x.GrandPrixStats.GrandPrixWins)
            .LessThanOrEqualTo(x => x.GrandPrixStats.GrandPrixRaces)
            .WithMessage("Wins cannot exceed total races.");

        // Nested Sprint Stats validation
        RuleFor(x => x.SprintStats.SprintRaces).GreaterThanOrEqualTo(0);
    }
}