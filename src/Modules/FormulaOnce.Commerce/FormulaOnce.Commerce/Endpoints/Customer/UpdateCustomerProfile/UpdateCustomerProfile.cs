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
        // FastEndpoints automatically links the UpdateProfileValidator here
    }

    public override async Task HandleAsync(UpdateCustomerProfileRequest req, CancellationToken ct)
    {
        // Pull the identity from the JWT claim
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
        // 1. Fetch existing customer (created during user registration via Integration Event or lazy-loaded)
        var customer = await customerRepository.GetByIdAsync(userId, ct);

        if (customer == null)
            return Result.NotFound("Customer profile not found.");

        // 2. Map DTOs to Domain Value Objects (Guard clauses handle validation inside Create)
        var shipping = Address.Create(
            request.ShippingAddress.Street,
            request.ShippingAddress.City,
            request.ShippingAddress.State,
            request.ShippingAddress.ZipCode,
            request.ShippingAddress.Country);

        var billing = Address.Create(
            request.BillingAddress.Street,
            request.BillingAddress.City,
            request.BillingAddress.State,
            request.BillingAddress.ZipCode,
            request.BillingAddress.Country);

        // 3. Update the Aggregate Root
        customer.UpdateAddresses(shipping, billing);

        // 4. Persist
        await customerRepository.UpdateAsync(customer, ct);

        return Result.Success();
    }
}

public interface ICustomerService
{
    Task<Result> UpdateProfileAsync(Guid userId, UpdateCustomerProfileRequest request, CancellationToken ct);
}