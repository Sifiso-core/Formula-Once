using FormulaOnce.Teams.Domain.Driver;

namespace FormulaOnce.Teams.Services.DriverServices.Dto;

public class DriverDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
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

    public DriverStats CareerDriverStats { get; set; } = null!;
}