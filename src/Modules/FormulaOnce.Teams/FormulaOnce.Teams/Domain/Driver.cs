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

    public Driver UpdateDriver(Driver driver)
    {
        Name = Guard.Against.NullOrEmpty(driver.Name, nameof(driver.Name));
        ConstructorId = Guard.Against.Default(driver.ConstructorId, nameof(driver.ConstructorId));
        CareerStats = driver.CareerStats;
        Nationality = Guard.Against.NullOrEmpty(driver.Nationality, nameof(driver.Nationality));
        Surname = Guard.Against.NullOrEmpty(driver.Surname, nameof(driver.Surname));
        RacingNumber = Guard.Against.NegativeOrZero(driver.RacingNumber, nameof(driver.RacingNumber));
        CareerStats = driver.CareerStats;
        Acronym = Guard.Against.NullOrEmpty(driver.Acronym, nameof(driver.Acronym));
        return driver;
    }

    public class Factory
    {
        public static Driver Create(string name, string surname, string nationality, Guid constructorId,
            int racingNumber, DateTime dateOfBirth, string? acronym, Guid? id = null!)
        {
            return new Driver()
            {
                Id = id ?? Guid.NewGuid(),
                Name = Guard.Against.NullOrEmpty(name, nameof(name)),
                Surname = Guard.Against.NullOrEmpty(surname, nameof(surname)),
                Nationality = Guard.Against.NullOrEmpty(nationality, nameof(nationality)),
                ConstructorId = Guard.Against.Default(constructorId, nameof(constructorId)),
                RacingNumber = Guard.Against.NegativeOrZero(racingNumber, nameof(racingNumber)),
                CareerStats = new Stats(0, 0, 0, 0, 0),
                Acronym = acronym ?? surname[0..3].ToUpper()
            };
        }
    }
}