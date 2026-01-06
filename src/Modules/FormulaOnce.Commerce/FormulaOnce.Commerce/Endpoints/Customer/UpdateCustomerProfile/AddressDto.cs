namespace FormulaOnce.Commerce.Endpoints.Customer.UpdateCustomerProfile;

public record AddressDto(
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country);