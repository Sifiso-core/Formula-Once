using FastEndpoints;
using FluentValidation;

namespace FormulaOnce.Teams.Endpoints.Drivers.UpdateDriver;

public class UpdateDriverRequestValidator : Validator<UpdateDriverRequest>
{
    public UpdateDriverRequestValidator()
    {
        RuleFor(x => x.RacingNumber).NotEmpty().WithMessage("Please specify a number");
        RuleFor(x => x.Nationality).NotEmpty().WithMessage("Please specify a nationality");
        RuleFor(x => x.ConstructorId).NotEqual(Guid.Empty)
            .WithMessage("Please specify a constructor id");
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Please specify a full name");
        RuleFor(x => x.Acronym).NotEmpty().WithMessage("Please specify a acronym");
    }
}