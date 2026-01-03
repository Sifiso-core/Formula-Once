using FastEndpoints;
using FluentValidation;
using FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor._Dtos;

namespace FormulaOnce.Teams.Endpoints.Constructors.CreateConstructor.Validators;

public class CreateAllTimeSummaryValidator : Validator<UpdateAllTimeSummaryDto>
{
    public CreateAllTimeSummaryValidator()
    {
        RuleFor(x => x.GrandPrixEntered).GreaterThanOrEqualTo(0);
        RuleFor(x => x.TeamPoints).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Podiums).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PolePositions).GreaterThanOrEqualTo(0);
        RuleFor(x => x.WorldChampionships).GreaterThanOrEqualTo(0);

        // Validation for the Ferrari finish format: "1 (x249)"
        RuleFor(x => x.HighestRaceFinish)
            .NotEmpty()
            .Matches(@"^\d+\s?(\(x\d+\))?$")
            .WithMessage("Highest Race Finish must be a number or formatted like '1 (x249)'.");

        RuleFor(x => x.HighestGridPosition)
            .NotEmpty()
            .Matches(@"^\d+\s?(\(x\d+\))?$")
            .WithMessage("Highest Grid Position must be a number or formatted like '1 (x254)'.");
    }
}