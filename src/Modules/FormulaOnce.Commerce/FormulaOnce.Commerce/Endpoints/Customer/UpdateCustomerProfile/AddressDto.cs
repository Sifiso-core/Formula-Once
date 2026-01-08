namespace FormulaOnce.Commerce.Endpoints.Customer.UpdateCustomerProfile;

public record AddressDto(
    string Street,
    string City,
    string Province,
    string PostalCode,
    string Country);