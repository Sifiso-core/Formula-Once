namespace FormulaOnce.Commerce.Domain.Cart;

public class CartItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Guid CartId { get; private set; }
    public Cart? Cart { get; set; }
    public int Quantity { get; private set; }

    internal CartItem(Guid productId, int quantity)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
    }

    public static CartItem Create(Guid productId, Guid cartId, int quantity)
    {
        return new CartItem
        {
            Id = Guid.NewGuid(),
            CartId = cartId,
            ProductId = productId,
            Quantity = quantity
        };
    }

    private CartItem()
    {
    }

    internal void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0) throw new ArgumentException("Quantity must be positive");
        Quantity = newQuantity;
    }
}