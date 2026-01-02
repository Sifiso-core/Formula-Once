using FastEndpoints;
using FluentValidation;

namespace FormulaOnce.Teams.Endpoints.Drivers.CreateDriver;

public class CreateDriverRequestValidator : Validator<CreateDriverRequest>
{
    public CreateDriverRequestValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Please specify a full name");
        RuleFor(x => x.Acronym).NotEmpty().WithMessage("Please specify an acronym");
        RuleFor(x => x.RacingNumber).GreaterThan(0).WithMessage("Racing number must be greater than zero");
        RuleFor(x => x.Nationality).NotEmpty().WithMessage("Please specify the driver nationality");
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now).WithMessage("Date of birth must be in the past");
        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Please specify a date of birth");
        RuleFor(x => x.ConstructorId).NotEqual(Guid.Empty).WithMessage("Please specify a valid constructor ID");
    }
}