using FastEndpoints;
using FluentValidation;

namespace FormulaOnce.Teams.Endpoints.Drivers.UpdateDriver;

public class UpdateDriverRequestValidator : Validator<UpdateDriverRequest>
{
    public UpdateDriverRequestValidator()
    {
        RuleFor(x => x.RacingNumber).NotEmpty().WithMessage("Please specify a number");
        RuleFor(x => x.Nationality).NotEmpty().WithMessage("Please specify a nationality");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please specify a first name");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Please specify a last name");
        RuleFor(x => x).Must(BeAValidMatch)
            .WithMessage("Full name must match the combination of first name and last name");
        RuleFor(x => x.ConstructorId).NotEqual(Guid.Empty)
            .WithMessage("Please specify a constructor id");
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Please specify a full name");
        RuleFor(x => x.Acronym).NotEmpty().WithMessage("Please specify a acronym");
    }

    private bool BeAValidMatch(UpdateDriverRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.FullName)) return false;

        var nameParts = req.FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);


        if (nameParts.Length < 2) return false;

        var firstNamePart = nameParts[0];

        var surnamePart = string.Join(" ", nameParts.Skip(1));

        return firstNamePart.Equals(req.FirstName, StringComparison.InvariantCultureIgnoreCase) &&
               surnamePart.Equals(req.LastName, StringComparison.InvariantCultureIgnoreCase);
    }
}