namespace FormulaOnce.Commerce.Endpoints.Customer.UserDashboard;

public record OrderSummaryDto(
    Guid OrderId,
    DateTime OrderDate,
    decimal TotalAmount,
    string Status);