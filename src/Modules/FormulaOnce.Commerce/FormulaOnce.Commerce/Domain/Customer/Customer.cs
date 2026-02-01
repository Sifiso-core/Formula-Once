namespace FormulaOnce.Commerce.Domain.Customer;

public class Customer
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public Address? DefaultShippingAddress { get; private set; }
    public Address? DefaultBillingAddress { get; private set; }

    private Customer()
    {
    }

    public void UpdateAddresses(Address shipping, Address billing)
    {
        DefaultShippingAddress = shipping;
        DefaultBillingAddress = billing;
    }
}