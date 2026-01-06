using FormulaOnce.Commerce.Endpoints.Customer.UserDashboard;

namespace FormulaOnce.Commerce.Infrastructure.Repositories.Order;

public interface IOrderRepository
{
    Task<Domain.Order.Order?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Order.Order order, CancellationToken ct);
    Task<UserDashboardDto> GetUserDashboardAsync(Guid userId, CancellationToken ct);
}