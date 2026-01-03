using FastEndpoints;
using FluentValidation;
using FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor._Dtos;

namespace FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor.Validators;

public class AllTimeSummaryValidator : Validator<UpdateAllTimeSummaryDto>
{
    public AllTimeSummaryValidator()
    {
        RuleFor(x => x.GrandPrixEntered).GreaterThanOrEqualTo(0);
        RuleFor(x => x.TeamPoints).GreaterThanOrEqualTo(0);
        RuleFor(x => x.WorldChampionships).GreaterThanOrEqualTo(0);

        // Regex to ensure "1 (x249)" format from the image if strictness is needed
        RuleFor(x => x.HighestRaceFinish)
            .NotEmpty()
            //.Matches(@"^\d+\s?(\(x\d+\))?$")
            .WithMessage("Highest Race Finish must be provided & in a format '1' or '1 (x249)'.");
    }
}