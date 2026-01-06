namespace FormulaOnce.Commerce.Endpoints.Customer.UpdateCustomerProfile;

public record UpdateCustomerProfileRequest(
    AddressDto ShippingAddress,
    AddressDto BillingAddress);