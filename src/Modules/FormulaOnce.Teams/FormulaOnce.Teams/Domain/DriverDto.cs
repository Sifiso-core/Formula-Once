namespace FormulaOnce.Teams.Domain;

public class DriverDto
{
    public string FullName { get; set; } = string.Empty;

    public string Acronym { get; private set; } = string.Empty;
    public int RacingNumber { get; set; }

    public string Nationality { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public Guid ConstructorId { get; set; }
    public int Age { get; set; }
    public Stats CareerStats { get; set; } = null!;
}