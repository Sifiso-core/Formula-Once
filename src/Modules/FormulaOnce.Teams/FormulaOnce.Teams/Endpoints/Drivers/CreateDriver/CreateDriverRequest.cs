namespace FormulaOnce.Teams.Endpoints.Drivers.CreateDriver;

public record CreateDriverRequest(
    string FirstName,
    string LastName,
    string FullName,
    string Acronym,
    int RacingNumber,
    string Nationality,
    DateTime DateOfBirth,
    Guid ConstructorId);