using System.Security.Claims;
using FastEndpoints;
using FormulaOnce.Commerce.Infrastructure.Repositories.Order;
using FormulaOnce.Commerce.Services.Order;

namespace FormulaOnce.Commerce.Endpoints.GetOrderById;

internal class GetOrderById(IOrderRepository orderRepository) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/commerce/orders/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var order = await orderRepository.GetByIdAsync(id, ct);

        if (order == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (order.UserId != userId)
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        await Send.OkAsync(new
        {
            order.Id,
            order.OrderDate,
            order.TotalAmount,
            Status = order.Status.ToString(),
            Items = order.Items.Select(i => new
            {
                i.ProductId,
                i.Quantity,
                i.PriceAtPurchase,
                SubTotal = i.Quantity * i.PriceAtPurchase
            })
        }, cancellation: ct);
    }
}