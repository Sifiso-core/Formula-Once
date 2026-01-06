namespace FormulaOnce.Commerce.Infrastructure.Repositories.Customer;

public interface ICustomerRepository
{
    Task<Domain.Customer.Customer?> GetByIdAsync(Guid userId, CancellationToken ct);
    Task UpdateAsync(Domain.Customer.Customer customer, CancellationToken ct);
    Task AddAsync(Domain.Customer.Customer customer, CancellationToken ct);
}