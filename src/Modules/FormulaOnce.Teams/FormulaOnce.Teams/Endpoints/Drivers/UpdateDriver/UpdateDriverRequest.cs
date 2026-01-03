namespace FormulaOnce.Teams.Endpoints.Drivers.UpdateDriver;

public record UpdateDriverRequest(
    Guid Id,
    string FullName,
    string FirstName,
    string LastName,
    string Acronym,
    int RacingNumber,
    string Nationality,
    Guid ConstructorId);