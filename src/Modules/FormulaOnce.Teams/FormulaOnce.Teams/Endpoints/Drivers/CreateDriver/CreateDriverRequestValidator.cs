using FastEndpoints;
using FluentValidation;

namespace FormulaOnce.Teams.Endpoints.Drivers.CreateDriver;

public class CreateDriverRequestValidator : Validator<CreateDriverRequest>
{
    public CreateDriverRequestValidator()
    {
        RuleFor(x => x.Acronym).NotEmpty().WithMessage("Please specify an acronym");
        RuleFor(x => x.RacingNumber).GreaterThan(0).WithMessage("Racing number must be greater than zero");
        RuleFor(x => x.Nationality).NotEmpty().WithMessage("Please specify the driver nationality");
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now).WithMessage("Date of birth must be in the past");
        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Please specify a date of birth");
        RuleFor(x => x.ConstructorId).NotEqual(Guid.Empty).WithMessage("Please specify a valid constructor ID");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please specify the driver's first name");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Please specify the driver's surname");
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Please specify the driver's full name")
            .Must((request, fullName) => BeAValidMatch(request))
            .WithMessage("Full name must match the combination of first name and surname");
    }

    private bool BeAValidMatch(CreateDriverRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.FullName)) return false;

        var nameParts = req.FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Ensure there are at least two parts
        if (nameParts.Length < 2) return false;

        var firstNamePart = nameParts[0];
        // Join remaining parts in case of middle names or double surnames
        var surnamePart = string.Join(" ", nameParts.Skip(1));

        return firstNamePart.Equals(req.FirstName, StringComparison.InvariantCultureIgnoreCase) &&
               surnamePart.Equals(req.LastName, StringComparison.InvariantCultureIgnoreCase);
    }
}