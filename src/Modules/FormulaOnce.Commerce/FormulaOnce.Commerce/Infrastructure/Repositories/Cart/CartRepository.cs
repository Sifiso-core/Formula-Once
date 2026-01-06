using FormulaOnce.Commerce.Endpoints.Cart.GetUserCart;
using FormulaOnce.Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormulaOnce.Commerce.Infrastructure.Repositories.Cart;

public class CartRepository(CommerceDbContext context) : ICartRepository
{
    public async Task<Domain.Cart.Cart?> GetByUserIdAsync(Guid userId, CancellationToken ct)
    {
        return await context.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserId == userId, ct);
    }

    public async Task AddAsync(Domain.Cart.Cart cart, CancellationToken ct)
    {
        await context.Carts.AddAsync(cart, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Domain.Cart.Cart cart, CancellationToken ct)
    {
             await context.SaveChangesAsync(ct);
         }

    public async Task<CartDto?> GetCartDtoByUserIdAsync(Guid userId, CancellationToken ct)
    {
        return await context.Carts
            .Where(c => c.UserId == userId)
            .Select(c => new CartDto(
                c.UserId,
                c.Items.Select(i => new CartItemDto(
                    i.ProductId,
                    context.Products.First(p => p.Id == i.ProductId).Name,
                    i.Quantity,
                    context.Products.First(p => p.Id == i.ProductId).Price.Amount,
                    i.Quantity * context.Products.First(p => p.Id == i.ProductId).Price.Amount
                )).ToList(),
                c.Items.Sum(i =>
                    i.Quantity * context.Products.First(p => p.Id == i.ProductId).Price.Amount)
            ))
            .FirstOrDefaultAsync(ct);
    }
}