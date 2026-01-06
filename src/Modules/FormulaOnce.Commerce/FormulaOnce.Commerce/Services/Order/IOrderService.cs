using Ardalis.Result;

namespace FormulaOnce.Commerce.Services.Order;

public interface IOrderService
{
    Task<Result<Guid>> CheckoutAsync(Guid userId, CancellationToken ct);
}