using Ardalis.GuardClauses;
using Ardalis.Result;
using FormulaOnce.Commerce.Domain.Customer;

namespace FormulaOnce.Commerce.Domain.Order;

public class Order
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public Address ShippingAddress { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order()
    {
    }

    public static Result<Order> Create(Guid userId, Address shippingAddress)
    {
        Guard.Against.Default(userId);
        Guard.Against.Null(shippingAddress);

        return new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            ShippingAddress = shippingAddress
        };
    }

    public Result AddItem(Guid productId, decimal priceAtPurchase, int quantity)
    {
        if (Status != OrderStatus.Pending)
            return Result.Error("Cannot modify a finalized order.");

        if (quantity <= 0)
            return Result.Invalid(new ValidationError("Quantity must be positive."));

        _items.Add(new OrderItem(productId, priceAtPurchase, quantity));
        TotalAmount = _items.Sum(x => x.PriceAtPurchase * x.Quantity);

        return Result.Success();
    }

    private void CalculateTotal()
    {
        TotalAmount = _items.Sum(x => x.PriceAtPurchase * x.Quantity);
    }
}