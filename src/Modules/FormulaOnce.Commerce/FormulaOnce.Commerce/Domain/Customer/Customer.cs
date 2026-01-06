namespace FormulaOnce.Commerce.Domain.Customer;

public class Customer
{
    public Guid Id { get; private set; } // Matches Identity UserId
    public string Email { get; private set; }
    public Address? DefaultShippingAddress { get; private set; }
    public Address? DefaultBillingAddress { get; private set; }

    private Customer()
    {
    }

    public static Customer Create(Guid userId, string email) =>
        new() { Id = userId, Email = email };

    public void UpdateAddresses(Address shipping, Address billing)
    {
        DefaultShippingAddress = shipping;
        DefaultBillingAddress = billing;
    }
}