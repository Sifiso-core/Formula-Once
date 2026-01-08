using FormulaOnce.Commerce.Endpoints.Cart.GetUserCart;
using FormulaOnce.Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FormulaOnce.Commerce.Infrastructure.Repositories.Cart;

public class CartRepository : ICartRepository
{
    private readonly CommerceDbContext _context;

    public CartRepository(CommerceDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Cart.Cart?> GetByUserIdAsync(Guid userId, CancellationToken ct)
    {
        //context.ChangeTracker.Clear();

        return await _context.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserId == userId, ct);
    }

    public async Task AddAsync(Domain.Cart.Cart cart, CancellationToken ct)
    {
        await _context.Carts.AddAsync(cart, ct);

        await _context.SaveChangesAsync(ct);
    }


    public async Task UpdateAsync(Domain.Cart.Cart cart, CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }

    public async Task<CartDto?> GetCartDtoByUserIdAsync(Guid userId, CancellationToken ct)
    {
        return await _context.Carts
            .Where(c => c.UserId == userId)
            .Select(c => new CartDto(
                c.UserId,
                c.Items.Select(i => new CartItemDto(
                    i.ProductId,
                    _context.Products.First(p => p.Id == i.ProductId).Name,
                    i.Quantity,
                    _context.Products.First(p => p.Id == i.ProductId).Price.Amount,
                    i.Quantity * _context.Products.First(p => p.Id == i.ProductId).Price.Amount
                )).ToList(),
                c.Items.Sum(i =>
                    i.Quantity * _context.Products.First(p => p.Id == i.ProductId).Price.Amount)
            ))
            .FirstOrDefaultAsync(ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }
}