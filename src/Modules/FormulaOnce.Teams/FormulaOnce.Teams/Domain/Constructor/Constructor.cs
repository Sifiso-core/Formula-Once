using Ardalis.GuardClauses;

namespace FormulaOnce.Teams.Domain.Constructor;

internal class Constructor
{
    private readonly List<Driver.Driver> _drivers = [];

    private Constructor()
    {
        //For EF Core}
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string BaseLocation { get; private set; } = string.Empty;
    public int EstablishedYear { get; private set; }
    public IReadOnlyCollection<Driver.Driver> Drivers => _drivers.AsReadOnly();

    public ConstructorStats Stats { get; private set; } = null!;

    public void UpdateDetails(string name, string baseLocation, int year, ConstructorStats stats)
    {
        Name = Guard.Against.NullOrEmpty(name);
        BaseLocation = Guard.Against.NullOrEmpty(baseLocation);
        EstablishedYear = Guard.Against.OutOfRange(year, nameof(year), 1900, DateTime.UtcNow.Year);

        // For Value Objects/Owned Types, you can often just replace them
        Stats = stats;
    }

    public class Factory
    {
        public static Constructor Create(string name, string baseLocation, int establishedYear, ConstructorStats stats,
            Guid? id = null!)
        {
            return new Constructor
            {
                Id = id ?? Guid.NewGuid(),
                Name = Guard.Against.NullOrEmpty(name),
                BaseLocation = Guard.Against.NullOrEmpty(baseLocation),
                EstablishedYear = Guard.Against.NegativeOrZero(establishedYear),
                Stats = Guard.Against.Null(stats)
            };
        }
    }
}