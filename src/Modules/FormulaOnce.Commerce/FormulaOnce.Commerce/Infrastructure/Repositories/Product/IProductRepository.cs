namespace FormulaOnce.Commerce.Infrastructure.Repositories.Product;

public interface IProductRepository
{
    Task<Domain.Product.Product?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Product.Product product, CancellationToken ct);
}