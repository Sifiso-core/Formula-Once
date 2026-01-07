using FormulaOnce.Commerce.Endpoints.Cart.GetUserCart;

namespace FormulaOnce.Commerce.Infrastructure.Repositories.Cart;

public interface ICartRepository
{
    Task<Domain.Cart.Cart?> GetByUserIdAsync(Guid userId, CancellationToken ct);
    Task AddAsync(Domain.Cart.Cart cart, CancellationToken ct);
    Task UpdateAsync(Domain.Cart.Cart cart, CancellationToken ct);
    Task<CartDto?> GetCartDtoByUserIdAsync(Guid userId, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}