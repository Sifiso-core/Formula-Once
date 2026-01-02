using Ardalis.GuardClauses;

namespace FormulaOnce.Teams.Domain;

internal class Driver
{
    private Driver()
    {
        //For EF Core
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Surname { get; private set; } = string.Empty;
    public string FullName => $"{Name} {Surname}";

    public string Acronym { get; private set; } = string.Empty;
    public int RacingNumber { get; private set; }

    public string Nationality { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public Guid ConstructorId { get; private set; }

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

    public Stats CareerStats { get; private set; } = null!;

    public class Factory
    {
        public static Driver Create(string name, string surname, string nationality, Guid constructorId,
            int racingNumber, DateTime dateOfBirth, string? acryonym)
        {
            return new Driver()
            {
                Id = Guid.NewGuid(),
                Name = Guard.Against.NullOrEmpty(name, nameof(name)),
                Surname = Guard.Against.NullOrEmpty(surname, nameof(surname)),
                Nationality = Guard.Against.NullOrEmpty(nationality, nameof(nationality)),
                ConstructorId = Guard.Against.Default(constructorId, nameof(constructorId)),
                RacingNumber = Guard.Against.NegativeOrZero(racingNumber, nameof(racingNumber)),
                DateOfBirth = Guard.Against.Default(dateOfBirth, nameof(dateOfBirth)),
                CareerStats = new Stats(0, 0, 0, 0, 0),
                Acronym = acryonym ?? surname[0..3].ToUpper()
            };
        }
    }
}