namespace FormulaOnce.Teams.Endpoints.Drivers.CreateDriver;

public record CreateDriverRequest(
    string FullName,
    string Acronym,
    int RacingNumber,
    string Nationality,
    DateTime DateOfBirth,
    Guid ConstructorId);
    