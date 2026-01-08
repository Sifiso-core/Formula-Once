using FastEndpoints;
using FluentValidation;

namespace FormulaOnce.Commerce.Endpoints.Customer.UpdateCustomerProfile;

public class AddressValidator : Validator<AddressDto>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street).NotEmpty().MaximumLength(200);
        RuleFor(x => x.City).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PostalCode).NotEmpty().Matches(@"^\d{5}(-\d{4})?$")
            .WithMessage("Invalid Zip Code format.");
        RuleFor(x => x.Country).NotEmpty().MaximumLength(100);
    }
}