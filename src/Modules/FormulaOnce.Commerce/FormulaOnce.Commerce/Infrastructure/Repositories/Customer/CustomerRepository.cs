using FormulaOnce.Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormulaOnce.Commerce.Infrastructure.Repositories.Customer;

public class CustomerRepository(CommerceDbContext context) : ICustomerRepository
{
    public async Task<Domain.Customer.Customer?> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        return await context.Customers
            .FirstOrDefaultAsync(x => x.Id == userId, ct);
    }

    public async Task UpdateAsync(Domain.Customer.Customer customer, CancellationToken ct)
    {
        await context.SaveChangesAsync(ct);
    }
    public async Task AddAsync(Domain.Customer.Customer customer, CancellationToken ct)
    {
        await context.Customers.AddAsync(customer, ct);
        await context.SaveChangesAsync(ct);
    }
}