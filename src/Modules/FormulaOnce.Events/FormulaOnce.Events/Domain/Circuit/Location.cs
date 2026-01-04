namespace FormulaOnce.Events.Domain.Circuit;

public record Location
{
    public string Country { get; private set; }
    public string City { get; private set; }
    public Coordinates Coordinates { get; private set; }

    private Location()
    {
    }

    public Location(string country, string city, Coordinates coordinates)
    {
        Country = country;
        City = city;
        Coordinates = coordinates;
    }
}