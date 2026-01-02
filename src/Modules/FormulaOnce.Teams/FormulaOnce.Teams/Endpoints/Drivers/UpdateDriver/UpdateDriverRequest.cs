namespace FormulaOnce.Teams.Endpoints.Drivers.UpdateDriver;

public record UpdateDriverRequest(
    Guid Id,
    string FullName,
    string Acronym,
    int RacingNumber,
    string Nationality,
    Guid ConstructorId);