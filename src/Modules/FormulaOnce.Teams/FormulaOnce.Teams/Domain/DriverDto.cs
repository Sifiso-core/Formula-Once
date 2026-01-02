namespace FormulaOnce.Teams.Domain;

public class DriverDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;

    public string Acronym { get; set; } = string.Empty;
    public int RacingNumber { get; set; }

    public string Nationality { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public Guid ConstructorId { get; set; }

    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    public Stats CareerStats { get; set; } = null!;
}