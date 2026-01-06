using Ardalis.Result;
using FormulaOnce.Commerce.Infrastructure.Repositories;
using FormulaOnce.Commerce.Infrastructure.Repositories.Cart;
using FormulaOnce.Commerce.Infrastructure.Repositories.Product;

namespace FormulaOnce.Commerce.Services.Cart;

public class CartService(
    ICartRepository cartRepository,
    IProductRepository productRepository) : ICartService
{
    public async Task<Result> AddToCartAsync(Guid userId, Guid productId, int quantity, CancellationToken ct)
    {
       
        var product = await productRepository.GetByIdAsync(productId, ct);
        if (product == null)
            return Result.NotFound("Product not found.");

        if (product.StockQuantity < quantity)
            return Result.Conflict($"Insufficient stock. Only {product.StockQuantity} available.");

       
        var cart = await cartRepository.GetByUserIdAsync(userId, ct);
        bool isNew = false;

        if (cart == null)
        {
            cart = Domain.Cart.Cart.Create(userId);
            isNew = true;
        }

       
        var result = cart.AddOrUpdateItem(productId, quantity);
        if (!result.IsSuccess)
            return result;

       
        if (isNew)
            await cartRepository.AddAsync(cart, ct);
        else
            await cartRepository.UpdateAsync(cart, ct);

        return Result.Success();
    }

    public async Task<Result> RemoveFromCartAsync(Guid userId, Guid productId, CancellationToken ct)
    {
        var cart = await cartRepository.GetByUserIdAsync(userId, ct);
        if (cart == null) return Result.NotFound();

        cart.RemoveItem(productId);
        await cartRepository.UpdateAsync(cart, ct);

        return Result.Success();
    }
}