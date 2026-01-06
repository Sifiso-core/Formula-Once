namespace FormulaOnce.Commerce.Endpoints.Customer.UserDashboard;

public record UserDashboardDto(
    Guid UserId,
    int TotalOrdersPlaced,
    decimal LifetimeSpend,
    List<OrderSummaryDto> RecentOrders,
    int ItemsInCurrentCart);