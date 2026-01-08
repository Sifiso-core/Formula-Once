using FormulaOnce.Commerce.Endpoints.Customer.UserDashboard;
using FormulaOnce.Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormulaOnce.Commerce.Infrastructure.Repositories.Order;

public class OrderRepository(CommerceDbContext context) : IOrderRepository
{
    public async Task<Domain.Order.Order?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.Orders
            .Include(x => x.Items) // Must include items to reconstruct the Aggregate
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<Domain.Order.Order>> GetByUserIdAsync(Guid userId, CancellationToken ct)
    {
        return await context.Orders
            .Include(x => x.Items)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.OrderDate)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Domain.Order.Order order, CancellationToken ct)
    {
        await context.Orders.AddAsync(order, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task<UserDashboardDto> GetUserDashboardAsync(Guid userId, CancellationToken ct)
    {
        var orders = await context.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync(ct);

        var cartItemCount = await context.Carts
            .Where(c => c.UserId == userId)
            .SelectMany(c => c.Items)
            .SumAsync(i => i.Quantity, ct);

        return new UserDashboardDto(
            userId,
            orders.Count,
            orders.Sum(o => o.TotalAmount),
            orders.Take(5).Select(o => new OrderSummaryDto(
                o.Id,
                o.OrderDate,
                o.TotalAmount,
                o.Status.ToString()
            )).ToList(),
            cartItemCount
        );
    }
}