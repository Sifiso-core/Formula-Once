using Ardalis.GuardClauses;

namespace FormulaOnce.Teams.Domain.Driver;

internal class Driver
{
    private Driver()
    {
        //For EF Core
    }

    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";

    public string Acronym { get; private set; } = string.Empty;
    public int RacingNumber { get; private set; }

    public string Nationality { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public Guid ConstructorId { get; private set; }
    public Constructor.Constructor Constructor { get; set; }

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

    public DriverStats CareerDriverStats { get; private set; } = null!;

    public void UpdateDetails(string firstName, string surname, Guid constructorId, int racingNumber, string acronym)
    {
        FirstName = Guard.Against.NullOrEmpty(firstName);
        LastName = Guard.Against.NullOrEmpty(surname);
        ConstructorId = Guard.Against.Default(constructorId);
        RacingNumber = Guard.Against.NegativeOrZero(racingNumber);
        Acronym = Guard.Against.NullOrEmpty(acronym);
    }

    public class Factory
    {
        public static Driver Create(string firstName, string surname, string nationality, Guid constructorId,
            int racingNumber, DateTime dateOfBirth, string? acronym, Guid? id = null!)
        {
            return new Driver
            {
                Id = id ?? Guid.NewGuid(),
                FirstName = Guard.Against.NullOrEmpty(firstName),
                LastName = Guard.Against.NullOrEmpty(surname),
                Nationality = Guard.Against.NullOrEmpty(nationality),
                ConstructorId = Guard.Against.Default(constructorId),
                RacingNumber = Guard.Against.NegativeOrZero(racingNumber),
                CareerDriverStats = new DriverStats(0, 0, 0, 0, 0),
                Acronym = acronym ?? surname[..3].ToUpper(),
                DateOfBirth = Guard.Against.Default(dateOfBirth)
            };
        }

        public static Driver CreateForUpdate(string firstName, string surname, string nationality, Guid constructorId,
            int racingNumber, string? acronym, Guid id)
        {
            return new Driver
            {
                Id = id,
                FirstName = Guard.Against.NullOrEmpty(firstName),
                LastName = Guard.Against.NullOrEmpty(surname),
                Nationality = Guard.Against.NullOrEmpty(nationality),
                ConstructorId = Guard.Against.Default(constructorId),
                RacingNumber = Guard.Against.NegativeOrZero(racingNumber),
                CareerDriverStats = new DriverStats(0, 0, 0, 0, 0),
                Acronym = acronym ?? (surname.Length >= 3 ? surname[..3].ToUpper() : surname.ToUpper()),
                // Intentionally do not validate or set a meaningful DateOfBirth here so updates won't fail.
                DateOfBirth = default
            };
        }
    }
}