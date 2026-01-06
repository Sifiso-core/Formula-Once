using FormulaOnce.Commerce.Infrastructure.Data;

namespace FormulaOnce.Commerce.Infrastructure.Repositories.Product;

public class ProductRepository(CommerceDbContext context) : IProductRepository
{
    public async Task<Domain.Product.Product?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.Products.FindAsync([id], ct);
    }

    public async Task AddAsync(Domain.Product.Product product, CancellationToken ct)
    {
        await context.Products.AddAsync(product, ct);
        await context.SaveChangesAsync(ct);
    }
}