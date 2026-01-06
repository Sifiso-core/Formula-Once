using Ardalis.Result;

namespace FormulaOnce.Commerce.Services.Cart;

public interface ICartService
{
    Task<Result> AddToCartAsync(Guid userId, Guid productId, int quantity, CancellationToken ct);
    Task<Result> RemoveFromCartAsync(Guid userId, Guid productId, CancellationToken ct);
}