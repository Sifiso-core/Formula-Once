using Ardalis.Result;
using FormulaOnce.Commerce.Infrastructure.Data;
using FormulaOnce.Commerce.Infrastructure.Repositories;
using FormulaOnce.Commerce.Infrastructure.Repositories.Cart;
using FormulaOnce.Commerce.Infrastructure.Repositories.Customer;
using FormulaOnce.Commerce.Infrastructure.Repositories.Order;
using FormulaOnce.Commerce.Infrastructure.Repositories.Product;

namespace FormulaOnce.Commerce.Services.Order;

public class OrderService(
    CommerceDbContext context,
    ICartRepository cartRepository,
    ICustomerRepository customerRepository,
    IProductRepository productRepository,
    IOrderRepository orderRepository) : IOrderService
{
    public async Task<Result<Guid>> CheckoutAsync(Guid userId, CancellationToken ct)
    {
        var cart = await cartRepository.GetByUserIdAsync(userId, ct);
        if (cart == null || !cart.Items.Any())
            return Result.Error("Cannot checkout with an empty cart.");

        var customer = await customerRepository.GetByIdAsync(userId, ct);
        if (customer?.DefaultShippingAddress == null)
            return Result.Error("Please set a default shipping address before checkout.");


        await using var transaction = await context.Database.BeginTransactionAsync(ct);
        try
        {
            var orderResult = Domain.Order.Order.Create(userId, customer.DefaultShippingAddress);
            if (!orderResult.IsSuccess)
                return orderResult.Map(x => x.Id);

            var order = orderResult.Value;

            foreach (var cartItem in cart.Items)
            {
                var product = await productRepository.GetByIdAsync(cartItem.ProductId, ct);

                if (product == null)
                    return Result.NotFound($"Product {cartItem.ProductId} not found.");

                var stockResult = product.UpdateStock(-cartItem.Quantity);
                if (!stockResult.IsSuccess)
                {
                    return Result.Conflict(
                        $"Insufficient stock for {product.Name}. Available: {product.StockQuantity}");
                }

                var addItemResult = order.AddItem(product.Id, product.Price.Amount, cartItem.Quantity);
                if (!addItemResult.IsSuccess)
                    return addItemResult.Map(x => order.Id);
            }

            await orderRepository.AddAsync(order, ct);
            cart.Clear();

            await context.SaveChangesAsync(ct);

            await transaction.CommitAsync(ct);

            return Result.Success(order.Id);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(ct);
            return Result.Error("An unexpected error occurred during checkout.");
        }
    }
}