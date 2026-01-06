namespace FormulaOnce.Commerce.Domain.Order;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal PriceAtPurchase { get; private set; }
    public int Quantity { get; private set; }

    internal OrderItem(Guid productId, decimal priceAtPurchase, int quantity)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        PriceAtPurchase = priceAtPurchase;
        Quantity = quantity;
    }
}