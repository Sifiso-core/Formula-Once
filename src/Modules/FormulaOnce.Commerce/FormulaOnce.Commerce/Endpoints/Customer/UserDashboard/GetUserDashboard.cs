using System.Security.Claims;
using FastEndpoints;
using FormulaOnce.Commerce.Infrastructure.Repositories.Order;

namespace FormulaOnce.Commerce.Endpoints.Customer.UserDashboard;

internal class GetUserDashboard(IOrderRepository orderRepository)
    : EndpointWithoutRequest<UserDashboardDto>
{
    public override void Configure()
    {
        Get("/commerce/dashboard");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var dashboard = await orderRepository.GetUserDashboardAsync(userId, ct);

        await Send.OkAsync(dashboard, cancellation: ct);
    }
}