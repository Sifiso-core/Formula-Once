using FastEndpoints;
using FluentValidation;

namespace FormulaOnce.Commerce.Endpoints.Customer.UpdateCustomerProfile;

public class UpdateProfileValidator : Validator<UpdateCustomerProfileRequest>
{
    public UpdateProfileValidator()
    {
        RuleFor(x => x.ShippingAddress)
            .NotNull()
            .SetValidator(new AddressValidator());

        RuleFor(x => x.BillingAddress)
            .NotNull()
            .SetValidator(new AddressValidator());
    }
}