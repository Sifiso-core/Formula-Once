using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using FormulaOnce.Commerce.Domain.Customer;
using FormulaOnce.Commerce.Infrastructure.Repositories.Customer;

namespace FormulaOnce.Commerce.Endpoints.Customer.UpdateCustomerProfile;

internal class UpdateCustomerProfile(ICustomerService customerService)
    : Endpoint<UpdateCustomerProfileRequest>
{
    public override void Configure()
    {
        Put("/commerce/customer/profile");
    }

    public override async Task HandleAsync(UpdateCustomerProfileRequest req, CancellationToken ct)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await customerService.UpdateProfileAsync(userId, req, ct);

        if (result.IsSuccess)
        {
            await Send.NoContentAsync(ct);
            return;
        }

        await Send.NotFoundAsync(ct);
    }
}

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public async Task<Result> UpdateProfileAsync(Guid userId, UpdateCustomerProfileRequest request,
        CancellationToken ct)
    {
        var customer = await customerRepository.GetByIdAsync(userId, ct);

        if (customer == null)
            return Result.NotFound("Customer profile not found.");
        
        //TODO: Add Mapping Extension To Handle Dto to Domain Model Mapping
        var shipping = Address.Create(
            request.ShippingAddress.Street,
            request.ShippingAddress.City,
            request.ShippingAddress.Province,
            request.ShippingAddress.PostalCode,
            request.ShippingAddress.Country);

        var billing = Address.Create(
            request.BillingAddress.Street,
            request.BillingAddress.City,
            request.BillingAddress.Province,
            request.BillingAddress.PostalCode,
            request.BillingAddress.Country);

        customer.UpdateAddresses(shipping, billing);

        await customerRepository.UpdateAsync(customer, ct);

        return Result.Success();
    }
}

public interface ICustomerService
{
    Task<Result> UpdateProfileAsync(Guid userId, UpdateCustomerProfileRequest request, CancellationToken ct);
}