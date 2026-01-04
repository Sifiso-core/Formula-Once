using Ardalis.GuardClauses;

namespace FormulaOnce.Events.Domain.Circuit;

public record Coordinates
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }

    public Coordinates(double lat, double lon)
    {
        Latitude = Guard.Against.OutOfRange(lat, nameof(lat), -90, 90);
        Longitude = Guard.Against.OutOfRange(lon, nameof(lon), -180, 180);
    }
    private Coordinates() { }
}