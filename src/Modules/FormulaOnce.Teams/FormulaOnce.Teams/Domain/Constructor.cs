namespace FormulaOnce.Teams.Domain;

public class Constructor
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string BaseLocation { get; private set; } = string.Empty;
    public DateOnly EstablishedYear { get; private set; }
    private readonly List<Driver> _drivers = [];
    public IReadOnlyCollection<Driver> Drivers => _drivers.AsReadOnly();
}